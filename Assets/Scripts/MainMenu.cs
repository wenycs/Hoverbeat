using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadDelay());
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
