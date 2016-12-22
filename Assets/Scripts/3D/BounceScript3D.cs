using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


//****************************************************************************
// Project: Bounce.wav 
// Script: BounceScript3D
// Description: Script for the player bouncing behavior

// Game URL: https://nightkenny.itch.io/bouncewav
// Search Engine: https://google.com
// Unity Scripting References: https://docs.unity3d.com/ScriptReference/
//****************************************************************************

public class BounceScript3D : MonoBehaviour {

    // ******************* PUBLIC **************************//
    public Vector3[] patrolPoints;

    public float rotationSpeed = 5;
    public Transform BoomOnTheWall;
    public float bouncingSpeed = 10;
    public int bouncingDirection = 1;

    public float bouncingCooldown;

    public float cameraShakeDuration = 0.1f;
    public float cameraShakeStrength = 1;
    public int cameraShakeVibrato = 5;
    public float cameraShakeRandomness = 90;

    public float cameraTranslationSpeed = 2;

    public GameObject deathParticles;

    public bool enteredVortex;
    
    public float attractionForce = 5;

    public float cooldownBeforeNextLevel = 0;
    public float coolDownBeforeRestart = 0;
  
    public AudioClip bipSound;
    public AudioClip explosion;
    public AudioClip vortexSound;

    public GameObject cam;

    // ******************* PRIVATE **************************//

    private Camera mainCamera;
    private GameObject musicPlayer;

    private Text text;
    private Text timer;

    private float bouncingActualCooldown;

    private bool gameStarted = false;
    private bool gameOver = false;
    private bool musicIsPlaying = false;

    private GameObject endLevelVortex;

    private float timeBeforeNextLevel;
    private float timeBeforeRestart;

    private float timeSinceStart = 0;

    #region debug
    float lastTimeHit = 0;
    float durationBetweenHits = 0;
    #endregion

    // Use this for initialization
    void Start () {
        timeSinceStart = PlayerPrefs.GetFloat("timeSinceStart", 0);

        timer = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        text = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();

        mainCamera = Camera.main;
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");

        timer.text = "TIME\n" + string.Format("{0:00}:{1:00}", (int)(timeSinceStart / 60), (int)(timeSinceStart % 60));

        text.text = " DEATHS\n" + PlayerPrefs.GetInt("Deaths");
        bouncingActualCooldown = bouncingCooldown;

        endLevelVortex = GameObject.FindGameObjectWithTag("EndLevelVortex");
        enteredVortex = false;

        timeBeforeNextLevel = 0;
        timeBeforeRestart = 0;

        GetComponent<AudioSource>().volume = 0.25f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!enteredVortex)
        {
            bouncingActualCooldown -= Time.deltaTime;

        
            transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
       

            if (Input.anyKeyDown && !gameStarted)
            {
                gameStarted = true;
            }


            if(gameOver)
            {
                PlayerPrefs.SetFloat("timeSinceStart", timeSinceStart);
                timeBeforeRestart += Time.deltaTime;

                if ((timeBeforeRestart > coolDownBeforeRestart) && Input.anyKeyDown)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
        else
        {
            PlayerPrefs.SetFloat("timeSinceStart", timeSinceStart);
        }
    }

    void FixedUpdate()
    {

        if (gameStarted && !gameOver)
        {
            transform.Translate(new Vector3(bouncingSpeed, 0, 0) * bouncingDirection * Time.deltaTime, Space.World);
            timeSinceStart += Time.deltaTime;
            timer.text = "TIME\n" + string.Format("{0:00}:{1:00}", (int)(timeSinceStart / 60), (int)(timeSinceStart % 60));
        }

        if (enteredVortex)
        {
            timeBeforeNextLevel += Time.deltaTime;

            if (endLevelVortex.transform.position.y > mainCamera.transform.position.y)
                mainCamera.transform.position += new Vector3(0, cameraTranslationSpeed * Time.deltaTime, 0);
            else
                mainCamera.transform.position -= new Vector3(0, cameraTranslationSpeed * Time.deltaTime, 0);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.Translate((endLevelVortex.transform.position - transform.position) * attractionForce * Time.deltaTime);
            transform.Translate(new Vector3(endLevelVortex.transform.position.y - transform.position.y,
                                        -1 * (endLevelVortex.transform.position.x - transform.position.x),
                                        0) * attractionForce * 5 * Time.deltaTime);

            if (transform.localScale.x >= 0)
                transform.localScale -= new Vector3(0.005f, 0.005f, 0);
        }

        //if (timeBeforeNextLevel >= cooldownBeforeNextLevel)
          //  Application.LoadLevel(Application.loadedLevel + 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!enteredVortex && !gameOver)
        {
            if (collision.gameObject.tag == "BouncyObject" && bouncingActualCooldown < 0)
            {
                bouncingActualCooldown = bouncingCooldown;
                bouncingDirection *= -1;

                GetComponent<AudioSource>().PlayOneShot(bipSound);
                Instantiate(BoomOnTheWall,transform.position, Quaternion.identity);

                Vibration.Vibrate(10);

                lastTimeHit = Time.timeSinceLevelLoad;
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                GetComponent<AudioSource>().PlayOneShot(explosion);
                Vibration.Vibrate(100);

                gameOver = true;
                GetComponent<Renderer>().enabled = false;
                Instantiate(deathParticles, transform.position, Quaternion.identity);

                PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0) + 1);
                text.text = " DEATHS\n" + PlayerPrefs.GetInt("Deaths");
            }

            if (!musicIsPlaying)
            {
                musicIsPlaying = true;
                musicPlayer.GetComponent<AudioSource>().Play();
            }

            mainCamera.DOShakePosition(cameraShakeDuration, cameraShakeStrength, cameraShakeVibrato, cameraShakeRandomness, true);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "EndLevelVortex")
        {
            GetComponent<AudioSource>().PlayOneShot(vortexSound);
            enteredVortex = true;
            gameOver = true;
            Invoke("EndLevel", 3);
        }
    }

    void EndLevel()
    {
        //EndLevelOverlay.SetActive(true);
        //iTween.ScaleTo(EndLevelOverlay, new Vector3(1, 1, 1), 0.5f);
        //Application.LoadLevel(Application.loadedLevel + 1);
        iTween.RotateTo(cam, new Vector3(-90, 0, 0), 2);
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
