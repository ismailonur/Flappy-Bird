using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{

    public static AdManager instance;
    // Buradaki appId'ye kendi appIdinizi yapıştırın.
    private string _appId = "ca-app-pub-7215776518930801~4603552933";

    public BannerView _bannerAd;
    // Bu test Banner Id si 
    private string _bannerAdId = "ca-app-pub-3940256099942544/6300978111";

    // Tam ekran reklam göstermek için gereken test id si
    private string _fullScreenAdId = "ca-app-pub-3940256099942544/1033173712";
    public InterstitialAd _fullscreenAd;

    // Ödüllü reklam göstermek için gereken test id'si   
    private string _rewardedAdID = "ca-app-pub-3940256099942544/5224354917";
    public RewardBasedVideoAd _rewardBasedVideoAd;

    public Text Banner;
    public Text FullScreen;
    public Text Rewarded;

    public bool FullScreenAdControl = true;
    public bool RewardedAdControl = true;

    public void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-7215776518930801~4603552933";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-7215776518930801~4603552933";
        #else
            string appId= "unexpected_platform";
        #endif
         

        MobileAds.Initialize(appId);

    }

    public void Update()
    {
        // Banner reklamımızı test etmek için Update methodunda klavyemizden B tuşuna basacağız.
        if (Input.GetKeyDown(KeyCode.B))
        {
            showBannerAd();
        }

        // FullScreen reklamamızı test etmek için F tuşuna basacağız.
        if (Input.GetKeyDown(KeyCode.F))
        {
            showFullScreenAd();
        }

        // Rewarded reklamamızı test etmek için R tuşuna basacağız.
        if (Input.GetKeyDown(KeyCode.R))
        {
            showRewardedAd();
        }

        if (FullScreenAdControl)
        {
            requestFullScreenAd();
            FullScreenAdControl = false;
        }

        if (RewardedAdControl)
        {
            requestRewardedAd();
            RewardedAdControl = false;
        }
    }

    public void requestBannerAd()
    {

        _bannerAd = new BannerView(_bannerAdId, AdSize.Banner, AdPosition.Bottom);

        AdRequest adRequest = new AdRequest.Builder().Build();

        // burada banner reklamımızın AdMobdan yüklüyoruz ve göstermek için hazır hale getiriyoruz gibi düşünebilirsiniz.
        _bannerAd.LoadAd(adRequest);
        Banner.text = "Çalıştı";
    }

    public void showBannerAd()
    {
        requestBannerAd();

        // yüklenen banner reklamımızı göstermek için aşağıdaki kodu kullanıyoruz.
        _bannerAd.Show();
    }

    // FULLSCREENAD - START
    public void requestFullScreenAd()
    {
        _fullscreenAd = new InterstitialAd(_fullScreenAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        _fullscreenAd.LoadAd(adRequest);
    }

    public void showFullScreenAd()
    {
        FullScreen.text = "İçerde";
        if (_fullscreenAd.IsLoaded())
        {
            FullScreen.text = "Calıştı";
            _fullscreenAd.Show();
            FullScreenAdControl = true;

        }
        else
        {
            FullScreen.text = "Daha Yüklenmedi";
            Debug.Log("FullScreenAd daha yüklenmedi!!");
        }
    }
    // FULLSCREENAD - END

    // REWARDEDAD - START
    public void requestRewardedAd()
    {
        _rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        AdRequest adRequest = new AdRequest.Builder().Build();

        _rewardBasedVideoAd.LoadAd(adRequest, _rewardedAdID);
    }

    public void showRewardedAd()
    {
        Rewarded.text = "İçerde";
        if (_rewardBasedVideoAd.IsLoaded())
        {
            Rewarded.text = "Çalıştı";
            _rewardBasedVideoAd.Show();
            RewardedAdControl = true;
        }
        else
        {
            Rewarded.text = "Daha Yüklenmedi";
            Debug.Log("Rewarded reklamımız daha yüklenmedi!!");
        }
    }

}
