using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour {

    /// <summary>
    /// http://dreamlo.com/lb/YgHJr_GuzkeqFn9rr5zMQAnOhDA_uEg0WbxZ7uWxFTdw 
    /// ^ Link to Dreamlo
    /// http://dreamlo.com/lb/YgHJr_GuzkeqFn9rr5zMQAnOhDA_uEg0WbxZ7uWxFTdw/clear
    /// ^ Link to clear all score.
    /// </summary>

    public InputField userInput;
    public string inputText;

    public float _dmgDealt;
    public float _timeSpent;
    public int _healthLeft;
    public float _userScore;

    public TMP_Text dmgDealtUI;
    public TMP_Text timeUI;
    public TMP_Text lifeUI;
    public TMP_Text scoreUI;

    public GameObject ScoreOverlay;
    public GameObject HighscoreOverlay;

    public Score score;

    void Awake()
    {
        score = GameObject.Find("ScoreCounter").GetComponent<Score>();
        _dmgDealt = 2000 - score.curBossHealth;
        if (_dmgDealt > 2000)
        {
            _dmgDealt = 2000;
        }

        _timeSpent = score.timeSpent;
        string minutes = ((int)_timeSpent / 60).ToString("00");
        string seconds = (_timeSpent % 60).ToString("00");

        _healthLeft = score.healthLeft;
        if (_healthLeft == 0)
        {
            _healthLeft = 1;
        }

        _userScore = _dmgDealt / _timeSpent * _healthLeft * 5;

        dmgDealtUI.text = "Dmg Dealt : " + _dmgDealt.ToString("0000");
        timeUI.text = "Time : " + minutes + ":" + seconds;
        lifeUI.text = "Life : " + _healthLeft.ToString("0");
        scoreUI.text = "Score : " + _userScore.ToString("0000");

        //bossHealthUI.text = "Boss Health : " + curBossHealth.ToString("0000");


        //float bossHealth = 10000;
        //float curHealthScore = 1000;

        //float healthScore = 0;

        //healthScore = Mathf.Lerp(bossHealth, curHealthScore, Time.time * 0.25f);

        //GetComponent<TMP_Text>().text = healthScore.ToString("Score : 0000");
    }

    public void RestartGame()
    {
        GameManager.HasWon = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnterUsername()
    {
        ScoreOverlay.SetActive(false);
        HighscoreOverlay.SetActive(true);

        ChangeScore(userInput.text, (int)_userScore);
    }

    void ChangeScore(string _username, int _score)
    {
        int score = _score;
        string username = _username;
        Highscores.AddNewHighscore(username, score);

    }
}
