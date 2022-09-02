using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlacement : MonoBehaviour
{
    public GameObject[] terrains;
    public GameObject grass;
    public Transform planet;
    public float grassFrequency;
    [Range(0f,1f)]
    public float grassThreshold;
    [Range(0f, 1f)]
    public float slopeThreshold;
    public float scaleMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var element in terrains)
        {
            Mesh mesh = element.GetComponent<MeshFilter>().mesh;

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                //calculate slope
                Vector3 upVector = mesh.vertices[i] - planet.transform.position;
                float slope = Vector3.Angle(upVector, mesh.normals[i]) / 180;

                //calculate noise value
                float noiseVal = (float)NoiseS3D.Noise(mesh.vertices[i].x * grassFrequency, mesh.vertices[i].y * grassFrequency, mesh.vertices[i].z * grassFrequency);

                if ( slope <= slopeThreshold)
                {
                    //creates new grass
                    GameObject newGrass = Instantiate<GameObject>(grass);
                    newGrass.transform.parent = transform;

                    //place and rotates grass
                    newGrass.transform.position = element.transform.TransformPoint(mesh.vertices[i]);
                    newGrass.transform.rotation = Quaternion.FromToRotation(newGrass.transform.up, mesh.normals[i]);
                    newGrass.transform.Rotate(newGrass.transform.up, Random.Range(-35, 35));

                    Vector3 tmpScale = newGrass.transform.localScale;
                    tmpScale.x = tmpScale.x * scaleMultiplier * (noiseVal + 1) * 0.5f;
                    tmpScale.z = tmpScale.z * scaleMultiplier * (noiseVal + 1) * 0.5f;
                    tmpScale.y = tmpScale.y * scaleMultiplier * (noiseVal + 1) * 0.5f;
                    newGrass.transform.localScale = tmpScale;
                }
            }
        }
    }    
}
