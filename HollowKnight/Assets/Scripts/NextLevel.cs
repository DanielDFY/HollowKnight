using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // TODO 保存通关时间，显示通关时间排行榜，进入下一关需要按钮
    // 网络部分：将时间上传到服务器，并通过服务器获取
    // 没有用户名，可以只显示超越了 xx% 的用户
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;

        PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        SceneManager.LoadScene(GlobalController.Instance.nextScene);
    }
}
