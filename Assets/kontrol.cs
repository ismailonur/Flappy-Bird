using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class kontrol : MonoBehaviour
{
    public Sprite []KusSprite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;

    Rigidbody2D fizik;

    int puan = 0;

    public Text puanText;

    bool oyunBitti = true;

    OyunKontrol oyunKontrol;

    AudioSource[] sesler;

    int enYuksekPuan = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("karakterKontrol").GetComponent<OyunKontrol>();
        sesler = GetComponents<AudioSource>();
        enYuksekPuan = PlayerPrefs.GetInt("kayit");

        Debug.Log(enYuksekPuan);
    }

     
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && oyunBitti == true)
        {
            fizik.velocity = new Vector2(0, 0);  // Yerçekimi sürekli arttığı için uyguladığımız kuvvet işe yaramıyor o yüzden velocityi sıfırlıyoruz.
            fizik.AddForce(new Vector2(0, 200));
            sesler[2].Play();
        }
        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        if (fizik.velocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }
        Animasyon();
    }

    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)
        {
            kusAnimasyonZaman = 0;
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "puan")
        {
            puan++;
            puanText.text = "Puan = " + puan;
            sesler[1].Play();
        }
        if(collision.gameObject.tag == "engel")
        {
            oyunBitti = false;
            sesler[0].Play();
            oyunKontrol.OyunBitti();
            GetComponent<CircleCollider2D>().enabled = false;

            if (puan > enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("kayit", enYuksekPuan);
            }
            Invoke("AnaMenuyeDon", 2);
        }
    }

    void AnaMenuyeDon()
    {
        PlayerPrefs.SetInt("puanKayit", puan);
        SceneManager.LoadScene("AnaMenu");
    }

}
