using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anamorfer : MonoBehaviour
{
    [SerializeField]
    private MeshFilter _ImageMesh;
    [SerializeField]
    private Transform _PlayerPosition;
    [SerializeField]
    private Material _ImageMaterial;

    public void CreateLinearAnamorfedMesh()
    {
        GameObject anamorfedGo = new GameObject();
        anamorfedGo.name = "Result";
        var fitler = anamorfedGo.AddComponent<MeshFilter>();
        var renderer = anamorfedGo.AddComponent<MeshRenderer>();
        renderer.material = _ImageMaterial;
        var mesh = new Mesh();
        Vector3[] verts = new Vector3[_ImageMesh.sharedMesh.vertices.Length];
        Vector3[] normals = new Vector3[_ImageMesh.sharedMesh.vertices.Length];

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = GetIntersectionOfLineAndPlane(
                _ImageMesh.sharedMesh.vertices[i] + _ImageMesh.transform.position,
                _PlayerPosition.position,
                Vector3.forward,
                Vector3.forward + Vector3.right,
                Vector3.right, ref normals[i]);
        }

        mesh.vertices = verts;
        mesh.normals = normals;
        mesh.uv = _ImageMesh.sharedMesh.uv;
        mesh.triangles = _ImageMesh.sharedMesh.triangles;
        fitler.sharedMesh = mesh;
    }

    private Vector3 GetIntersectionOfLineAndPlane(
        Vector3 linePoint1, 
        Vector3 linePoint2,
        Vector3 planePoint1,
        Vector3 planePoint2, 
        Vector3 planePoint3,
        ref Vector3 planeNormal)
    {
        Vector3 result = new Vector3();

        planeNormal = Vector3.Cross(planePoint2 - planePoint1, planePoint3 - planePoint1);
        planeNormal.Normalize();
        Debug.Log(planeNormal.ToString());
        var distance = Vector3.Dot(planeNormal, planePoint1 - linePoint1);
        var w = linePoint2 - linePoint1;
        var e = Vector3.Dot(planeNormal, w);

        if(e != 0)
        {
            result = new Vector3(
                linePoint1.x + w.x * distance / e,
                linePoint1.y + w.y * distance / e,
                linePoint1.z + w.z * distance / e);
        }
        else
        {
            result = Vector3.one * (-505);
        }
        return result;
    }
}
