using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public Image[] hp=new Image[5];
	public Image[] zerohp=new Image[5];
	public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	if (player != null){
		for(int i=1;i<=5;++i){
			if(player.health>=i){
				hp[i].color=new Color(hp[i].color[0],hp[i].color[1],hp[i].color[2],1);
				zerohp[i].color=new Color(zerohp[i].color[0],zerohp[i].color[1],zerohp[i].color[2],0);
			}
			else{
				hp[i].color=new Color(hp[i].color[0],hp[i].color[1],hp[i].color[2],0);
				zerohp[i].color=new Color(zerohp[i].color[0],zerohp[i].color[1],zerohp[i].color[2],1);
			}
		}
	}
    }
}
