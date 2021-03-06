﻿using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Character;

    private float movementSpeed;
    //private float timePassed;
    float SpeedMult = 1f;

    private void Start()
    {
        movementSpeed = 0.5f;
        //timePassed = Time.time;
    }

    private void Update()
    {
        float cameraDistance = transform.position.y - Character.transform.position.y;

        if (cameraDistance <= -2.75f)
        {
            SpeedMult = 10f;
        }
        else if (cameraDistance >= 4.68f)
        {
            SpeedMult = 0f;
        }
        else
        {
            SpeedMult = 1f;
        }

        if (Time.timeScale != 0)
        {
            // Every 2 seconds, increase camera movement speed by 0.125f
            /*if(Time.time - timePassed >= 4f)
            {
                movementSpeed += 0.125f;
                timePassed = Time.time;
            }*/

            transform.position += Vector3.up * movementSpeed * SpeedMult * Time.deltaTime;
        }
    }
}