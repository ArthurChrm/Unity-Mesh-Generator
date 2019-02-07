using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Mesh_Generator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    int X_SIZE = 100;
    int Z_SIZE = 100;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(X_SIZE + 1) * (Z_SIZE + 1)];

        for (int i = 0, z = 0; z <= Z_SIZE; z++)
        {
            for (int x = 0; x < X_SIZE; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[X_SIZE * Z_SIZE * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < Z_SIZE; z++)
        {
            for (int x = 0; x < X_SIZE; x++)
            {

                triangles[0 + tris] = vert + 0;
                triangles[1 + tris] = vert + X_SIZE + 1;
                triangles[2 + tris] = vert + 1;
                triangles[3 + tris] = vert + 1;
                triangles[4 + tris] = vert + X_SIZE + 1;
                triangles[5 + tris] = vert + X_SIZE + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
