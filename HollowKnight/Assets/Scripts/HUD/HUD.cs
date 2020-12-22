using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject pauseMenu;

    public float shadeDelay;

    private void Start()
    {
        // 画面渐亮动画
        StartCoroutine(shadeCoroutine());
    }
    private IEnumerator shadeCoroutine()
    {
        Image shadePanalImage = GameObject.Find("ShadePanel").GetComponent<Image>();

        while (shadeDelay > 0)
        {
            shadeDelay -= Time.deltaTime;

            if (shadePanalImage.color.a > 0)
            {
                Color newColor = shadePanalImage.color;
                newColor.a -= Time.deltaTime / shadeDelay;
                shadePanalImage.color = newColor;
                yield return null;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
        }
    }

    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
