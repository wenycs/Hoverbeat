using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject transition;

    void Start()
    {
        StartCoroutine(LoadFinish(transition));
    }

    IEnumerator LoadFinish(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
        Time.timeScale = 0;
    }

        public void UnfreezeGame()
    {
        Time.timeScale = 1;
    }

    public void returntoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
