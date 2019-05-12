using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public float curBossHealth;
    public float timeSpent;
    public int healthLeft;
    public float userScore;

    public TMP_Text bossHealthUI;

	void Start ()
    {
		
	}

	void Update ()
    {
        DontDestroyOnLoad(gameObject);

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ScoreScene")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Destroy(gameObject);
        }
    }

    #region Comment
    //private float startTime;

    //void Start()
    //{
    //    startTime = Time.time;
    //}

    //void Update()
    //{
    //    float t = Time.time - startTime;

    //    string minutes = ((int)t / 60).ToString();
    //    string seconds = (t % 60).ToString("f0");
    //    Debug.Log(t);
    //    float timeSpent = 200;

    //    float minSpent = 0f;
    //    float secSpent = 0f;

    //    while (timeSpent > 60)
    //    {
    //        timeSpent -= 60f;
    //        minSpent++;
    //    }

    //    if (timeSpent < 60)
    //    {
    //        secSp60ent = timeSpent;
    //    }

    //    GetComponent<TMP_Text>().text = minSpent.ToString("Time : 00") + ":" + secSpent.ToString("00");
    //}
    #endregion
}
