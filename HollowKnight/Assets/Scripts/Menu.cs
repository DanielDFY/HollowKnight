using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void clickStartButton()
    {
        PlayerPrefs.SetString("Milestone", "Spawn");
        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
    }

    public void clickLoadButton()
    {
        SceneManager.LoadScene("LoadMenu");
    }

    public void clickQuitButton()
    {
        Application.Quit();
    }


}
