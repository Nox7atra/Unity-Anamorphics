using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private float _MeshHeight;
    [SerializeField]
    private int _RowsCount;
    [SerializeField]
    private Material _ImageMaterial;

    private float _MeshWidth;
	public void GenerateMesh()
    {
        GameObject go = new GameObject();
        
        go.name = "Generated Mesh";
        MeshFilter filter = go.AddComponent<MeshFilter>();
        var renderer = go.AddComponent<MeshRenderer>();
        renderer.material = _ImageMaterial;
        Mesh mesh = new Mesh();

        float imageHeight = _ImageMaterial.mainTexture.height;
        float imageWidth = _ImageMaterial.mainTexture.width;
        _MeshWidth = _MeshHeight * imageWidth / imageHeight;

        float step = _MeshWidth / _RowsCount;

        int pointsCountX = Mathf.FloorToInt(_MeshWidth / step) + 1;
        int pointsCountY = Mathf.FloorToInt(_MeshHeight / step) + 1;

        mesh.vertices = CreateVertices(pointsCountX, pointsCountY);
        mesh.normals = CreateNormals(pointsCountX, pointsCountY);
        mesh.uv = CreateUVs(pointsCountX, pointsCountY);
        mesh.triangles = CreateTriangles(pointsCountX, pointsCountY);
        filter.sharedMesh = mesh;

    }

    private Vector3[] CreateVertices(int pointsCountX, int pointsCountY)
    {
        Vector3[] vertices = new Vector3[pointsCountX * pointsCountY];
        for (int z = 0; z < pointsCountY; z++)
        {
            float zPos = ((float)z / (pointsCountY - 1) - .5f) * _MeshHeight;
            for (int x = 0; x < pointsCountX; x++)
            {

                float xPos = ((float)x / (pointsCountX - 1) - .5f) * _MeshWidth;
                int index = x + z * pointsCountX;
                vertices[index] = new Vector3(xPos, zPos + _MeshHeight / 2, 0f);
                
            }
        }
        return vertices;
    }
    private Vector3[] CreateNormals(int pointsCountX, int pointsCountY)
    {
        Vector3[] normals = new Vector3[pointsCountX * pointsCountY];
        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.forward;
        }
        return normals;
    }
    private Vector2[] CreateUVs(int pointsCountX, int pointsCountY)
    {
        Vector2[] uvs = new Vector2[pointsCountX * pointsCountY];
        for (int y = 0; y < pointsCountY; y++)
        {
            for (int x = 0; x < pointsCountX; x++)
            {
                uvs[x + y * pointsCountX].Set(
                    x / (float)(pointsCountX - 1),
                    y / (float)(pointsCountY - 1));
            }
        }
        return uvs;
    }
    private int[] CreateTriangles(int pointsCountX, int pointsCountY)
    {
        int numFaces = (pointsCountX - 1) * (pointsCountY - 1);
        int[] triangles = new int[numFaces * 6];
        int t = 0;
        for (int face = 0; face < numFaces; face++)
        {
            int i = face % (pointsCountX - 1) + (face / (pointsCountX - 1) * pointsCountX);

            triangles[t++] = i + pointsCountX;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + pointsCountX;
            triangles[t++] = i + pointsCountX + 1;
            triangles[t++] = i + 1;
        }
        return triangles;
    }
}
