using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
    public Dictionary<string, bool> testDic = new Dictionary<string, bool>();

    public void OnPlayAgain()
    {
        SceneManager.LoadScene("HookedScene");
    }

    public void OnExitToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
