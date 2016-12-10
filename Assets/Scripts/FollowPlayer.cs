using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public float cameraTranslateSpeed;

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
        if(player.transform.position.x > 5)
        {
            if(transform.position.x < 10)
            {
                transform.Translate(new Vector3(cameraTranslateSpeed * Time.deltaTime, 0, 0));
            }
        }
        else if(player.transform.position.x <= 5 && player.transform.position.x > -5)
        {
            if (transform.position.x > 0.1f)
            {
                transform.Translate(new Vector3(-cameraTranslateSpeed * Time.deltaTime, 0, 0));
            }
            else if (transform.position.x < -0.1f)
            {
                transform.Translate(new Vector3(cameraTranslateSpeed * Time.deltaTime, 0, 0));
            }
        }
        else if(player.transform.position.x <= -5)
        {
            if (transform.position.x > -10)
            {
                transform.Translate(new Vector3(-cameraTranslateSpeed * Time.deltaTime, 0, 0));
            }
        }
	}
}
