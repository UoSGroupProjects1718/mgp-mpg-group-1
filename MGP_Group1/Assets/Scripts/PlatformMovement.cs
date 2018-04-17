using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool IsMoving;
    public float HorizontalTravel = 5f;

    private float movementSpeed;
    public float moveSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }
    private bool movingRight = true;
    public bool moveRight
    {
        get { return movingRight; }
        set { movingRight = value; }
    }

    private static float SpeedMult = 1f;
    public static float SpeedMultiplier
    {
        get { return SpeedMult; }
        set { SpeedMult = value; }
    }
    private float PowerUpMult = 1f;
    public float SpeedIncrease 
    {
        get { return PowerUpMult; }
        set { PowerUpMult *= value; }
    }
    private float MissedPlatformMult = 1f;
    public float MissMult
    {
        get { return MissedPlatformMult; }
        set { MissedPlatformMult -= value; }
    }

    public GameObject[] PickUps;

    private void OnEnable()
    {
        IsMoving = true;
        movementSpeed = Random.Range(3f, 6f);
        for (int i = 0; i < PickUps.Length; i++)
        {
            PickUps[i].SetActive(true);
        }
        PowerUpMult = 1f;
        MissedPlatformMult = 1f;
    }

    private void Update()
    {
        if (IsMoving && movingRight)
        {
            if (transform.position.x >= HorizontalTravel)
                movingRight = false;
        }
        else if (IsMoving && !movingRight)
        {
            if (transform.position.x <= -HorizontalTravel)
                movingRight = true;
        }
        if (IsMoving)
            transform.Translate((movingRight ? new Vector3(1, 0) : new Vector3(-1, 0)) * movementSpeed * Time.deltaTime * SpeedMult * PowerUpMult * MissedPlatformMult);
    }
}