using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshGenerator myScript = (MeshGenerator)target;
        if (GUILayout.Button("Generate Mesh"))
        {
            myScript.GenerateMesh();
        }
    }

}
