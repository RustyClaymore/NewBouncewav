using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        float timeSinceStart = PlayerPrefs.GetFloat("timeSinceStart", 0);

        text.text = "Game Over !\n\nFinal Score\n " + PlayerPrefs.GetInt("Deaths", 0)  + " Deaths\n\n FINAL TIME\n" +
            string.Format("{0:00}:{1:00}", (int)(timeSinceStart / 60), (int)(timeSinceStart % 60)) + "\n\nThanks for\n Playing!";

        //text.text = "Game Over !\n\nFinal Score\n" + PlayerPrefs.GetInt("Deaths", 0) + " Deaths!\nIn\n"+
        //    string.Format("{0:00}:{1:00}", (int)(timeSinceStart / 60), (int)(timeSinceStart % 60)) + "\n\nThanks for\n Playing!";
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.Sin(Time.deltaTime) * 4;
    }
}
