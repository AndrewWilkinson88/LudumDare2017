using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
    public void OnPlayAgain()
    {
        SceneManager.LoadScene("HookedScene");
    }

    public void OnExitToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
