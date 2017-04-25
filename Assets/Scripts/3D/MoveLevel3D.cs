using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveLevel3D : MonoBehaviour {

    public enum Control_Type {
        Touch,
        Slide,
        Mouse,
        Keyboard,
        DualTouch
    }

    public Control_Type controlType;

    public float levelTranslationSpeed = 5f;
    public bool canMoveLevelUp = true;
    public bool canMoveLevelDown = true;

    private BounceScript3D bounceScript;
    private GameObject level;

    // Use this for initialization
    void Start () {
        level = GameObject.FindGameObjectWithTag("Level");
        bounceScript = GetComponent<BounceScript3D>();
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        controlType = Control_Type.Keyboard;
#elif UNITY_ANDROID
        controlType = Control_Type.DualTouch;
#endif

    }

    // Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().buildIndex == 0)
				Application.Quit ();
			else
				SceneManager.LoadScene (0);
		}
        if (!bounceScript.GetGameOver())
        {
            if (controlType == Control_Type.Mouse)
            {
                //Down Movement
                if (Input.GetMouseButton(0) && (Input.mousePosition.y < Screen.height / 4) && canMoveLevelDown)
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, levelTranslationSpeed * Time.deltaTime, 0));
                }
                //Up Movement
                else if (Input.GetMouseButton(0) && ((Input.mousePosition.y > (Screen.height / 4)) && (Input.mousePosition.y < (Screen.height / 2) && canMoveLevelUp)))
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, -levelTranslationSpeed * Time.deltaTime, 0));
                }
            }
            else if(controlType == Control_Type.Touch)
            {
                //Down Movement
                if (Input.touchCount > 0 && (Input.GetTouch(0).position.y < Screen.height / 4) && canMoveLevelDown)
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, levelTranslationSpeed * Time.deltaTime, 0));
                }
                //Up Movement
                else if (Input.touchCount > 0 && ((Input.GetTouch(0).position.y > (Screen.height / 4)) && (Input.GetTouch(0).position.y < (Screen.height / 2) && canMoveLevelUp)))
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, -levelTranslationSpeed * Time.deltaTime, 0));
                }
            }
            else if (controlType == Control_Type.Slide)
            {
                if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Moved))
                {
                    // Get movement of the finger since last frame
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition.normalized;

                    if (touchDeltaPosition.y > 0 && canMoveLevelDown)
                        level.transform.Translate(new Vector3(0, touchDeltaPosition.y * levelTranslationSpeed * 2 * Time.deltaTime, 0));
                    else if (touchDeltaPosition.y < 0 && canMoveLevelUp)
                        level.transform.Translate(new Vector3(0, touchDeltaPosition.y * levelTranslationSpeed * 2 * Time.deltaTime, 0));
                }
            }
            else if (controlType == Control_Type.DualTouch)
            {
                //Down Movement
                if (Input.touchCount > 0 && (Input.GetTouch(0).position.x < Screen.width / 2) && canMoveLevelDown)
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, levelTranslationSpeed * Time.deltaTime, 0));
                }
                //Up Movement
                else if (Input.touchCount > 0 && (Input.GetTouch(0).position.x > Screen.width / 2) && canMoveLevelUp)
                {
                    Debug.Log(Input.mousePosition.y);
                    level.transform.Translate(new Vector3(0, -levelTranslationSpeed * Time.deltaTime, 0));
                }
            }
            else if (controlType == Control_Type.Keyboard)
            {

                if (Input.GetAxis("Vertical") > 0 && canMoveLevelUp)
                {
                    level.transform.Translate(new Vector3(0, -Input.GetAxis("Vertical") * levelTranslationSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetAxis("Vertical") < 0 && canMoveLevelDown)
                {
                    level.transform.Translate(new Vector3(0, -Input.GetAxis("Vertical") * levelTranslationSpeed * Time.deltaTime, 0));
                }
            }
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "NonBouncyObject")
        {
            if (collider.gameObject.transform.position.y <= transform.position.y)
            {
                canMoveLevelDown = false;
            }
            else
            {
                canMoveLevelUp = false;
            }
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.tag == "NonBouncyObject")
        {
            if (collider.gameObject.transform.position.y <= transform.position.y)
            {
                canMoveLevelDown = true;
            }
            else
            {
                canMoveLevelUp = true;
            }
        }
    }
} 
