using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.Advertisements;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class GameController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{

    public GameObject player;

    private float t = 0;

    private float t2 = 0;

    private float t3 = 0;

    public static bool collidedTF;

    public static bool collidedFIST;

    public static bool fistShot;

    public static bool tripleShot;

    public static bool circleActive;

    public static int deathAmount;

    public Sound[] sounds;

    public static int rewardedAdCount;


    public PostProcessVolume volume;

    private Vignette vignette;

    private bool ready;

    private bool song = true;

    public static bool start;

    string adUnitId1 = "Interstitial_iOS";

    string adUnitId2 = "Rewarded_iOS";

    #region Singleton class: GameManager

    public static GameController Instance;

    #endregion

    Camera cam;

    public Player ball;
    public PlayerAimWeapon rocketLauncher;
    public Trajectory trajectory;
    private float pushForce;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;
    public static float speedOfRocket;

    public Slider slider;

    public Image fill;
    public Image border;
    public Image timer;

    public Text scoreText;
    private float alphaLevel = 0;
    private float inverseAlphaLevel = 1f;

    public Image title;
    public Image byAphy;

    public Image pointer;
    public Image dottedArrow;

    public Image store;
    public Image settings;
    public Image about;

    public Button statsButton;
    public Button settingsButton;
    public Button aboutButton;

    public Transform lowerBoundary;

    public static float sliderTime;

    void Start()
    {
        tV = 0;

        fire = true;

        deathV = true;

        Player.hitMeteor = false;

        statsButton.enabled = true;
        settingsButton.enabled = true;
        aboutButton.enabled = true;

        collidedTF = false;

        collidedFIST = false;

        tripleShot = false;

        fistShot = false;

        circleActive = false;

        volume.profile.TryGetSettings(out vignette);

        vignette.intensity.value = 0;

        ready = true;

        cam = Camera.main;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        LoadBanner();
        ShowBannerAd();

        start = false;

        temp = 100;
        tempV = 0;

        if(deathAmount == 0)
        {
            Settings.musicOn = true;
            Settings.sfxOn = true;
        }
    }

    void Update()
    {
        sliderTime = slider.value;

        if (fistShot)
        {
            pushForce = 4;
        } else
        {
            pushForce = 3.25f;
        }

        if (slider.value <= 0)
        {
            lr.SetColors(lrDisabled, lrDisabled);
            trajectory.Hide();
            fire = false;
            Time.timeScale = 1;
        }

        fill.color = new Color(1, 0.9258f, 0, alphaLevel);
        border.color = new Color(1, 1, 1, alphaLevel);
        timer.color = new Color(1, 1, 1, alphaLevel);
        title.color = new Color(1, 1, 1, inverseAlphaLevel);
        byAphy.color = new Color(1, 1, 1, inverseAlphaLevel);
        scoreText.color = new Color(1, 1, 1, alphaLevel);
        store.color = new Color(1, 1, 1, inverseAlphaLevel);
        settings.color = new Color(1, 1, 1, inverseAlphaLevel);
        about.color = new Color(1, 1, 1, inverseAlphaLevel);

        if (collidedTF == true)
        {
            StartCoroutine(TripleFDuration());
            tripleShot = true;
        }

        if (collidedFIST == true)
        {
            StartCoroutine(FistDuration());
            fistShot = true;
        }

        if (deathAmount % 5 == 0 && deathAmount != 0 && player == null)
        {
            StartCoroutine(BeginAd());
            deathAmount = 1;
        }

        if (TapToStart.tap && player != null)
        {
            lowerBoundary.position = new Vector3(player.transform.position.x, lowerBoundary.position.y, lowerBoundary.position.z);

            t += 0.8f * Time.unscaledDeltaTime;
            alphaLevel = Mathf.Lerp(0, 1, t);
            inverseAlphaLevel = Mathf.Lerp(1, 0, t*2);
            pointer.color = new Color(1, 1, 1, inverseAlphaLevel);
            dottedArrow.color = new Color(1, 1, 1, inverseAlphaLevel);

            if(start)
            {
                statsButton.enabled = false;
                settingsButton.enabled = false;
                aboutButton.enabled = false;
            }

            if (t == 1)
            {
                TapToStart.tap = false;
            }

        }

        if (player == null)
        {
            vignette.intensity.value = 0;
            SetSlowMo(0);
            trajectory.Hide();
            lr.SetColors(lrDisabled, lrDisabled);

            pointer.color = new Color(1, 1, 1, 0);
            dottedArrow.color = new Color(1, 1, 1, 0);

            t2 += 0.75f * Time.unscaledDeltaTime;
            alphaLevel = Mathf.Lerp(1f, 0f, t2);
            inverseAlphaLevel = 0;

            if (t2 == 1)
            {
                TapToStart.tap = false;
            }
        }

        if ((!fire && start) || (slider.value <= 0 && !deathV && vignette.intensity.value != 0))
        {
            if (slider.value > 0 && vignette.intensity.value != 0)
            {
                tV += 6.5f * Time.unscaledDeltaTime;
                slider.value = Mathf.Lerp(temp, 100, tV);
            } else
            {
                tV += 0.5f * Time.unscaledDeltaTime;
            }

            if (tV < 1 && player != null && Player.hitMeteor == false)
            {
                vignette.intensity.value = Mathf.Lerp(tempV, 0, tV);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            tapToStartText.text = "\n" + "Release To " + "\n" + "Shoot";

            isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    public GameObject tapToStart;
    public Text tapToStartText;

    public IEnumerator BeginAd()
    {
        InitializeAds();
        LoadAd1();
        yield return new WaitForSecondsRealtime(2);
        LoadAd1();
        Debug.Log("Play Ad");
        ShowAd1();
    }

    public IEnumerator TripleFDuration()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        collidedTF = false;
        yield return new WaitForSecondsRealtime(11.20f);
        tripleShot = false;
        collidedTF = false;
    }

    public IEnumerator FistDuration()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        collidedFIST = false;
        yield return new WaitForSecondsRealtime(11.20f);
        fistShot = false;
        collidedFIST = false;
    }

    void Awake ()
    {
        InitializeAds();

        SetMaxSlowMo(100);
        lr.SetColors(lrDisabled, lrDisabled);

        Time.timeScale = 0;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }




   
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

    public void LoadAd1()
    {
        Advertisement.Load(adUnitId1, this);
    }

    public void ShowAd1()
    {
        Advertisement.Show(adUnitId1, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    //public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }

    public void InitializeAds()
    {
        Advertisement.Initialize("4519696", false, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load("Banner_iOS", options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show("Banner_iOS", options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    

//Rewarded Ad Stuff

    public void LoadAd2()
    {

        //Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(adUnitId2, this);
    }

    public void ShowAd2()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        //Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(adUnitId2, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adUnitId2) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            // Load another ad:
            //Advertisement.Load(_adUnitId, this);
        }
    }

    private Color lrDisabled = new Color(0, 0, 0, 0);

    void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        center = cam.ScreenToWorldPoint(new Vector2(0, 0));
        trajectory.UpdateDots(ball.pos, force);

        if (fire)
        {
            tV = 0;
        }

        TapToStart.tap = true;

        if (distance < 0.5f)
        {
            distance = 0.5f;
        }
    }

    private float tV;

    private bool deathV;

    void OnDrag()
    {
        if(slider.value <= 0 && deathV)
        {
            fire = false;
            deathV = false;
            Debug.Log("caught value");
            tempV = vignette.intensity.value;
            tV = 0;
            Debug.Log(vignette.intensity.value);
        }

        if (slider.value > 0 && player != null)
        {
            newCenter = cam.ScreenToWorldPoint(new Vector2(0, 0));

            diffX = newCenter.x - center.x;
            diffY = newCenter.y - center.y;

            startPoint2 = new Vector2(startPoint.x + diffX, startPoint.y + diffY);

            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            distance = Vector2.Distance(startPoint2, endPoint);
            direction = (startPoint2 - endPoint).normalized;
            PlayerAimWeapon.weaponAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;

            if (distance > 1.5f)
            {
                distance = 1.5f;
            }

            if (distance < 0.5f)
            {
                distance = 0.5f;
            }

            if (fire && startPoint2 != endPoint)
            {
                if (start)
                {
                    tV += 0.585f * Time.unscaledDeltaTime;
                    vignette.intensity.value = Mathf.Lerp(0, 0.45f, tV);
                    slider.value = Mathf.Lerp(100, 0, tV);
                    Time.timeScale = 0.3f;

                }
                    trajectory.Show();
                    trajectory.UpdateDots(ball.pos, force);
                    rocketLauncher.aimRocket();
                    ShootRay(player.transform.position, (endPoint - startPoint2).normalized, 10);
                    Color lrColor = new Color(0.5754f, 0.5754f, 0.5754f, 0.1373f);
                    lr.SetColors(lrColor, lrColor);
            } else
            {
                if(start)
                {
                    Time.timeScale = 1;
                }
                trajectory.Hide();
                lr.SetColors(lrDisabled, lrDisabled);
            }

            force = direction * distance * pushForce;

        }
    }

    void OnDragEnd()
    {
        if (slider.value > 0)
        {
            tV = 0;
            tempV = vignette.intensity.value;
            temp = slider.value;
        }

        if (slider.value > 0 && fire && startPoint2 != endPoint)
        {
            trajectory.UpdateDots(ball.pos, force);
            Player._rigidBody.velocity = Vector3.zero;
            Player._rigidBody.angularVelocity = 0;
            ball.Push(force);
            start = true;
            Time.timeScale = 1f;
            rocketLauncher.ShotRocket();
            speedOfRocket = distance;
            fire = false;
            StartCoroutine(Reload());
            lr.SetColors(lrDisabled, lrDisabled);
            trajectory.Hide();
        }

        distance = 0;

        if (GameOverScript.musicOff == false && song && start && Settings.musicOn)
        {
            Play("Song");
            song = false;
        }

        if (start)
        {
            TapToStart.tap = true;
            Destroy(tapToStart);
        }
    }

    private float temp;

    private float tempV;

    IEnumerator Reload()
    {
        if(slider.value > 0)
        {
            yield return new WaitForSeconds(0.25f);
            Debug.Log("Able to Fire");
            tV = 0;
            fire = true;
        }
    }

    private bool fire;


    private float diffX;
    private float diffY;

    private Vector2 center;
    private Vector2 newCenter;
    private Vector2 startPoint2;

    public void SetMaxSlowMo(int slowMo)
    {
        slider.maxValue = slowMo;
        slider.value = slowMo;
    }

    public void SetSlowMo(int slowMo)
    {
        slider.value = slowMo;
    }

    public LineRenderer lr;

    void ShootRay(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        lr.SetPosition(0, targetPosition);
        lr.SetPosition(1, endPosition);
    }

    public void goToAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void goToStats()
    {
        SceneManager.LoadScene("Stats");
    }

    public void goToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}