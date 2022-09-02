using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    float radius;

    //retrieve face vectors (up, right, front) from up vector
    Vector3 upVector;
    Vector3 rightVector;
    Vector3 forwardVector;
    ShapeGenerator shapeGenerator;
    PlanetProperties properties;

    private Vector3[] vertices;
    private int[] triangles;

    public TerrainFace(Mesh mesh, Vector3 upVector, ShapeGenerator shapeGenerator, PlanetProperties properties)
    {
        this.mesh = mesh;
        this.resolution = properties.resolution;
        this.upVector = upVector;
        this.radius = properties.planetRadius;
        this.shapeGenerator = shapeGenerator;
        this.properties = properties;

        rightVector = new Vector3(upVector.y, upVector.z, upVector.x);
        forwardVector = Vector3.Cross(upVector, rightVector);
    }

    public void ConstructMesh()
    {
        //vertices - the number is resolution^2
         vertices = new Vector3[resolution * resolution];

        /** mesh indexes
         * Given vertices, the number of indexes is ---> nTriangles = (nVertices-1)^2 * 2
         * Each triangle has three vertices, so -------> indexes = nTriangles * 3
         */
        triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int indexCount = 0;

        for(int y = 0; y < resolution; y++)
        {
            for(int x = 0; x < resolution; x++)
            {
                //vertece's index
                int i = x + y * resolution;

                //percentage of completition
                Vector2 percent = new Vector2(x,y) / (resolution - 1);

                //retrieve a point on the current face of the cube we are rendering
                Vector3 pointOnUnitCube = upVector + (percent.x - 0.5f) * 2 * rightVector + (percent.y - 0.5f) * 2 * forwardVector;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.getPointHeight(pointOnUnitSphere);

                //create indexes
                if(x != resolution-1 && y != resolution-1)
                {
                    //first triangle
                    triangles[indexCount] = i;
                    triangles[indexCount + 1] = i + resolution + 1;
                    triangles[indexCount + 2] = i + resolution;

                    //second triangle
                    triangles[indexCount + 3] = i;
                    triangles[indexCount + 4] = i + 1;
                    triangles[indexCount + 5] = i + resolution + 1;

                    indexCount += 6;
                }
            }
        }

        //update current mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.normals = CalculateNormals();
        mesh.RecalculateNormals();
        mesh.uv = CalculateUV();
    }

    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length];
        int triangleNumber = triangles.Length / 3;

        //for each triangle we want the normal
        for(int i = 0; i < triangleNumber; i++)
        {
            int index = i * 3;
            int vertexIndexA = triangles[index];
            int vertexIndexB = triangles[index + 1];
            int vertexIndexC = triangles[index + 2];

            Vector3 normal = SurfaceNormalFromIndex(vertexIndexA, vertexIndexB, vertexIndexC);
            vertexNormals[vertexIndexA] = normal;
            vertexNormals[vertexIndexB] = normal;
            vertexNormals[vertexIndexC] = normal;
        }

        for(int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }   

    Vector3 SurfaceNormalFromIndex(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = vertices[indexA];
        Vector3 pointB = vertices[indexB];
        Vector3 pointC = vertices[indexC];

        Vector3 vectorAB = pointB - pointA;
        Vector3 vectorCA = pointC - pointA;

        return Vector3.Cross(vectorAB, vectorCA).normalized;
    }


    Vector2[] CalculateUV()
    {
        Vector2[] uvs = new Vector2[vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 d = vertices[i].normalized;

            //uvs calculation
            float u = Mathf.Clamp01((Mathf.Atan2(d.z, d.x) / (Mathf.PI) + 1f) * .5f);
            float v = 0.5f - Mathf.Asin(d.y) / Mathf.PI;

            uvs[i] = new Vector2(u, v);
        }

        return uvs;
    }
}
