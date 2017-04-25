using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindStart : MonoBehaviour {

    [SerializeField]
    GameObject deatheffect;
    //[SerializeField]
    //Transform backParticle;
    [SerializeField]
    Sprite[] sprites;
    BounceScript3D BS;
    List<GameObject> rewindObjects = new List<GameObject>();
    List<Vector3>[] pathList;
    List<Vector3> initPosList = new List<Vector3>();
    List<GameObject> explosionParticles = new List<GameObject>();
    bool startRestore, doRewind, doexplosion, imploded;
    Collider theCollider;

    public MusicPlayer musicPlayer;

    //GameObject UiLeft, UiRight; //UiCycle; // UI of rewinding icon, needs fixed for different screen resolution

    private int explosionParticleQuantity = 20; 
    private int pathCutThreshold = 50; // when the path lenth reaches threshold, the path units with odd index number will be removed. 
    private float explosionSpreadLowerLimit = 0.1f, explosionSpreadUpperLimit = 0.5f, explosionSpeedScaler = 0.02f;
    private float initialTickToStorePath = 0.1f, tickToStorePath; // When path is cut, the tick time will be doubled to maintain consistency of path restoring. 
    //The path lenth greater than half of pathCutThreshold rewinding time will be max time; Otherwise, it's path_length*maxtime/pathcutThreshold to scale the case when player dies at very beginning
    private float rewindingMaxTime = 0.5f;

	public bool autoRewind = true;
	public float autoRewindWaitTime = 0.5f;
    public bool Implosion{get{return imploded;}}

	// Use this for initialization
	void Start () {
        doRewind = true;
        doexplosion = true;
        imploded = false;
        tickToStorePath = initialTickToStorePath;
        //UiLeft = GameObject.Find("left");
        //UiRight = GameObject.Find("right");
        //UiCycle = GameObject.Find("rewindCycle");
        theCollider = GetComponent<Collider>();
        BS = GetComponent<BounceScript3D>();
        rewindObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        for(int i = rewindObjects.Count-1; i >=0; --i)
        {
            string temp = rewindObjects[i].transform.parent.name;
            if ((temp.Length >= 11 &&temp.Contains("StaticEnemy")) || rewindObjects[i].name.Contains("Wall"))
                rewindObjects.RemoveAt(i);
        }
        rewindObjects.Add(GameObject.FindGameObjectWithTag("Level"));
        rewindObjects.Add(gameObject);
        pathList = new List<Vector3>[rewindObjects.Count];
        for (int i = 0; i < rewindObjects.Count; ++i)
            pathList[i] = new List<Vector3>();
        loadPath();
        foreach (List<Vector3> list in pathList)
            initPosList.Add(list[0]);
        //foreach (GameObject a in enemies)
        //    print(a.name);

    }
	
	// Update is called once per frame
	void Update () {
        /*if (doexplosion && BS.GetGameOver())
            initExplosion();*/
        /*
        if (BS.Rewinding && doRewind && implosion)
        {
            implosion = false;
            //setBMG();
            StartCoroutine(ScaleTime(0.0f, 1.0f, 1.0f));
        }
        */
        if (!startRestore && BS.GetGameStart())
        {
            StartCoroutine(restorePath());
            startRestore = true;
        }

	}

	IEnumerator autoRewindStart(){
		yield return new WaitForSecondsRealtime(autoRewindWaitTime);
		if (autoRewind) {
			doImplosion();
		}
	}


    public void doImplosion()
    {

        imploded = false;
        //setBMG();
        StartCoroutine(iconAnim());
        StartCoroutine(imploding(0.0f, 1.0f, 1.0f));

    }


    public void initExplosion()
    {
        setCollider(false);
        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < explosionParticleQuantity; ++i)
        {
            Vector3 randPos = transform.position;
            float randRange = Random.Range(explosionSpreadLowerLimit,explosionSpreadUpperLimit);
            randPos.x += Random.Range(-randRange, randRange);
            randPos.y += Random.Range(-randRange, randRange);
            explosionParticles.Add(GameObject.Instantiate(deatheffect, randPos, Quaternion.identity));
        }
        doexplosion = false;
        StartCoroutine(imploding(1.0f,0.0f,1.0f));
    }

    IEnumerator iconAnim()
    {
        int counter = 0;
        bool isRight = true;
        //UiLeft.GetComponent<UnityEngine.UI.Image>().enabled = true;
        //UiRight.GetComponent<UnityEngine.UI.Image>().enabled = true;
        //UiCycle.GetComponent<UnityEngine.UI.Image>().enabled = true;
        while (BS.Rewinding)
        {
            Camera.main.GetComponent<CRT>().enabled = true;
            Camera.main.GetComponent<CRT>().Distortion -= 0.01f;

            //if (isRight)
            //    UiRight.GetComponent<UnityEngine.UI.Image>().sprite = sprites[counter];
            //else
            //    UiLeft.GetComponent<UnityEngine.UI.Image>().sprite = sprites[counter];
            ++counter;
            if (counter > 12)
            {
                isRight = !isRight;
                counter = 0;
            }
            //UiCycle.GetComponent<RectTransform>().Rotate(Vector3.forward*10);
            yield return null;
        }
        //UiLeft.GetComponent<UnityEngine.UI.Image>().enabled = false;
        //UiRight.GetComponent<UnityEngine.UI.Image>().enabled = false;
        //UiCycle.GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    IEnumerator imploding(float start, float end, float time)     //not in Start or Update
    {
        Vector3 pPos = transform.position;
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            foreach (GameObject part in explosionParticles)
            {
                Vector3 partPos = part.transform.position;
                Vector3 dirPos = (end == 0.0f) ? (partPos - pPos).normalized : (pPos - partPos).normalized;
                part.transform.Translate(dirPos * explosionSpeedScaler);
                //Color temp = part.GetComponent<Material>().color;
                //temp.a = Mathf.Lerp(start, end, timer / time);
                //part.GetComponent<Material>().color = temp;

            }
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }
        Time.timeScale = end;
		if (end == 0.0f) {
			imploded = true;
			StartCoroutine( autoRewindStart ());
		}
        if (end == 1.0f)
        {
            rewinding();
        }
    }

    //void setBMG()
    //{
    //    Vector3 pos = backParticle.position;
    //    Vector3 rot = backParticle.localEulerAngles;
    //    pos.z *= -1 ;
    //    rot.x += 180;
    //    backParticle.position = pos;
    //    backParticle.localEulerAngles = rot;
    //}

    void rewinding()
    {
        doRewind = false;
        int counter = pathList[0].Count;
        float totalTime = (counter > pathCutThreshold/2)?rewindingMaxTime:counter*rewindingMaxTime/pathCutThreshold;
        float tick = totalTime / counter;
        //Debug.LogWarning(tick);
        BS.SetRender(true);
        for(int i = explosionParticles.Count - 1; i >= 0; --i)
        {
            Destroy(explosionParticles[i]);
        }
        explosionParticles.Clear();
        StartCoroutine(ReadPath(tick,counter-1));
    }

    IEnumerator ReadPath(float tick, int counter)
    {

        yield return new WaitForSecondsRealtime(tick);
        if (counter >= 0)
        {
            for (int i = 0; i < rewindObjects.Count; ++i)
            {
                rewindObjects[i].transform.localPosition = pathList[i][counter];
            }
            StartCoroutine(ReadPath(tick, --counter));
        }
        else
        {
            for (int i = 0; i < rewindObjects.Count; ++i)
            {
                pathList[i].Clear();
                pathList[i].Add(initPosList[i]);
            }
            BS.ResetGameInitials();
            resetVars();
        }
    }

    IEnumerator restorePath()
    {
        yield return new WaitForSeconds(tickToStorePath);
        if (!BS.GetGameOver())
        {
            StartCoroutine(restorePath());
            loadPath();
        }
    }

    void loadPath()
    {
        for (int i = 0; i < rewindObjects.Count; ++i)
        {
            pathList[i].Add(rewindObjects[i].transform.localPosition);
        }
        //Debug.LogWarning("pre count: " + pathList[0].Count);
        if (pathList[0].Count > pathCutThreshold)
            CutPath();
        //Debug.LogWarning("after count: " + pathList[0].Count);

    }

    void CutPath()
    {
        for (int k = 1; k < pathList[0].Count; ++k)
        {
            for (int i = 0; i < rewindObjects.Count; ++i)
            {
                pathList[i].RemoveAt(k);
            }
        }
        tickToStorePath *= 2;
    }

    void setCollider(bool input)
    {
        theCollider.enabled = input;
    }

    void resetVars()
    {
        Camera.main.GetComponent<CRT>().enabled = false;
        Camera.main.GetComponent<CRT>().Distortion = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BounceScript3D>().musicIsPlaying = false;

        tickToStorePath = initialTickToStorePath;
        doRewind = true;
        startRestore = false;
        doexplosion = true;
        imploded = false;
        setCollider(true);
    }
}
