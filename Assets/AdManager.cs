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

    static AdManager reklamKontrol;

    public int star;
    public void Start()
    {
        if(reklamKontrol == null)
        {
            DontDestroyOnLoad(gameObject);
            reklamKontrol = this;
        }
        else
        {
            Destroy(gameObject);
        }
        

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

    }

    // BANNERAD START
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
    // BANNERAD END


    // FULLSCREENAD - START
    public void requestFullScreenAd()
    {
        _fullscreenAd = new InterstitialAd(_fullScreenAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        _fullscreenAd.LoadAd(adRequest);

        // Reklam yüklenmesini bekler ondan sonra reklamı gösterir.
        _fullscreenAd.OnAdLoaded += (sender, args) => { _fullscreenAd.Show(); };


    }
    // FULLSCREENAD - END


    // REWARDEDAD - START
    public void requestRewardedAd()
    {
        _rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        AdRequest adRequest = new AdRequest.Builder().Build();

        _rewardBasedVideoAd.LoadAd(adRequest, _rewardedAdID);

        _rewardBasedVideoAd.OnAdLoaded += (sender, args) => { _rewardBasedVideoAd.Show(); };

        _rewardBasedVideoAd.OnAdRewarded += (sender, args) =>
        {
            _bannerAd.Hide();
            Rewarded.text = "Kazandı";
            star = PlayerPrefs.GetInt("star");
            star += 10;
            PlayerPrefs.SetInt("star", star);
        };
    }
    // REWARDEDAD - END
}
