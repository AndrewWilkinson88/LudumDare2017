﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    public static float ROUND_TIME = 120f;

    public static RoundManager instance;
    public GameObject itemBreakPrefab;

    public GameObject depositeArea;
    public GameObject bag;
    public List<DraggableObject> collectedItems = new List<DraggableObject>();

    public Text timerText;
    public Text scoreText;
    public Text curLoot;
    public Image gameOverImage;
    private float elapsedTime;
    private int totalScore;

    public bool gameOver = false;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        totalScore = 0;
        gameOverImage.gameObject.SetActive(false);
        ScoreManager.instance.ResetGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver)
            return;

        //If not paused
        elapsedTime += Time.deltaTime;

        timerText.text = "" + Mathf.Ceil(ROUND_TIME - elapsedTime);
        if (ROUND_TIME - elapsedTime < 10)
            timerText.color = Color.red;

        if(elapsedTime > ROUND_TIME)
        {
            //Debug.Log("GAME IS OVER!!!!");
            gameOver = true;
            gameOverImage.gameObject.SetActive(true);
            ScoreManager.instance.SetItemsCollected(collectedItems);
            StartCoroutine(WaitForSceneEnd());
        }
	}

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score : " + totalScore;
        ScoreManager.instance.UpdateScore(totalScore);
        float si = 0.001f * score;
        bag.transform.localScale = RoundManager.instance.bag.transform.localScale  + new Vector3(si, si, si);
    }
    
    IEnumerator WaitForSceneEnd()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayWhistle();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Results");
    }

    public void SetCurLootScore(int curValue, int multiplier)
    {
        if (curValue == 0)
            curLoot.text = "";
        else
            curLoot.text = "value: " + curValue +
                           "\nmultiplier: " + multiplier +
                           "\ntotal: " + (curValue * multiplier);
    }
}
