using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(new Vector3(0, 1.5f, 0));
            }
        }
    }
}
