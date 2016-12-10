using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BounceScript2D : MonoBehaviour {
    
    public float bouncingSpeed = 10;
    public int bouncingDirection = 1;

    public float bouncingCooldown;
    private float bouncingActualCooldown;

    private bool gameStarted = false;
    private bool gameOver = false;
    private bool musicIsPlaying = false;

    public float cameraShakeDuration = 0.1f;
    public float cameraShakeStrength = 1;
    public int cameraShakeVibrato = 5;
    public float cameraShakeRandomness = 90;

    public Camera mainCamera;
    public GameObject musicPlayer;

    public GameObject deathParticles;

    #region debug
    float lastTimeHit = 0;
    float durationBetweenHits = 0;
    #endregion

    // Use this for initialization
    void Start () {
        bouncingActualCooldown = bouncingCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        bouncingActualCooldown -= Time.deltaTime;

        if(Input.anyKeyDown && !gameStarted)
        {
            gameStarted = true;
        }

        if(gameStarted && !gameOver)
        {
            transform.Translate(new Vector3(bouncingSpeed, 0, 0) * bouncingDirection * Time.deltaTime);
        }
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BouncyObject" && bouncingActualCooldown < 0)
        {
            bouncingActualCooldown = bouncingCooldown;
            bouncingDirection *= -1;
            
            //Debug.Log(Time.timeSinceLevelLoad - lastTimeHit);
            lastTimeHit = Time.timeSinceLevelLoad;
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            gameOver = true;
            GetComponent<Renderer>().enabled = false;
            //Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        if (!musicIsPlaying)
        {
            musicIsPlaying = true;
            musicPlayer.GetComponent<AudioSource>().Play();
        }

        mainCamera.DOShakePosition(cameraShakeDuration, cameraShakeStrength, cameraShakeVibrato, cameraShakeRandomness, true);

    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
