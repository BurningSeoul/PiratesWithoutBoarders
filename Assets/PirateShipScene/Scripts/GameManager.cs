using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameState {MENU, INTRO, GAMEPLAY, GAMEOVER };
    private GameState currentState = GameState.MENU;

    public GameObject[] introObjects;
    private bool gamePlayReady = false;
    private float currentScore = 0;
    private float scoreMultiplier = 1f;
    public Text scoreUI;
    public int winningScore;

    private float ballsRemaining = 30;
    public Text shotsUI;

    public GameObject EndScreenPanel;
    public Text EndScreenScore;
	// Use this for initialization
	void Start () {
		
        foreach(GameObject go in introObjects)
        {
            go.SetActive(false);
        }
        EndScreenPanel.SetActive(false);
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
                    currentState = GameState.GAMEOVER;
                }
                break;
            case GameState.GAMEOVER:
                foreach (GameObject go in introObjects)
                {
                    go.SetActive(false);
                }
                EndScreenPanel.SetActive(true);
                EndScreenScore.text = currentScore.ToString();
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
            ballsRemaining -= 1;
    }
    public void AdjustScore(float score)
    {
        if (currentState == GameState.GAMEPLAY)
            currentScore += score;

        if(currentScore >= winningScore)
        {
            currentState = GameState.GAMEOVER;
        }
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
