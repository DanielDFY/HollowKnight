using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{
    public float movingSpeed;
    public float movingLimit;
    public float movingOffset;
    public int movingDirection = 0; // 0 for x, 1 for y

    private Vector3 basePosition;

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        basePosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffset = movingOffset + Time.deltaTime * movingSpeed;
        if (Math.Abs(newOffset) >= movingLimit)
        {
            movingSpeed = -movingSpeed;

            if (movingDirection == 0)
            {
                basePosition.x += movingOffset;
            }
            else
            {
                basePosition.y += movingOffset;
            }
            movingOffset = 0;
        }
        else
        {
            movingOffset = newOffset;
        }

        Vector3 newPosition = basePosition;
        if(movingDirection == 0)
        {
            newPosition.x += movingOffset;
        }
        else
        {
            newPosition.y += movingOffset;
        }
        _transform.position = newPosition;
    }

    public override void trigger()
    {
        
    }
}
