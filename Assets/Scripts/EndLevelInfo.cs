using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelInfo : MonoBehaviour {

    float timeElapsed;
    public Text Deaths;
    public Text TotalTime;
    bool once;

	// Use this for initialization
	void Start () {
        once = false;
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        if (once == false && GameObject.Find("Player").GetComponent<BounceScript3D>().gameOver) {
            once = true;
            Deaths.text = PlayerPrefs.GetInt("Deaths").ToString();
            TotalTime.text = (Mathf.Floor(timeElapsed / 60).ToString() + ":" + Mathf.Floor(timeElapsed % 60).ToString());
        }
	}
}
