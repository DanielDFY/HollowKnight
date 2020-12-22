using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public float shadeDelay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;

        GameObject showTimeObj = GameObject.Find("ShowTime");
        if (showTimeObj != null)
        {
            Text textTime = showTimeObj.GetComponent<Text>();
            PlayerPrefs.SetString("time", textTime.text);
        }
        
        // TODO：保存当前关卡，重玩的话重新加载
        PlayerPrefs.SetString("level", SceneManager.GetActiveScene().name);

        // 画面渐暗动画, 结束后切换场景
        StartCoroutine(shadeCoroutine());
    }

    private IEnumerator shadeCoroutine()
    {
        Image shadePanalImage = GameObject.Find("ShadePanel").GetComponent<Image>();

        while (shadeDelay > 0)
        {
            shadeDelay -= Time.deltaTime;

            if (shadePanalImage.color.a < 1)
            {
                Color newColor = shadePanalImage.color;
                newColor.a += Time.deltaTime / shadeDelay;
                shadePanalImage.color = newColor;
                yield return null;
            }
        }

        // PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        if (SceneManager.GetActiveScene().name == "Spawn")
            SceneManager.LoadScene(GlobalController.Instance.nextScene);
        else
            SceneManager.LoadScene("Victory");

    }
}
