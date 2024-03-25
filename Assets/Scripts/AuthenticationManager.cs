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
                OnSignedIn?.Invoke(playerInfo, name);


                //TODO MOVE TO OTHR FUNCTION
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
