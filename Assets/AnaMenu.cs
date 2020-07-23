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
    void Start()
    {
        int enYuksekSkor = PlayerPrefs.GetInt("kayit");
        int score = PlayerPrefs.GetInt("puanKayit");
        

        scoreText.text = "Score " + score;
        highScoreText.text = "High Score " + enYuksekSkor;
        

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
        Application.Quit();
    }
}
