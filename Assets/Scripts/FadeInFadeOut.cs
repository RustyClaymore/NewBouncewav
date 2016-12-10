using UnityEngine;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InvisWall")
            StartCoroutine(FadeTo(0.0f, 0.6f));
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag== "InvisWall")
        StartCoroutine(FadeTo(1.0f, 0.6f));
    }
    // Update is called once per frame
    void Update () {
	
	}
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            Debug.Log(alpha);
            GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}
