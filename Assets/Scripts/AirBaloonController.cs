using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AirBalloonController : MiniGame
{
    AudioClip clip;

    // Default Mic
    [Header("Mic Settings")]
    public string selectedDevice;
    public bool breathingIN;
    public float loudness;
    public int bufferSize = 44100;
    private float[] audioBuffer;
    private int bufferPosition;

    [Header("Physics Settings")]
    public float fallSpeed=5f;
    public float upMultiplier=7f;
    Rigidbody rb;



    [Header("UI Settings")]
    public GameObject micWarningDetection;
    public GameObject selectMicScreen;
    public TMP_Dropdown microphoneDropdown;
    public Slider micSensitivity;
    public TMP_Text breathUIText; // Reference to the UI Text for breathing status and countdown.





    void Awake()
    {
        rb = GetComponent < Rigidbody>();
        audioBuffer = new float[bufferSize];
        bufferPosition = 0; // Initialize the buffer position
    }




    void Start()
    {
        // Populate the microphone dropdown with available devices.
        PopulateMicrophoneDropdown();
        selectMicScreen.SetActive(true);
    }
    void Update()
    {
        if (selectedDevice != null && selectedDevice != "Select Microphone...")
        {
            // Update the audio buffer with the latest audio data
            UpdateAudioBuffer();

            // Calculate loudness over the last second of audio
            loudness = CalculateLoudness();

            if (loudness == 0)
            {
                micWarningDetection.SetActive(true);
                selectMicScreen.SetActive(true);
            }
            else
            {
                if (loudness > 0.3f)
                {
                    breathingIN = false;
                }
                else
                {
                    breathingIN = true;
                }
                AirBaloonBehavior();
                micWarningDetection.SetActive(false);
                selectMicScreen.SetActive(false);
                counter += Time.deltaTime;
            }
        }
    }
    void UpdateAudioBuffer()
    {
        if (Microphone.IsRecording(selectedDevice))
        {
            // Copy the audio data from the microphone clip to the audio buffer
            clip.GetData(audioBuffer, bufferPosition);

            // Update the buffer position
            bufferPosition = (bufferPosition + audioBuffer.Length) % bufferSize;
        }
    }

    float CalculateLoudness()
    {
        // Calculate the total loudness from the last second of audio data
        float totalLoudness = 0;

        for (int i = 0; i < audioBuffer.Length; i++)
        {
            totalLoudness += Mathf.Abs(audioBuffer[i]);
        }

        // Calculate the average loudness over the last second
        return totalLoudness / audioBuffer.Length;
    }
    void PopulateMicrophoneDropdown()
    {
        // Clear the dropdown options.
        microphoneDropdown.ClearOptions();

        // Create a list of options.
        List<string> options = new List<string>();

        // Add the "Select Microphone..." option as the first item.
        options.Add("Select Microphone ...");

        // Get a list of available microphones and add them to the options list.
        options.AddRange(Microphone.devices);

        // Set the options in the dropdown.
        microphoneDropdown.AddOptions(options);
    }


    public void OnMicrophoneDropdownValueChanged()
    {
        // This function is called when the dropdown value changes.
        // Set the selected device based on the dropdown selection.
        selectedDevice = Microphone.devices[microphoneDropdown.value].ToString();

        // Start the selected microphone.
        clip = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);
    }




    private void AirBaloonBehavior()
    {
        if (breathingIN)
        {
            // Set UI to say "Breathing In" + countdown timer
            breathUIText.text = "Breathing In: " + (counter).ToString("F1") + "s";

            // Slow down falling speed (adjust the fallSpeed value)
            rb.velocity = new Vector3(0, -fallSpeed * 0.5f, 0);
        }
        else
        {
            // Set UI to "Breathing Out" + countdown timer
            breathUIText.text = "Breathing Out: " + (counter).ToString("F1") + "s";

            // Maintain normal fall speed and apply force related to loudness
            rb.velocity = new Vector3(0, -fallSpeed, 0);

            // Apply an upward force related to loudness (adjust the multiplier as needed)
            rb.AddForce(Vector3.up * loudness * upMultiplier);
        }
    }

}
