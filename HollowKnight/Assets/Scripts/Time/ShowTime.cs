using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour
{
    int hour;
    int minute;
    int second;
    int millisecond;
    // 已经花费的时间 
    float timeSpent = 0.0f;
    // 显示时间区域的文本 
    private Text textTime;

    void Start()
    {
        textTime = GetComponent<Text>();
    }

    void Update()
    {
        timeSpent += Time.deltaTime;

        hour = (int)timeSpent / 3600;
        minute = ((int)timeSpent - hour * 3600) / 60;
        second = (int)timeSpent - hour * 3600 - minute * 60;
        millisecond = (int)((timeSpent - (int)timeSpent) * 1000);

        textTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
    }

    public Text getTextTime()
    {
        return textTime;
    }
}