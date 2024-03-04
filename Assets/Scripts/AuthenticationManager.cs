using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class AuthenticationManager : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text errorLog;
    public GameObject errorPopUp;
    async void Awake()
    {
        errorPopUp.SetActive(false);
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }


  
    public async void SignUpWithUsernamePassword()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        errorPopUp.SetActive(false);
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            await PlayerManager.instance.SaveAllPlayerData();
            SceneManager.LoadScene("MainScene");
            Debug.Log("SignUp is successful.");

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            errorPopUp.SetActive(true);
            errorLog.text = ex.Message;
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            errorPopUp.SetActive(true);
            errorLog.text = ex.Message;
            Debug.LogException(ex);
        }
    }

    public async void SignInWithUsernamePasswordAsync()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        errorPopUp.SetActive(false);
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            await PlayerManager.instance.LoadAllPlayerData();

            SceneManager.LoadScene("MainScene");
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            errorPopUp.SetActive(true);
            errorLog.text = ex.Message;
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            errorPopUp.SetActive(true);
            errorLog.text = ex.Message;
            Debug.LogException(ex);
        }
    }

    // Setup authentication event handlers if desired
    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

}