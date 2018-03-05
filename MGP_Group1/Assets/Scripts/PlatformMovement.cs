using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool IsMoving;
    public float HorizontalTravel = 6f;

    private float movementSpeed;
    private bool movingRight = true;

    private float SpeedMult = 1f;
    private float PowerUpMult = 1f;
    public float PowerUpTimer 
    {
        get { return PowerUpMult; }
        set { PowerUpMult *= value; }
    }

    private void Start()
    {
        IsMoving = true;
        movementSpeed = Random.Range(3f, 6f);
    }

    private void Update() {
        //foreach (Touch touch in Input.touches) 
        //{
        //    if ((Time.timeScale != 0) && (touch.phase == TouchPhase.Began))
        if ((Time.timeScale != 0) && (Input.GetKeyDown(KeyCode.Space))) 
        {
            SpeedMult += 0.05f;
        }
        //}
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
            transform.Translate((movingRight ? new Vector3(1, 0) : new Vector3(-1, 0)) * movementSpeed * Time.deltaTime * SpeedMult * PowerUpMult);
    }
}