using UnityEngine;
using System.Collections;

public class RotateWall : MonoBehaviour {

    public float durationBeforeRotate;
    private float currentDuration;

    public LineRenderer[] lineRenderers;

	// Use this for initialization
	void Start () {
        currentDuration = durationBeforeRotate;

        if (transform.tag == "BouncyObject")
        {
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].material.SetColor("_EmissionColor", Color.blue);
            }
        }
        else
        {
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].material.SetColor("_EmissionColor", Color.red);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        currentDuration -= Time.deltaTime;

        if(currentDuration <= 0)
        {
            currentDuration = durationBeforeRotate;
            transform.Rotate(new Vector3(0, 0, 90));
            if(transform.tag == "BouncyObject")
            {
                for (int i = 0; i < lineRenderers.Length; i++)
                {
                    lineRenderers[i].material.SetColor("_EmissionColor", Color.red);
                }
                transform.tag = "Enemy";
            }
            else
            {
                for (int i = 0; i < lineRenderers.Length; i++)
                {
                    lineRenderers[i].material.SetColor("_EmissionColor", Color.blue);
                }
                transform.tag = "BouncyObject";
            }
        }
	}
}
