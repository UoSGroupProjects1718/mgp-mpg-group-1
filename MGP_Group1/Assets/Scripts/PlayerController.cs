using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score += value; }
    }

    public GameObject MovingPlatform;

    private const int numberOfPlatforms = 100;
    private GameObject[] Platforms;

    private int currentPlatform;

    public float InitialYPosition = 0;

    private void Start()
    {
        currentPlatform = 0;

        GameObject PlatformsParent = new GameObject("Ice Platforms");

        Platforms = new GameObject[numberOfPlatforms];


        for (int i = 0; i < numberOfPlatforms; ++i)
        {
            Platforms[i] = Instantiate(MovingPlatform,
                                          new Vector3(Random.Range(-2.5f, 2.5f), InitialYPosition, 0),
                                          Quaternion.Euler(0, 0, Random.Range(-1.5f, 1.5f)),
                                          PlatformsParent.transform);

            InitialYPosition += 1.5f;
        }
    }

    private void Update()
    {
        if ((Time.timeScale != 0) && (Input.GetKeyDown(KeyCode.Space)))
        {
            transform.Translate(new Vector3(0, 1.5f, 0));
            Platforms[currentPlatform].GetComponent<PlatformMovement>().IsMoving = false;
            ++currentPlatform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Point0")
        {
            score += 5;
        }
        else if (other.gameObject.tag == "Point1")
        {
            score += 3;
        }
        else if (other.gameObject.tag == "Point2")
        {
            score += 1;
        }
        Debug.Log(score.ToString());
    }
}