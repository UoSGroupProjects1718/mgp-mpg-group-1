using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //private int PlayerNum = 0;
    private bool Player1Turn = true;

    private int P1Score = 0;
    public Text P1ScoreText;
    private int P2Score = 0;
    public Text P2ScoreText;

    public GameObject[] MovingPlatforms;

    public const int numberOfPlatforms = 100;
    private GameObject[] Platforms;

    private int currentPlatform = 0;

    public float InitialYPosition = 0;

    private bool onPlatform = true;
    private int noPlatformFrameCount = 0;

    private void Start()
    {
        SetPlayerTurn();
        P1ScoreText.text = "Player 1 Score: " + P1Score;
        P2ScoreText.text = "Player 2 Score: " + P2Score;

        GameObject PlatformsParent = new GameObject("Ice Platforms");

        Platforms = new GameObject[numberOfPlatforms];

        for (int i = 0; i < numberOfPlatforms; ++i)
        {
            Platforms[i] = Instantiate(MovingPlatforms[Random.Range(0, MovingPlatforms.Length)],
                                       new Vector3(Random.Range(-2.5f, 2.5f), InitialYPosition, 0),
                                       Quaternion.Euler(0, 0, Random.Range(-1.5f, 1.5f)),
                                       PlatformsParent.transform);

            InitialYPosition += 1.5f;
        }
    }

    private void Update()
    {
        if (!onPlatform)
        {
            noPlatformFrameCount++;
            if (noPlatformFrameCount >= 5)
            {
                Time.timeScale = 0;
            }
        }
        if ((Time.timeScale != 0) && (((Input.touchCount > 0) && (Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began) && (Input.GetTouch(Input.touchCount - 1).position.x < 0 == Player1Turn)) || (Input.GetKeyDown(KeyCode.Space))))
        {
            onPlatform = false;
            transform.Translate(new Vector3(0, 1.5f, 0));
            Platforms[currentPlatform].GetComponent<PlatformMovement>().IsMoving = false;
            ++currentPlatform;
            SetPlayerTurn();
            noPlatformFrameCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            onPlatform = true;
        }
        else if (other.gameObject.tag == "Point0") 
        {
            //if (PlayerNum == 1)
            if (Player1Turn)
            {
                P1Score += 5;
            }
            //else if (PlayerNum == 2) 
            else if (!Player1Turn)
            {
                P2Score += 5;
            }
        }
        else if (other.gameObject.tag == "Point1") 
        {
            //if (PlayerNum == 1)
            if (Player1Turn)
            {
                P1Score += 3;
            }
            //else if (PlayerNum == 2) 
            else if (!Player1Turn)
            {
                P2Score += 3;
            }
        }
        else if (other.gameObject.tag == "Point2") 
        {
            //if (PlayerNum == 1)
            if (Player1Turn)
            {
                P1Score += 1;
            }
            //else if (PlayerNum == 2) 
            else if (!Player1Turn)
            {
                P2Score += 1;
            }
        }
        else if (other.gameObject.tag == "PowerUp0") 
        {
            for(int i = 0; i < 3; i++) 
            {
                Platforms[currentPlatform + i].GetComponent<PlatformMovement>().PowerUpTimer = 2f;
            }
        }
        P1ScoreText.text = "Player 1 Score: " + P1Score;
        P2ScoreText.text = "Player 2 Score: " + P2Score;
    }

    private void SetPlayerTurn()
    {
        //PlayerNum = (currentPlatform + 1) % 2;
        //PlayerNum++;
        Player1Turn = !Player1Turn;
    }
}