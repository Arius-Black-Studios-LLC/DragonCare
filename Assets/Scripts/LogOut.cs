using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{

    public async void LogOutButton()
    {
        SceneManager.LoadScene(0);
    }
}
