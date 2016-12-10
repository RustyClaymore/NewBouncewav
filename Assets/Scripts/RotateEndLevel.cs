using UnityEngine;
using System.Collections;

public class RotateEndLevel : MonoBehaviour {

    public float rotationSpeed = 150;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
