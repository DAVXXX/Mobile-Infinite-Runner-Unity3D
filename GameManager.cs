using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject PowerUP;

    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    public List<GameObject> targets;
    public List<GameObject> powerUp;

    public GameObject Titlescreen;

    private PlayerController playerController;


    public Text highScore;
    public Text scoreText;
    public Text timerText;

    public Image Gameover;
    public Button restartButton;

    public Button Camera1;
    public Button Camera2;
    public Button Camera3;

    private int score;
    public float spawnRate = 3.0f;
    private float lastIncreased;
    private float timeDelay = 10;



    private float startTime;

    public float PowerUpspawnRate = 9.0f;


    public bool isGameActive;

    private AudioSource playerAudio;
    public AudioClip explode;
    public AudioClip powerup;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        startTime = Time.time;


        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeDelay + lastIncreased)
        {
            lastIncreased = Time.time;

            //spawnRate -= 0.1f;
        }


        if (isGameActive == false)

            return;

            float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

        
       


    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {


            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index], transform.position, obstaclePrefab.transform.rotation );

        }

    

    }


    IEnumerator SpawnPowerUp()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(PowerUpspawnRate);

            int index = Random.Range(0, powerUp.Count);
            Instantiate(powerUp[index], new Vector3(75,1,-5), PowerUP.transform.rotation) ;
        }

    }
    


    public void UpdateScore(int scoreToAdd)
    {
       ///All this need to be reworked 
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();



        // All this works well 
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }

    }

    public void HighScoreReset()
    {
        if (isGameActive == false)
        {
            PlayerPrefs.DeleteAll();
            highScore.text = "0";
        }
        
    }

  
    
    public void GameOver()
    {

        playerAudio.PlayOneShot(explode, .5f);
        Gameover.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        timerText.color = Color.yellow;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        isGameActive = true;

        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);


        Titlescreen.gameObject.SetActive(false);
    }

    public void PowerUpSFX()
    {
        playerAudio.PlayOneShot(powerup, 1.0f);
    }

    public void ExplodeSFX()
    {
        playerAudio.PlayOneShot(explode, .50f);

    }
    public void ChangeCam()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);


    }

    public void ChangeCam2()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        cam3.SetActive(false);

    }

    public void ChangeCam2D()
    {
        cam1.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(true);

    }
}
