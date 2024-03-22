using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication.PlayerAccounts;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AuthenticationManager : MonoBehaviour
{

    //public TMP_InputField usernameInput;
    //public TMP_InputField passwordInput;
    //public TMP_Text errorLog;
    //public GameObject errorPopUp;
    //async void Awake()
    //{
    //    errorPopUp.SetActive(false);
    //    try
    //    {
    //        await UnityServices.InitializeAsync();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogException(e);
    //    }
    //}



    //public async void SignUpWithUsernamePassword()
    //{
    //    string username = usernameInput.text;
    //    string password = passwordInput.text;
    //    errorPopUp.SetActive(false);
    //    try
    //    {
    //        await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
    //        await PlayerManager.instance.SaveAllPlayerData();
    //        SceneManager.LoadScene("MainScene");
    //        Debug.Log("SignUp is successful.");

    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        // Compare error code to AuthenticationErrorCodes
    //        // Notify the player with the proper error message
    //        errorPopUp.SetActive(true);
    //        errorLog.text = ex.Message;
    //        Debug.LogException(ex);
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        // Compare error code to CommonErrorCodes
    //        // Notify the player with the proper error message
    //        errorPopUp.SetActive(true);
    //        errorLog.text = ex.Message;
    //        Debug.LogException(ex);
    //    }
    //}

    //public async void SignInWithUsernamePasswordAsync()
    //{
    //    string username = usernameInput.text;
    //    string password = passwordInput.text;
    //    errorPopUp.SetActive(false);
    //    try
    //    {
    //        await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
    //        await PlayerManager.instance.LoadAllPlayerData();

    //        SceneManager.LoadScene("MainScene");
    //        Debug.Log("SignIn is successful.");
    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        // Compare error code to AuthenticationErrorCodes
    //        // Notify the player with the proper error message
    //        errorPopUp.SetActive(true);
    //        errorLog.text = ex.Message;
    //        Debug.LogException(ex);
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        // Compare error code to CommonErrorCodes
    //        // Notify the player with the proper error message
    //        errorPopUp.SetActive(true);
    //        errorLog.text = ex.Message;
    //        Debug.LogException(ex);
    //    }
    //}

    //// Setup authentication event handlers if desired
    //void SetupEvents()
    //{
    //    AuthenticationService.Instance.SignedIn += () => {
    //        // Shows how to get a playerID
    //        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

    //        // Shows how to get an access token
    //        Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

    //    };

    //    AuthenticationService.Instance.SignInFailed += (err) => {
    //        Debug.LogError(err);
    //    };

    //    AuthenticationService.Instance.SignedOut += () => {
    //        Debug.Log("Player signed out.");
    //    };

    //    AuthenticationService.Instance.Expired += () =>
    //    {
    //        Debug.Log("Player session could not be refreshed and expired.");
    //    };
    //}

    public event Action<PlayerInfo, string> OnSignedIn;
    public PlayerInfo playerInfo;



    public async void LoginButtonPressed()
    {
       await InitSignIn();
    }

    async void Awake()
    {

        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        PlayerAccountService.Instance.SignedIn += SignedIn;
    }

    private async void SignedIn()
    {
        try
        {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            await SignInWithUnityAsync(accessToken);

            Debug.Log("SignIn is successful.");
        }
        catch (Exception e)
        {

        }
    }

    public async Task InitSignIn()
    {
        await PlayerAccountService.Instance.StartSignInAsync();
    }

    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");

            playerInfo = AuthenticationService.Instance.PlayerInfo;
            var name = await AuthenticationService.Instance.GetPlayerNameAsync();
            OnSignedIn?.Invoke(playerInfo,name);

            await PlayerManager.instance.LoadAllPlayerData();

            SceneManager.LoadScene("MainScene");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }


    private void OnDestroy()
    {
        PlayerAccountService.Instance.SignedIn -= SignedIn;
    }
}