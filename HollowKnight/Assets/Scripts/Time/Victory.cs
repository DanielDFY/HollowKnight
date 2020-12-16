using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Victory : MonoBehaviour
{
    private string timeSpent;
    public Text time;
    public Text[] ranking;
    private string defaultTime = "99:59:59.999";
    private string[] times;

    public void Awake()
    {
        
        times = new string[6];
        if (PlayerPrefs.GetString("times").Length == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                times[i] = defaultTime;
            }
            // PlayerPrefs.SetString("times", string.Join(",", times));
        }
        else
        {
            PlayerPrefs.GetString("times").Split(',').CopyTo(times, 0);
        }

        // 1. 得到当前的时间值
        timeSpent = PlayerPrefs.GetString("time");
        
        // 2. 获取时间排行榜(前五位)
        times[5] = timeSpent;
        Array.Sort(times);

        // 3. 记录时间排行榜
        PlayerPrefs.SetString("times", string.Join(",", times));

        // 4. 设置时间排行榜界面
        time.text = timeSpent;
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking[i].text = times[i];
        }

        // 4. 修改下一关设置
        PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        // SceneManager.LoadScene(GlobalController.Instance.nextScene);

        // TODO：记得删掉
        PlayerPrefs.DeleteKey("time");
        PlayerPrefs.DeleteKey("times");
    }
}
