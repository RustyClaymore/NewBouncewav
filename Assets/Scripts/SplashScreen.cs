using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public float cooldownBeforeStart = 3;
    private float actualCoolDown = 0;

    public GameObject text;

    private Color tmpColor;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Deaths", 0);
        PlayerPrefs.SetFloat("timeSinceStart", 0);
        tmpColor = text.GetComponent<Text>().color;

    }
	
	// Update is called once per frame
	void Update () {
        actualCoolDown += Time.deltaTime;

        if(actualCoolDown > cooldownBeforeStart)
        {
            text.GetComponent<Text>().color = tmpColor;
            if(Input.anyKeyDown)
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
            tmpColor.a = Mathf.Sin(Time.time) * 3;
        }

        GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.Sin(Time.time) * 30;
	}
}
