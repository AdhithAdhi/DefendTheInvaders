using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ObjectRotater : EditorWindow
{
    //TerrainData terrainData;
    [MenuItem("Custom Manager/Object Rotate")]
    static void Init()
    {
        ObjectRotater window =
            (ObjectRotater)EditorWindow.GetWindow(typeof(ObjectRotater));
        window.titleContent.text = "Object Rotate";
        //window.terrainData = Terrain.activeTerrain.terrainData;
    }
    void OnGUI()
    {


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Select Objet to Roate", MessageType.Warning, true);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Rotate", GUILayout.Height(30)))
        {
            RotetSelectedObjects();
            //FindTextures();
        }
        GUILayout.Space(10);
    }

    void RotetSelectedObjects()
    {
        foreach(GameObject gb in Selection.gameObjects)
        {
            foreach(Transform child in gb.transform)
            {
                //child.parent = null;
                child.rotation= Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
                //child.parent = gb.transform;
            }
        }
    }


}