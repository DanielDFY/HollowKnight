using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void clickStartButton()
    {
        PlayerPrefs.SetString("Level", "Spawn");
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
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
