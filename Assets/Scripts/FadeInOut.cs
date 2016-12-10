using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FadeInOut : MonoBehaviour {

    public float durationBetweenFades;
    Renderer m_renderer;

	// Use this for initialization
	void Start () {
        m_renderer = GetComponent<Renderer>();
        InvokeRepeating("FadeIn", 0f, 2f * durationBetweenFades);
        InvokeRepeating("FadeOut", 1f, 2f * durationBetweenFades);
    }

	void FadeIn () {
        m_renderer.material.DOFade(1, durationBetweenFades);
	}

    void FadeOut()
    {
        m_renderer.material.DOFade(0, durationBetweenFades);
    }
}
