using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetController))]
public class PlanetEditor : Editor
{
    PlanetController planet;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //shows in editor the PlanetProperties as a menu
        Editor editor = CreateEditor(planet.planetProperties);
        editor.OnInspectorGUI();
        

        //regenerate the mesh
        planet.UpdateMesh();
    }

    private void OnEnable()
    {
        planet = (PlanetController) target;    
    }
}
