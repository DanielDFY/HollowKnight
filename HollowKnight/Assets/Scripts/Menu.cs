using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    string start="Spawn";
    int level=1;
    public Text startAtButtonText;
    public void clickStartButton()
    {
        PlayerPrefs.SetString("Milestone", "Spawn");
        clickLoadButton();
    }

    public void clickLoadButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
    }

    public void clickQuitButton()
    {
        Application.Quit();
    }

    public void clickStartAtButton()
    {
        PlayerPrefs.SetString("Milestone", start);
        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
    }

    public void clickDecButton()
    {
        if(level>1){
            --level;
            start="Level"+level;
            startAtButtonText.text="START AT LEVEL"+level;
        }
    }

    public void clickIncButton()
    {
        if(level<stringToIntInPostive(PlayerPrefs.GetString("Milestone").Remove(0,5))){
            ++level;
            start="Level"+level;
            startAtButtonText.text="START AT LEVEL"+level;
        }
    }

    public int stringToIntInPostive(string num)
    {
        int temp=0;
        char[] temp2;
        temp2=num.ToCharArray(0,num.Length);
        for(int i=num.Length-1;i>=0;--i)
        {
            temp*=10;
            switch(temp2[i]){
                case '0':
                    temp+=0;
                    break;
                case '1':
                    temp+=1;
                    break;
                case '2':
                    temp+=2;
                    break;
                case '3':
                    temp+=3;
                    break;
                case '4':
                    temp+=4;
                    break;
                case '5':
                    temp+=5;
                    break;
                case '6':
                    temp+=6;
                    break;
                case '7':
                    temp+=7;
                    break;
                case '8':
                    temp+=8;
                    break;
                case '9':
                    temp+=9;
                    break;
            }
        }
        return temp;
    }

}
