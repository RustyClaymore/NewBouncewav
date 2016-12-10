using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroAnimation : MonoBehaviour {

    public float fovTransitionDuration;
    public float rotationTransitionDuration;

	// Use this for initialization
	IEnumerator Start () {
        //yield return new WaitForSeconds(1f);
        yield return Camera.main.GetComponent<Camera>().DOFieldOfView(60, fovTransitionDuration);

        yield return transform.DORotate(new Vector3(-30, 0, 0), rotationTransitionDuration);
        
    }
	
    void transitionRotate()
    {
    }
	// Update is called once per frame
	void Update () {
		
	}
}
