using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    //create cube faces - SerializeField allows to save the obj
    [SerializeField, HideInInspector]
    MeshFilter[] cubeFaces;

    //create terrain faces, one for each cube face
    TerrainFace[] terrFace;

    //orientation of the faces of the cube
    Vector3[] directions = { Vector3.up, Vector3.forward, Vector3.back, Vector3.right, Vector3.left, Vector3.down };

    //planet properties
    public PlanetProperties planetProperties;

    [SerializeField]
    public ShapeGenerator shapeGenerator;


    private void Init()
    {
        shapeGenerator = new ShapeGenerator(planetProperties);

        //instantiate parameters
        if (cubeFaces == null || cubeFaces.Length == 0)
        {
            cubeFaces = new MeshFilter[6];
        }
        terrFace = new TerrainFace[6];


        for(int i = 0; i < 6; i++)
        {
            if (cubeFaces[i] == null)
            {
                //create a empty mesh obj for each face, hyerarchically bound to this object
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                //give each face a material to be rendered
                meshObj.AddComponent<MeshRenderer>();
                cubeFaces[i] = meshObj.AddComponent<MeshFilter>();
                cubeFaces[i].sharedMesh = new Mesh();

                //add mesh collider
                cubeFaces[i].gameObject.AddComponent<MeshCollider>();
            }
            cubeFaces[i].GetComponent<MeshRenderer>().sharedMaterial = planetProperties.materialProperties.planetMaterial;

            //initialize the terrainfaces
            terrFace[i] = new TerrainFace(cubeFaces[i].sharedMesh,directions[i], shapeGenerator, planetProperties);
        }
    }


    private void GenerateMesh()
    {
        //construct mesh
        foreach(TerrainFace face in terrFace)
        {
            face.ConstructMesh();
        }

        shapeGenerator.updateElevation();
        shapeGenerator.setCenter(gameObject.transform.position);
        shapeGenerator.MapTextures();
    }


    public void UpdateMesh()
    {
        Init();
        GenerateMesh();
    }


    private void Start()
    {
        UpdateMesh();
    }

}
