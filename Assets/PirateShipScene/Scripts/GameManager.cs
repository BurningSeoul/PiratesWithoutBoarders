using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameState {MENU, INTRO, GAMEPLAY, GAMEOVER, HIGHSCORE };
    private GameState currentState = GameState.MENU;

    public GameObject[] introObjects;
    private bool gamePlayReady = false;
    private float currentScore = 0;
    private float scoreMultiplier = 1f;
    public Text scoreUI;
    public Text multiUI;
    public int winningScore;
    public Text[] highScoreNums;
    public Text[] highScoreNames;
    private float[] highScores;
    private string[] names;
    private string tempName;
    public GameObject highScorePanel;
    public GameObject inputF;
    public InputField input;
    public GameObject controls;
    private bool startTimer = false;
    private float time = 4;
    public GameObject highScoreText;
    public GameObject yourScoreText;
    private bool b_checkScore = false;
    private float f_tempScore;
    private string f_tempName;
    private float defaultScore;

    private float ballsRemaining = 30;
    public Text shotsUI;

    public GameObject EndScreenPanel;
    public Text EndScreenScore;
	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        defaultScore = 200;
        highScores = new float[10];
        names = new string[10];
        b_checkScore = false;
        time = 4;

        if (PlayerPrefs.HasKey("HighScore" + 1))
        {
            for (int i = 0; i < 10; i++)
            {
                highScores[i] = PlayerPrefs.GetFloat("HighScore" + i);
                PlayerPrefs.SetFloat("HighScore" + i, highScores[i]);
                highScoreNums[i].text = highScores[i].ToString();
                //PlayerPrefs.Save();
                Debug.Log(highScores[i]);
            }
        }

        else
        {
            for (int i = 0; i < 10; i++)
            {
                defaultScore -= 20;
                //PlayerPrefs.DeleteAll();
                PlayerPrefs.SetFloat("HighScore" + i, defaultScore);
                // PlayerPrefs.Save();
                highScores[i] = PlayerPrefs.GetFloat("HighScore" + i);
                highScoreNums[i].text = highScores[i].ToString();
                //PlayerPrefs.Save();
            }
        }

        if (PlayerPrefs.HasKey("Names1" + 1))
        {
            for (int i = 0; i < 10; i++)
            {
                names[i] = PlayerPrefs.GetString("Names1" + i);
                highScoreNames[i].text = names[i];
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                //PlayerPrefs.SetString("Names1" + i, "AAA");
                PlayerPrefs.SetString("Names1" + i, "AAA");
                names[i] = PlayerPrefs.GetString("Names1" + i);
                PlayerPrefs.Save();
                highScoreNames[i].text = names[i];
            }
        }

        PlayerPrefs.Save();

        foreach (GameObject go in introObjects)
        {
            go.SetActive(false);
        }
        startTimer = false;
        EndScreenPanel.SetActive(false);
        highScorePanel.SetActive(false);
        highScoreText.SetActive(false);
        yourScoreText.SetActive(false);
        inputF.SetActive(false);

    }

    // Update is called once per frame
    void Update() {
        switch (currentState)
        {
            case GameState.MENU:
                controls.SetActive(true);
                break;
            case GameState.INTRO:
                controls.SetActive(false);
                if (gamePlayReady)
                {
                    foreach (GameObject go in introObjects)
                    {
                        go.SetActive(true);
                    }
                    currentState = GameState.GAMEPLAY;
                }
                break;
            case GameState.GAMEPLAY:
                if(ballsRemaining <= 0)
                {
                    currentState = GameState.HIGHSCORE;
                    if (!b_checkScore)
                    {
                        CheckHighScores();
                        b_checkScore = true;
                    }
                    
                }
                break;
            case GameState.GAMEOVER:
                foreach (GameObject go in introObjects)
                {
                    go.SetActive(false);
                }

                highScorePanel.SetActive(false);
                EndScreenPanel.SetActive(true);
                EndScreenScore.text = currentScore.ToString();
                break;
            case GameState.HIGHSCORE:
                highScorePanel.SetActive(true);

                if (startTimer)
                { 
                    time -= Time.deltaTime;

                    if(time <= 0)
                        currentState = GameState.GAMEOVER;  
                }
                break;
            default:
                return;
        }
	}

    private void LateUpdate()
    {
        if (currentScore < 0.0f)
            currentScore = 0.0f;

        scoreUI.text = currentScore.ToString();
        shotsUI.text = ballsRemaining.ToString();
        multiUI.text = scoreMultiplier.ToString();
    }

    public void SubtractBallsRemaining()
    {
        if(currentState == GameState.GAMEPLAY)
        {
            ballsRemaining -= 1;
        }
            
    }
    public void AdjustScore(float score)
    {
        if (currentState == GameState.GAMEPLAY)
        {
            if (score > 0)
                currentScore += score * scoreMultiplier;
            else
                currentScore += score;
        }
            

        if(currentScore >= winningScore)
            currentState = GameState.GAMEOVER;
    }

    public void IncreaseScoreMultiplier() { scoreMultiplier += 0.5f; }
    public void ResetScoreMultiplier() { scoreMultiplier = 1.0f; }

    public void UpdateHighScore()
    {
        for (int i = 0; i < 10; i++)
        {
            if (f_tempScore > highScores[i])
            {
                highScores[i] = currentScore;
                highScoreNums[i].text = highScores[i].ToString();

                names[i] = tempName;
                highScoreNames[i].text = names[i];
                break;
                //Debug.Log(highScores[i]);
                //Debug.Log(names[i]);
                //Debug.Log(tempN);
            }
            else
            {
                //highScoreNums[i].text = highScores[i].ToString();
                //highScoreNames[i].text = names[i];
            }
        }

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, highScores[i]);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("Names1" + i, names[i]);
            PlayerPrefs.Save();
        }

        startTimer = true;
        //PlayerPrefs.Save();
    }

    private void CheckHighScores()
    {
        yourScoreText.GetComponent<Text>().text = currentScore.ToString();
        highScoreText.SetActive(true);
        yourScoreText.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            if (currentScore >= highScores[i])
            {
                inputF.SetActive(true);
                break;
            }
        }
    }

    public void EnterName()
    {
        f_tempScore = currentScore;
        tempName = input.text;
        UpdateHighScore();
    }
    public void PlayAgainButton(){ SceneManager.LoadScene("mike's testing scene"); }
    public void HitStartButton() { currentState = GameState.INTRO; }
    public void SetGameReady(bool gameReady) { gamePlayReady = gameReady; }
    public GameState GetGameState() { return currentState;}
    public GameState GetCurrentState() { return currentState; }
    
}
