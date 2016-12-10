using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PatrolMovement))]
public class AddPatrolPointsEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PatrolMovement ptrlMvmt = (PatrolMovement)target;

        if(GUILayout.Button("Add Patrol Point"))
        {
            ptrlMvmt.AddPatrolPoint();
        }

        if(GUILayout.Button("Delete Patrol Points"))
        {
            ptrlMvmt.DeletePatrolPoints();
        }
    }
}
