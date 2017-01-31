using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Anamorfer))]
public class AnamorferEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Anamorfer myScript = (Anamorfer)target;
        if (GUILayout.Button("Do magic"))
        {
            myScript.CreateLinearAnamorfedMesh();
        }
    }

}
