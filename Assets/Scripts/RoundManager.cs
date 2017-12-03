using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {    
    public static float ROUND_TIME = 60f;

    public static RoundManager instance;
    public GameObject itemBreakPrefab;

    public GameObject depositeArea;
    public List<DraggableObject> collectedItems = new List<DraggableObject>();

    public Text timerText;
    public Text scoreText;
    public Text gameOverText;
    private float elapsedTime;
    private int totalScore;

    public bool gameOver = false;

	// Use this for initialization
	void Start () {
        instance = this;
        totalScore = 0;
        gameOverText.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
            return;
        //If not paused
        elapsedTime += Time.deltaTime;

        timerText.text = ""+Mathf.Ceil(ROUND_TIME - elapsedTime);
        if(elapsedTime > ROUND_TIME)
        {
            Debug.Log("GAME IS OVER!!!!");
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            //TODO play end sequence?  Go to another scene?
        }
	}

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score : " + totalScore;
    }
}
