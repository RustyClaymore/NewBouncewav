using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject Camera;
    public GameObject[] CamPositions;
    public bool levelSelect;

    Vector3 levelSelectPos;
    bool active;

    public GameObject levelSelectPanel;

    public GameObject LevelInfo;

    public Text levelText;
    public int levelNum;

    public GameObject EndLevelOverlay;

	public Transform currentMount;
    // Use this for initialization
    void Start()
    {
		currentMount = Camera.transform;
    }

    public void DisplayLevelInfo(string level)
    {
        levelText.text = level;
    }

    public void SetNextLevel(int level)
    {
        levelNum = level;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(levelNum);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Nextlevel()
    {
        if(SceneManager.GetActiveScene().buildIndex < 15)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoveCam(int camNum)
    {
        
		Debug.Log("clicked the button" + camNum);
		currentMount = CamPositions [camNum - 1].transform;
       // iTween.MoveTo(Camera, iTween.Hash("position", CamPositions[camNum - 1].transform.position, "easetype", iTween.EaseType.spring, "time", 2f));
      //  iTween.MoveTo(Camera, iTween.Hash("path",[Vector3(0,200,0),Vector3(200,0,0)],"time",2.0f));
       // iTween.MoveTo(Camera, CamPositions[camNum - 1].transform.position, 1.0f);
       
    }

    public void SetLevelBool(bool value)
    {
        levelSelect = value;
    }

    public void LevelInfoCam()
    {
		currentMount = CamPositions [(levelSelect ? 1 : 0)].transform;
       // iTween.MoveTo(Camera, iTween.Hash("position", CamPositions[(levelSelect ? 1:0)].transform.position, "easetype", iTween.EaseType.spring, "time", 2f));
       // iTween.MoveTo(Camera, CamPositions[(levelSelect ? 1 : 0)].transform.position, 1.0f);
    }

    // Update is called once per frame
    void Update () {
		Camera.transform.position = Vector3.Lerp (Camera.transform.position, currentMount.position, 0.03f);
		Camera.transform.rotation = Quaternion.Slerp (Camera.transform.rotation, currentMount.rotation, 0.03f);
	}
}


/* DEPRECATED CODE
 * 
 * 
 *         if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            active = false;
            levelSelectPos = levelSelect.transform.position;
        }


 *     public void LevelSelect()
    {
        active = !active;

        if (active)
        {
            levelSelectPos = levelSelect.transform.position;
            iTween.ScaleTo(playButton, new Vector3(0, 1, 1), 0.5f);
            iTween.ScaleTo(creditsButton, new Vector3(0, 1, 1), 0.5f);
            iTween.MoveTo(levelSelect, playButton.transform.position, 0.5f);
            Invoke("SlideLevelsIn", 0.25f);
        }
        else
        {
            //SlideLevelsIn();
            iTween.ScaleTo(playButton, new Vector3(1, 1, 1), 0.5f);
            iTween.ScaleTo(creditsButton, new Vector3(1, 1, 1), 0.5f);
            iTween.MoveTo(levelSelect, levelSelectPos, 0.8f);
        }        
    }
 *     void SlideLevelsIn()
    {
        if (active)
        { 
            levelSelectPanel.SetActive(active);
            Debug.Log("triggered");
            iTween.MoveTo(levelSelectPanel, levelSelectPanelOpenRef.transform.position, 0.35f);
            Invoke("SlideLettersDown", .36f);
        }
        else
        {
            iTween.MoveTo(AsPanel, ClosedRef.transform.position, 0.25f);
            iTween.MoveTo(BsPanel, ClosedRef.transform.position, 0.5f);
            iTween.MoveTo(CsPanel, ClosedRef.transform.position, 0.75f);
            Invoke("CloseLevels", 0.75f);
        } 
    }

    void SlideLettersDown()
    {
        iTween.MoveTo(AsPanel, AsOpenRef.transform.position, 0.5f);
        iTween.MoveTo(BsPanel, BsOpenRef.transform.position, 0.75f);
        iTween.MoveTo(CsPanel, CsOpenRef.transform.position, 1f);
    }

    void CloseLevels()
    {
        iTween.MoveTo(levelSelectPanel, levelSelectPanelCloseRef.transform.position, 0.5f);
        Invoke("Deactivate", 0.4f);
    }

    void Deactivate()
    {
        levelSelectPanel.SetActive(active);
    }

    public void OpenLevelInfo()
    {
        iTween.MoveTo(LevelInfo, LevelInfoOpenRef.transform.position, 0.5f);
    }

    public void CloseLevelInfo()
    {
        iTween.MoveTo(LevelInfo, LevelInfoCloseRef.transform.position, 0.5f);
    }
*/
