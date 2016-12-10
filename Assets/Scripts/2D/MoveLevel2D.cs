using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveLevel2D : MonoBehaviour {

    public GameObject level;

    public float levelTranslationSpeed = 5f;
    public bool canMoveLevelUp = true;
    public bool canMoveLevelDown = true;

    private BounceScript2D bounceScript;

    // Use this for initialization
    void Start () {
        bounceScript = GetComponent<BounceScript2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!bounceScript.GetGameOver())
        { 
            if(Input.GetAxis("Vertical") > 0 && canMoveLevelUp) {
                level.transform.Translate(new Vector3(0, Input.GetAxis("Vertical") * levelTranslationSpeed * Time.deltaTime, 0));
            }
            else if (Input.GetAxis("Vertical") < 0 && canMoveLevelDown)
            {
                level.transform.Translate(new Vector3(0, Input.GetAxis("Vertical") * levelTranslationSpeed * Time.deltaTime, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "NonBouncyObject")
        {
            if (collider.gameObject.transform.position.y <= transform.position.y)
            {
                canMoveLevelUp = false;
            }
            else
            {
                canMoveLevelDown = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "NonBouncyObject")
        {
            if (collider.gameObject.transform.position.y <= transform.position.y)
            {
                canMoveLevelUp = true;
            }
            else
            {
                canMoveLevelDown = true;
            }
        }
    }
} 
