using UnityEngine;
using System.Collections;

public class RotateEnemy : MonoBehaviour {

    public float rotationSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime, Space.World);
	}
}
