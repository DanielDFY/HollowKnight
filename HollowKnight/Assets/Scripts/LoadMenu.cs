using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    string start="Level1";
    static int level;
    public Text[] levelText=new Text[3];
    void Awake ()
    {
        level=stringToIntInPostive(PlayerPrefs.GetString("Milestone").Remove(0,5));
        for (int i=1;i<=3;++i){
            if (level<i) {
                levelText[i-1].color=new Color(1,0,0,1);
            }
        }
    }

    public void clickLevelButton1()
    {
        start="Level1";
        Load();
    }

    public void clickLevelButton2()
    {
        start="Level2";
        Load();
    }

    public void clickLevelButton3()
    {
        start="Level3";
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene(start);
    }

    public void clickReturnButton()
    {
        SceneManager.LoadScene("Menu");
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
