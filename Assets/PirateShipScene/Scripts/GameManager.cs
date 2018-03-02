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
    public int winningScore;
    public Text[] highScoreNums;
    public Text[] highScoreNames;
    private float[] highScores;
    private string[] names;
    private string tempName;
    public GameObject highScorePanel;
    public GameObject inputF;
    public InputField input;
    private bool startTimer = false;
    private float time = 4;
    public GameObject highScoreText;
    public GameObject yourScoreText;
    private bool b_checkScore = false;

    private float ballsRemaining = 30;
    public Text shotsUI;

    public GameObject EndScreenPanel;
    public Text EndScreenScore;
	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        highScores = new float[10];
        names = new string[10];
        b_checkScore = false;
        time = 4;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            for(int i = 0; i < 10; i++)
            {
                highScores[i] = PlayerPrefs.GetFloat("HighScore" + i);
                PlayerPrefs.SetFloat("HighScore" + i, highScores[i]);
                highScoreNums[i].text = highScores[i].ToString();
                PlayerPrefs.Save();
                Debug.Log(highScores[i]);
            }
        }

        else
        {
            for(int i = 0; i < 10; i++)
            {
                Debug.Log("testing3");
                PlayerPrefs.SetFloat("HighScore" + i, 0);
                PlayerPrefs.Save();
                highScores[i] = PlayerPrefs.GetFloat("HighScore" + i);
                highScoreNums[i].text = highScores[i].ToString();
                PlayerPrefs.Save();
            }
        }

        if (PlayerPrefs.HasKey("Names1"))
        {
            for(int i = 0; i < 0; i++)
            {
                names[i] = PlayerPrefs.GetString("Names1" + i);
                highScoreNames[i].text = names[i];
            }
        }
        else
        {
            for (int i = 0; i < 0; i++)
            {
                PlayerPrefs.SetString("Names1" + i, "AAA");
                names[i] = PlayerPrefs.GetString("Names1" + i);
                PlayerPrefs.Save();
                highScoreNames[i].text = "AAA";
            }
        }

        PlayerPrefs.Save();
		
        foreach(GameObject go in introObjects)
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
                break;
            case GameState.INTRO:
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
                    {
                        currentState = GameState.GAMEOVER;
                    }
                }
                break;
            default:
                return;
        }
	}

    private void LateUpdate()
    {
        scoreUI.text = currentScore.ToString();
        shotsUI.text = ballsRemaining.ToString();
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
            currentScore += score * scoreMultiplier;
        }
            

        if(currentScore >= winningScore)
        {
            currentState = GameState.GAMEOVER;
        }
    }

    public void IncreaseScoreMultiplier() { scoreMultiplier += 0.5f; }

    public void UpdateHighScore()
    {
        for (int i = 0; i < 10; i++)
        {
            if (currentScore > highScores[i])
            {
                /*TempScore1 = highScores[i];
                highScores[i] = TempScore;
                TempScore = TempScore1;

                TempName2 = names[i];
                names[i] = TempName;
                TempName = TempName2;*/




                float temp = highScores[i];
                highScores[i] = currentScore;
                highScoreNums[i].text = highScores[i].ToString();
                //currentScore = highScores[i];

                string tempN = names[i];
                names[i] = tempName;
                highScoreNames[i].text = names[i];
                tempName = tempN;
                //Debug.Log(highScores[i]);
                //Debug.Log(names[i]);
                //Debug.Log(tempN);
            }
        }

        for(int i = 0; i < 10; i++)
        {
            
            PlayerPrefs.SetFloat("HighScore" + 1, highScores[i]);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("Names1" + 1, names[i]);
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

    public void EnterName() {   
        tempName = input.text;
        UpdateHighScore();
    }
    public void PlayAgainButton(){
        SceneManager.LoadScene("mike's testing scene");
    }
    public void HitStartButton()
    {
        currentState = GameState.INTRO;
    }

    public GameState GetGameState() { return currentState;}
    public void SetGameReady(bool gameReady) { gamePlayReady = gameReady; }

    public GameState GetCurrentState() { return currentState; }
    
}
