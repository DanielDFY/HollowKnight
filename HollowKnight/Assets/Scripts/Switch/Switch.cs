﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite triggered;
    public GameObject obstacle;
    public GameObject trap;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void turnOn()
    {
        _spriteRenderer.sprite = triggered;

        if(obstacle != null)
        {
            obstacle.GetComponent<Obstacle>().destroy();
        }

        if(trap != null)
        {
            trap.GetComponent<Trap>().trigger();
        }

        gameObject.layer = LayerMask.NameToLayer("Decoration");
    }
}
