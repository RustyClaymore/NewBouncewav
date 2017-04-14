using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
    public GameObject animationRoots;
    Transform[] root_array;
    public float time_period;
    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DrawAnimation()
    {
        root_array = new Transform[animationRoots.transform.childCount];
        for(int i=0;i<animationRoots.transform.childCount;++i)
        {
            root_array[i] = animationRoots.transform.GetChild(i);
        }
        iTween.MoveTo(this.gameObject, iTween.Hash("path", root_array, "time", time_period,"easetype",iTween.EaseType.easeInOutSine));
    }
}
