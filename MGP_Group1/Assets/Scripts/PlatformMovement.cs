using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool IsMoving;
    public float HorizontalTravel = 5f;

    private float movementSpeed;
    private bool movingRight = true;

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

    public GameObject Platform;
    public Sprite OldEra;
    public Sprite NewEra;
    private static bool Era = true; //True == OldEra, False == NewEra
    public static bool ChangeEra
    {
        get { return Era; }
        set { Era = value; }
    }
    public bool Entered = false;
    public bool hasEntered
    {
        get { return Entered; }
        set { Entered = value; }
    }

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
        Entered = false;
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
        if (Era)
        {
            Platform.GetComponent<SpriteRenderer>().sprite = OldEra;
        }
        else
        {
            Platform.GetComponent<SpriteRenderer>().sprite = NewEra;
        }
    }
}