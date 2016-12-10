using UnityEngine;
using System.Collections;

public class DestroyBoom : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.Find("Level").transform);
        Destroy(gameObject,0.6f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
