using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float worldScrollingSpeed;
    public Text scoreText;
    public GameObject obstacle;
    public float obstacleSpawRate;
    public float minObstacleSpawnHeight;
    public float maxObstacleSpawnHeight;
    public float obstacleSpawnPositionX;
    public bool inGame;
    public GameObject resetButton;
    public GameObject pausaMenu;
    public Text coinScore;
    public Text highTextScore;

    public bool isImmortal;
    public float immortalityTime;
    public float immortalitySpeed;

    public bool magnetActive;
    public float magnetSpeed;
    public float magnetTime;
    public float magnetDistance;




    private float score;
    private float highScore;
    private int coins;

    void Start()
    {
        instance = this;
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (PlayerPrefs.HasKey("High"))
        {
            highScore = PlayerPrefs.GetFloat("High");
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetFloat("High", 0);
        }

        InitializeGame();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;

        UpdateOnScreenScore();
    }

    void UpdateOnScreenScore()
    {
        score += worldScrollingSpeed;
        if (score > highScore)
        {
            highScore = score;
        }
        scoreText.text = score.ToString("0");
        highTextScore.text = highScore.ToString("0");
        coinScore.text = coins.ToString();
    }

    void SpawnObsacle()
    {
        if (!GameManager.instance.inGame) return;
        var spawPosition = new Vector3(obstacleSpawnPositionX,
            Random.Range(minObstacleSpawnHeight, maxObstacleSpawnHeight), 0f);
        Instantiate(obstacle, spawPosition, Quaternion.identity);
    }

    void InitializeGame()
    {
        inGame = true;
    }

    public void GameOver()
    {
        inGame = false;
        PlayerPrefs.SetFloat("High", highScore);
        resetButton.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
         inGame = false;
         pausaMenu.SetActive(true);
    }

    public void PlayGame()
    {
        inGame = true;
        pausaMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
   

    public void CoinCollection(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
    }


    public void ImmortalityStart()
    {
        if (isImmortal) { 
            CancelInvoke("CancelImmoortality");
            worldScrollingSpeed -= immortalitySpeed;
        }
        isImmortal = true;
        worldScrollingSpeed += immortalitySpeed;
        Invoke("CancelImmoortality", immortalityTime);

    }

    private void CancelImmoortality()
    {
        worldScrollingSpeed -= immortalitySpeed;
        isImmortal = false;
    }

    public void MagnetCollected()
    {
        if (magnetActive)
            CancelInvoke("CancelMagnet");
        magnetActive = true;
        Invoke("CancelMagnet", magnetTime);
    }

    private void CancelMagnet()
    {
        magnetActive = false;
    }
}
