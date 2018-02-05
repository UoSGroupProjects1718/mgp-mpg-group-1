using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float movementSpeed;

    private void Start()
    {
        movementSpeed = 5f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, 1.5f, 0));
        }
    }
}
