using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFFT : MonoBehaviour {

    AudioSource audioSource;

    [HideInInspector]
    public float[] spectrum;

    public int sampleSize = 256;
    [HideInInspector]
    public float rms;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        spectrum = new float[sampleSize];

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float sum = 0;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += spectrum[i] * spectrum[i];
        }

        rms = Mathf.Sqrt(sum / sampleSize);
        
    }
}
