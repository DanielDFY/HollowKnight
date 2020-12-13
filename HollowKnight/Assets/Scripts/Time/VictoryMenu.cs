using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    public void clickMemuButton()
    {
        PlayerPrefs.SetString("Milestone", "Spawn");
        // clickLoadButton();
        // TODO 主菜单
        SceneManager.LoadScene("Menu");
    }

    public void clickRetryButton()
    {
        // TODO：重新加载当前关卡
        SceneManager.LoadScene(PlayerPrefs.GetString("level"));
    }

    public void clickNextButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
    }
}
