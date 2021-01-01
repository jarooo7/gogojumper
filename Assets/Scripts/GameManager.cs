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
    public Text coinScore;

    public bool isImmortal;
    public float immortalityTime;
    public float immortalitySpeed;

    public bool magnetActive;
    public float magnetSpeed;
    public float magnetTime;
    public float magnetDistance;




    private float score;
    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        InitializeGame();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;
        score += worldScrollingSpeed;
        UpdateOnScreenScore();
    }

    void UpdateOnScreenScore()
    {
        scoreText.text = score.ToString("0");
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
        resetButton.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
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
