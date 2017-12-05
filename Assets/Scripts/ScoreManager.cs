using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private List<GameObject> collectedItems = new List<GameObject>();
    private int score;
    private int itemsCollected;
    private int totalItems;
    private int largestStack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void ResetGame()
    {
        largestStack = 0;
    }

    public void SetTotalItems(int count)
    {
        totalItems = count;
    }

    public void UpdateScore(int score)
    {
        this.score = score;
    }

    public void SetItemsCollected(List<DraggableObject> items)
    {
        itemsCollected = items.Count;
        collectedItems = new List<GameObject>();
        foreach (DraggableObject dragItem in items)
        {
            GameObject newObj = Instantiate(dragItem.gameObject);
            collectedItems.Add(newObj);
            DontDestroyOnLoad(newObj);
            newObj.transform.SetParent(transform);
        }
    }

    public void SetLargestStack(int stack)
    {
        if( stack > largestStack)
        {
            largestStack = stack;
        }
    }

    public List<GameObject> GetItems()
    {
        return collectedItems;
    }

    public int getScore()
    {
        return score;
    }

    public int getNumberCollected()
    {
        return itemsCollected;
    }

    public int getTotalNumber()
    {
        return totalItems;
    }

    public int getLargestStack()
    {
        return largestStack;
    }
}
