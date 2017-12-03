using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject helpPanel;

    public List<GameObject> helpPages;
    public Text helpPageCount;

    public Button leftButton;
    public Button rightButton;

    int currentPage;

    // Use this for initialization
    void Start()
    {
        currentPage = 0;
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    public void OnStartButton()
    {
        //Start Game
        SceneManager.LoadScene("HookedScene");
    }

    public void OnHelpButton()
    {
        mainPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnRightButton()
    {
        if( currentPage < helpPages.Count-1)
        {
            currentPage++;
        }
        updateHelpPage();
    }

    public void OnLeftButton()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
        updateHelpPage();
    }

    void updateHelpPage()
    {
        helpPageCount.text = (currentPage + 1).ToString() + " / " + helpPages.Count;

        foreach( GameObject go in helpPages)
        {
            go.SetActive(false);
        }

        helpPages[currentPage].SetActive(true);

        leftButton.enabled = (currentPage != 0);
        rightButton.enabled = (currentPage != helpPages.Count-1);
    }
}
