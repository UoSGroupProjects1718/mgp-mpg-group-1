using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLogic : MonoBehaviour
{
    public static int Score;

    private void Start()
    {
        Score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ++Score;
            this.gameObject.SetActive(false);
        }
    }
}
