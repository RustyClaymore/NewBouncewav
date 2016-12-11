using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScaleWithMusic : MonoBehaviour {

    public float scaleMultiplier;
    
    private MusicFFT musicFFT;
    private float initialScale;

    // Use this for initialization
    void Start () {
        initialScale = transform.localScale.x;
        musicFFT = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicFFT>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float desiredScale = initialScale + musicFFT.rms * scaleMultiplier;
        float newScale = Mathf.Lerp(transform.localScale.x, desiredScale, Time.deltaTime * 8);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
