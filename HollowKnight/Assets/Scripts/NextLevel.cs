using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;     
        Text textTime = GameObject.Find("ShowTime").GetComponent<Text>();
        PlayerPrefs.SetString("time", textTime.text);
        
        // TODO：保存当前关卡，重玩的话重新加载
        PlayerPrefs.SetString("level", SceneManager.GetActiveScene().name);

        // PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        SceneManager.LoadScene("Victory");
    }
}
