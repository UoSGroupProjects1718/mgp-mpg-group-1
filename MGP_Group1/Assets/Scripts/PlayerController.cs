﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool Player1Turn = false;

    private int P1Score = 0;
    public Text P1ScoreText;
    private int P2Score = 0;
    public Text P2ScoreText;

    public GameObject[] MovingPlatforms;

    public int currentPlatform = 0;

    public float YPosition = 0;

    private bool onPlatform = true;
    private int noPlatformFrameCount = 0;

    public int PooledAmount = 20; // PooledAmount = number of full length, no powerup platforms to pool
    private List<GameObject> Platforms;
    public List<GameObject> ActivePlatforms;

    public int ScorePenalty = 10;

    private float GameTimer = 60f;
    private int[] Winner = new int[3];
    private int RoundNumber = 0;
    private bool RoundEnded = false;

    public GameObject Camera;

    private void Start()
    {
        P1ScoreText.text = "Player 1 Score: " + P1Score;
        P2ScoreText.text = "Player 2 Score: " + P2Score;

        Platforms = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++) // PooledAmount of full length, no powerup platforms
        {
            GameObject obj = (GameObject)Instantiate(MovingPlatforms[0]);
            obj.SetActive(false);
            Platforms.Add(obj);
        }
        for (int i = 0; i < PooledAmount/2; i++) // Half as many half length, no powerup platforms
        {
            GameObject obj = (GameObject)Instantiate(MovingPlatforms[1]);
            obj.SetActive(false);
            Platforms.Add(obj);
        }
        for (int i = 2; i < MovingPlatforms.Length; i++) // 1 of every powerup platform
        {
            GameObject obj = (GameObject)Instantiate(MovingPlatforms[i]);
            obj.SetActive(false);
            Platforms.Add(obj);
        }

        ActivePlatforms = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            EnablePlatform();
        }
    }

    private void Update()
    {
        GameTimer -= Time.deltaTime;
        if (GameTimer <= 0 && !RoundEnded)
        {
            RoundEnded = true;
            RoundEnd();
        }
        if (!onPlatform)
        {
            noPlatformFrameCount++;
            if (noPlatformFrameCount >= 5)
            {
                if (currentPlatform > 9)
                {
                    ActivePlatforms[currentPlatform - 10].SetActive(true);
                }
                ActivePlatforms[ActivePlatforms.Count - 1].SetActive(false);
                ActivePlatforms.RemoveAt(ActivePlatforms.Count - 1);
                YPosition -= 1.5f;
                PlatformMovement.SpeedMultiplier = -0.05f;
                noPlatformFrameCount = 0;
                currentPlatform--;
                SetPlayerTurn();
                ActivePlatforms[currentPlatform].GetComponent<PlatformMovement>().IsMoving = true;
                transform.Translate(new Vector3(0, -1.5f, 0));
                onPlatform = true;
                if (Player1Turn)
                {
                    if (P1Score > ScorePenalty)
                    {
                        P1Score -= ScorePenalty;
                    }
                    else
                    {
                        P1Score = 0;
                    }
                }
                else if (!Player1Turn)
                {
                    if (P2Score > ScorePenalty)
                    {
                        P2Score -= ScorePenalty;
                    }
                    else
                    {
                        P2Score = 0;
                    }
                }
            }
        }
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(Input.touchCount - 1).position.x < Screen.width/2 == Player1Turn)
                    {
                        onPlatform = false;
                        transform.Translate(new Vector3(0, 1.5f, 0));
                        ActivePlatforms[currentPlatform].GetComponent<PlatformMovement>().IsMoving = false;
                        ++currentPlatform;
                        SetPlayerTurn();
                        noPlatformFrameCount = 0;
                        PlatformMovement.SpeedMultiplier = 0.05f; //Increases all platforms' speed by this amount
                        EnablePlatform();
                        if (currentPlatform > 9)
                        {
                            ActivePlatforms[currentPlatform - 10].SetActive(false);
                        }
                    }    
                }    
            }
            else if (Input.GetKeyDown(KeyCode.Space)) 
            {
                onPlatform = false;
                transform.Translate(new Vector3(0, 1.5f, 0));
                ActivePlatforms[currentPlatform].GetComponent<PlatformMovement>().IsMoving = false;
                ++currentPlatform;
                SetPlayerTurn();
                noPlatformFrameCount = 0;
                PlatformMovement.SpeedMultiplier = 0.05f; //Increases all platforms' speed by this amount
                EnablePlatform();
                if (currentPlatform > 9)
                {
                    ActivePlatforms[currentPlatform - 10].SetActive(false);
                }
                if (currentPlatform > 11)
                {
                    ActivePlatforms[currentPlatform - 12] = null;
                }
            }
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
                ActivePlatforms[currentPlatform + i].GetComponent<PlatformMovement>().PowerUpTimer = 2f;
            }
        }
        P1ScoreText.text = "Player 1 Score: " + P1Score;
        P2ScoreText.text = "Player 2 Score: " + P2Score;
    }

    private void SetPlayerTurn()
    {
        Player1Turn = !Player1Turn;
    }

    private void EnablePlatform()
    {
        int i = Random.Range(0, Platforms.Count);
        if (!Platforms[i].activeInHierarchy)
        {
            Platforms[i].transform.position = new Vector3(Random.Range(-2.5f, 2.5f), YPosition, 0);
            Platforms[i].SetActive(true);
            YPosition += 1.5f;
            ActivePlatforms.Add(Platforms[i]);
        }
        else EnablePlatform();
    }

    private void RoundEnd()
    {
        Time.timeScale = 0;
        if (P1Score > P2Score)
        {
            Winner[RoundNumber] = 1;
        }
        else if (P2Score > P1Score)
        {
            Winner[RoundNumber] = -1;
        }
        else
        {
            Winner[RoundNumber] = 0;
        }
        if (RoundNumber == 2)
        {
            int total = 0;
            for (int i = 0; i < Winner.Length; i++)
            {
                total += Winner[i];
            }
            if (total > 0) // Positive = P1 won more than P2
            {
                Debug.Log("Player 1 Wins");
            }
            else if (total < 0) // Negative = P2 won more than P1
            {
                Debug.Log("Player 2 Wins");
            }
            else // 0 = equal win/loss
            {
                Debug.Log("Draw");
            }
            //Enable play again/main menu buttons
        }
        else
        {
            RoundNumber++;
            gameObject.transform.SetPositionAndRotation(new Vector3(0, -1.5f), new Quaternion(0, 0, 0, 0));
            Camera.transform.SetPositionAndRotation(new Vector3(0, 0, -10), new Quaternion(0, 0, 0, 0));
            YPosition = 0;
            currentPlatform = 0;
            P1Score = 0;
            P2Score = 0;
            P1ScoreText.text = "Player 1 Score: " + P1Score;
            P2ScoreText.text = "Player 2 Score: " + P2Score;
            onPlatform = true;
            noPlatformFrameCount = 0;
            for (int i = 0; i < Platforms.Count; i++)
            {
                Platforms[i].SetActive(false);
            }
            while (ActivePlatforms.Count > 0)
            {
                ActivePlatforms.RemoveAt(0);
            }
            for (int i = 0; i < 10; i++)
            {
                EnablePlatform();
            }
            GameTimer = 60f;
            RoundEnded = false;
            Time.timeScale = 1;
        }
    }
}