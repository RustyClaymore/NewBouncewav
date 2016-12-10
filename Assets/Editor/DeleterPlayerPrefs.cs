using UnityEngine;
using System.Collections;
using UnityEditor;

public class DeleterPlayerPrefs : MonoBehaviour {

    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
}
