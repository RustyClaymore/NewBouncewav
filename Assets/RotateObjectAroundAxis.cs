using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAroundAxis : MonoBehaviour {

    public Vector3 rotationAxis;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationAxis * speed);	
	}
}
