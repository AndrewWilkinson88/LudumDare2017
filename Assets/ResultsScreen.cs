using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    public Text itemsCollectedLabel;
    public Text itemsCollected;
    public Text largestStackLabel;
    public Text largestStack;
    public Text totalScoreLabel;
    public Text totalScore;

    public GameObject stuffRoot;

    private int collectedCount = 0;
    private int totalItems;
    private int tallestStack;
    private int score;

    void Start()
    {
        itemsCollectedLabel.gameObject.SetActive(false);
        itemsCollected.gameObject.SetActive(false);
        largestStackLabel.gameObject.SetActive(false);
        largestStack.gameObject.SetActive(false);
        totalScoreLabel.gameObject.SetActive(false);
        totalScore.gameObject.SetActive(false);

        if (ScoreManager.instance == null)
        {
            totalItems = 0;
            tallestStack = 0;
            score = 0;
        }
        else
        {
            totalItems = ScoreManager.instance.getTotalNumber();
            tallestStack = ScoreManager.instance.getLargestStack();
            score = ScoreManager.instance.getScore();
        }

        StartCoroutine(SpawnItems());
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene("HookedScene");
    }

    public void OnExitToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(1.0f);

        itemsCollectedLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        itemsCollected.text = collectedCount + "/" + totalItems;
        itemsCollected.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (ScoreManager.instance != null)
        {
            foreach (GameObject obj in ScoreManager.instance.GetItems())
            {
                obj.GetComponentInChildren<DraggableObject>(true).breakThreashold = int.MaxValue;
                obj.transform.parent = stuffRoot.transform;
                obj.transform.position = stuffRoot.transform.position;
                obj.SetActive(true);
                collectedCount++;
                itemsCollected.text = collectedCount + "/" + totalItems;
                yield return new WaitForSeconds(0.1f);
            }
        }

        largestStackLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        largestStack.text = tallestStack.ToString();
        largestStack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        totalScoreLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        totalScore.text = score.ToString();
        totalScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
    }
}
