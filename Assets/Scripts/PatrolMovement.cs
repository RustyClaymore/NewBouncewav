using UnityEngine;
using System.Collections;

public class PatrolMovement : MonoBehaviour {

    public GameObject patrolPointPrefab;
    public float patrollingSpeed;

    private Transform[] patrolPoints;

    private int patrolPointCounter = 0;
    private int nextPointInTrajectoryIndex = 0;

    // Use this for initialization
	void Start () {
        patrolPoints = transform.parent.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((patrolPoints[nextPointInTrajectoryIndex].position - transform.position) * patrollingSpeed * Time.deltaTime);

        if(Vector3.Distance(patrolPoints[nextPointInTrajectoryIndex].position, transform.position) <= 0.3f)
        {
            nextPointInTrajectoryIndex++;
            if(nextPointInTrajectoryIndex >= patrolPoints.Length)
            {
                nextPointInTrajectoryIndex = 0;
            }
        }
    }

    [ExecuteInEditMode]
    public void AddPatrolPoint()
    {        
        GameObject patrolPnt = (GameObject) Instantiate(patrolPointPrefab, this.transform.position, Quaternion.identity, this.transform.parent);
        patrolPnt.name = "Patrol_Point " + (patrolPointCounter + 1);
        //patrolPoints[patrolPointCounter] = patrolPnt;
        patrolPointCounter++;
    }

    [ExecuteInEditMode]
    public void DeletePatrolPoints()
    {
        Transform[] children = transform.parent.GetComponentsInChildren<Transform>();
        foreach (Transform go in children)
        {
            if (go.CompareTag("PatrolPoint"))
            {
                DestroyImmediate(go.gameObject);
            }
        }
        patrolPointCounter = 0;
    }
}
