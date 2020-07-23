using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text starText;

    public int star;

    public AdManager Ad;

    void Start()
    {
        int enYuksekSkor = PlayerPrefs.GetInt("kayit");
        int score = PlayerPrefs.GetInt("puanKayit");
        

        scoreText.text = "Score " + score;
        highScoreText.text = "High Score " + enYuksekSkor;

        Ad = Object.FindObjectOfType<AdManager>();
        Ad.showBannerAd();

    }

    void Update()
    {
        star = PlayerPrefs.GetInt("star");
        starText.text = "Star " + star;
    }

    public void OyunaGit()
    {
        SceneManager.LoadScene("Level");
    }

    public void OyundanCik()
    {
        Ad.requestFullScreenAd();
        Ad._fullscreenAd.OnAdClosed += (sender, args) => { Application.Quit(); };
    }
}
