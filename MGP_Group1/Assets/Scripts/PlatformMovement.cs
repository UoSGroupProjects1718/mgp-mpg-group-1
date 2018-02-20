﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool IsMoving;
    public float HorizontalTravel = 6f;

    private float movementSpeed;
    private bool movingRight = true;

    private void Start()
    {
        IsMoving = true;
        movementSpeed = Random.Range(3f, 6f);
    }

    private void Update()
    {
        if (IsMoving)
        {
            if (Mathf.Abs(transform.position.x) >= HorizontalTravel)
                movingRight = !movingRight;

            transform.Translate((movingRight ? new Vector3(1, 0) : new Vector3(-1, 0)) * movementSpeed * Time.deltaTime);
        }
    }
}