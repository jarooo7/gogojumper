using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Text highScoreValue;
    public Text coinsValue;
    public Text soundBtnText;


    // Start is called before the first frame update
    void Start()
    {
        float hs = 0;
        if (PlayerPrefs.HasKey("High"))
        {
            hs = PlayerPrefs.GetFloat("High");
        }
        highScoreValue.text = hs.ToString("0");

        int coins = 0;
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        coinsValue.text = coins.ToString();
        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "Turn ON";
        }
        else
        {
            soundBtnText.text = "Turn OFF";
        }

    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Main");
    }
    public void SoundBtn()
    {
        SoundManager.instance.ToggleMuted();
        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "Turn ON";
        }
        else
        {
            soundBtnText.text = "Turn OFF";
        }
    }
}
