using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public PlayerMotor motor;
	public SpawnManager sm;
	public AudioManager am;
	public Text scoreText;
    public Text highScoreText;
    public Text menu_highScoreText;
    

	public int tempScore = 1;
	public int highScore = 0;

	public AudioSource powerUp;

	public Slider slider;

	public float lookTimer = 0f;
	public float timerDuration = 2f;
	public float startingAmount = 1f;
	public float currentAmount;


	// Use this for initialization
	void Start () 
	{
		//motor = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMotor> ();
		//sm = GameObject.FindGameObjectWithTag ("SM").GetComponent<SpawnManager> ();
		//am = GameObject.FindGameObjectWithTag ("AM").GetComponent<AudioManager> ();

		menu_highScoreText.text = PlayerPrefs.GetInt ("score").ToString ();
		highScore = PlayerPrefs.GetInt ("score");
        highScoreText.text = "Best:" + highScore.ToString();


        tempScore = 0;

        
        if (am.reloadScore == false)
		{
			//tempScore = highScore;
		}
		else
		if (am.reloadScore == true)
		{
			tempScore = am.tempScore;
			motor.buttonEnabled = false;
			Time.timeScale = 0.5f;
			StartCoroutine (endSlomo(0.5f));
		}
		//if (highScore == 0)
		//{
			//tempScore = 1;
		//}

		currentAmount = startingAmount;
        UpdateScoreUI();
    }
	
	// Update is called once per frame
	void Update () 
	{
//		if (motor.gameOver == true)
//		{
//			StartCoroutine (ExecuteAfterTime (3f));
//		}

		if (Input.GetKeyDown(KeyCode.Escape)) {Application.Quit();}

        

        /*(if (tempScore == 0)
		{
			Time.timeScale = 0.5f;
			StartCoroutine (endSlomo (0.5f));
			highScore++;
			tempScore = highScore;
			tempScoreText.color = Color.red;
			tempScoreText.fontSize = 100;
			powerUp.Play ();
		}


		if (tempScore < 0)
		{
			tempScore = 0;
		}*/






        if (motor.isLookedAt)
		{
			lookTimer += Time.deltaTime;
			currentAmount -= 0.5f * Time.deltaTime;
			SetUI ();
		}

		if (slider.value <= 0)
		{
			OnGameEnd ();
		}

	}

    void UpdateScoreUI()
    {
        scoreText.text = tempScore.ToString();
        scoreText.GetComponent<Animator>().SetTrigger("Score");
        am.tempScore = tempScore;
        if (tempScore > highScore)
        {
            highScore = tempScore;
            highScoreText.text = "Best:" + highScore.ToString();
            
        }
    }


	/*public void DecrementTemp()
	{
		tempScore--;
		tempScoreText.color = Color.yellow;
		tempScoreText.fontSize = 60;
		StartCoroutine (ScoreWait (0.3f));
	}*/
    public void IncreaseScore()
    {
        tempScore++;
        UpdateScoreUI();
    }
	public void EndTheGame()
	{
        
        StartCoroutine (ExecuteAfterTime (1f));
	}

	public void OnGameEnd()
	{
		if (PlayerPrefs.GetInt ("score") < highScore)
		{
			PlayerPrefs.SetInt ("score", highScore);
            Debug.Log("High Score");
            AppsFlyerMMP.HighScore();
            GoogleManager.ReportScore(highScore);
        }
        Debug.Log("Game End");
        AppsFlyerMMP.Score(tempScore);
        SceneManager.LoadScene ("Game");
		am.allowChance = true;
	}

	IEnumerator ExecuteAfterTime (float time)
	{
		yield return new WaitForSeconds (time);
	
		OnGameEnd ();
	}
	IEnumerator ScoreWait (float time)
	{
		yield return new WaitForSeconds (time);

		scoreText.color = Color.clear;
	}
	IEnumerator endSlomo(float time)
	{
		yield return new WaitForSeconds (time);

		Time.timeScale = 1f;
	}

	public void SetUI()
	{
		slider.value = currentAmount;
	}
    public void ActivateScoreUI()
    {
        scoreText.transform.parent.gameObject.SetActive(true);
        highScoreText.transform.parent.gameObject.SetActive(true);
        //tempScoreText.gameObject.SetActive(true);
        //playingHighScoreText.gameObject.SetActive(true);
    }
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
