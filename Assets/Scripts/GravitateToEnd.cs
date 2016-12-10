using UnityEngine;
using System.Collections;

public class GravitateToEnd : MonoBehaviour {

    public GameObject endLevelVortex;
    public float attractionForce = 5;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate((endLevelVortex.transform.position - transform.position) * attractionForce * Time.deltaTime);
        transform.Translate(new Vector3(endLevelVortex.transform.position.y - transform.position.y,
                                    -1 * (endLevelVortex.transform.position.x - transform.position.x),
                                    0) * attractionForce * 5 * Time.deltaTime);

        if(transform.localScale.x >= 0)
            transform.localScale -= new Vector3(0.005f, 0.005f, 0);

    }
}
