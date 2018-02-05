using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject IcePlatform;

    private const int numberOfPlatforms = 100;
    private GameObject[] icePlatforms;

    private int currentPlatform;

    private void Start()
    {
        currentPlatform = 0;

        GameObject icePlatformsParent = new GameObject("Ice Platforms");

        icePlatforms = new GameObject[numberOfPlatforms];

        float yPosition = 0;

        for(int i = 0; i < numberOfPlatforms; ++i)
        {
            icePlatforms[i] = Instantiate(IcePlatform,
                                          new Vector3(Random.Range(-2.5f, 2.5f), yPosition, 0),
                                          Quaternion.Euler(0, 0, 90 + Random.Range(-1.5f, 1.5f)),
                                          icePlatformsParent.transform);

            yPosition += 1.5f;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            icePlatforms[currentPlatform].GetComponent<IcePlatformMovement>().IsMoving = false;
            ++currentPlatform;
        }
    }
}
