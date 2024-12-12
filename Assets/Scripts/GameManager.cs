using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying, panelClosed, stopPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText, countdownText;

    [SerializeField] Button button;

    void Start()
    {
        instance = this;

        scoreText.text = "Score : 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    void Update()
    {
        button.GetComponent<Button>().onClick.AddListener(StartGame);
        theBS.hasStarted = true;

        if(stopPlaying)
        {
            ShowResults();
            stopPlaying = false;
        }
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
    
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        scoreText.text = "Score : " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMiss()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }

    void StartGame()
    {
        panelClosed = true;
        if(!startPlaying)
        {
            if(panelClosed)
            {
                StartCoroutine(Countdown(3));
            }
        }
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            yield return new WaitForSeconds(1);
            countdownText.text = count.ToString();
            
            count--;
        }

        countdownText.enabled = false;
        startPlaying = true;

        theMusic.Play();
        yield return new WaitForSeconds(35);
        stopPlaying = true;
    }

    void ShowResults()
    {
        resultsScreen.SetActive(true);

        normalsText.text = normalHits.ToString();
        goodsText.text = goodHits.ToString();
        perfectsText.text = perfectHits.ToString();
        missesText.text = missedHits.ToString();

        float totalHit = (normalHits * 0.33f) + (goodHits * 0.66f) + perfectHits;
        float percentHit = (totalHit / totalNotes) * 100f;

        percentHitText.text = percentHit.ToString("F1") + "%";

        if(percentHit == 100)
        {
            rankText.text = "SS";
        }
        else if(percentHit > 95)
        {
            rankText.text = "S";
        }
        else if(percentHit > 85)
        {
            rankText.text = "A";
        }
        else if(percentHit > 70)
        {
            rankText.text = "B";
        }
        else if(percentHit > 55)
        {
            rankText.text = "C";
        }
        else if(percentHit > 40)
        {
            rankText.text = "D";
        }
        else
        {
            rankText.text = "F";
        }

        finalScoreText.text = currentScore.ToString();
        Time.timeScale = 0;
    }
}

