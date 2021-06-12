using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ObjectSpwaner : EditorWindow
{
    public static ObjectSpwaner Instance = null;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }
    Vector2 vector2;
    public List<SpwanObject> spwanObjects= new List<SpwanObject>();
    //TerrainData terrainData;
    [MenuItem("Custom Manager/Objects")]
    static void Init()
    {
        ObjectSpwaner window =
            (ObjectSpwaner)EditorWindow.GetWindow(typeof(ObjectSpwaner));
        window.titleContent.text = "Object Spwaner";
        //window.terrainData = Terrain.activeTerrain.terrainData;
    }
    bool loded = false;
    void OnGUI()
    {
        if (spwanObjects.Count == 0)
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Add Textures in Unity Terrain field to see it in here.....", MessageType.Warning, true);
            EditorGUILayout.EndHorizontal(); 
            if (GUILayout.Button("Create New Object", GUILayout.Height(30)))
            {
                loded = false;
                spwanObjects.Add(new SpwanObject());
                //FindTextures();
            }
            return;
        }
        //terrain = Terrain.activeTerrain;
        //terrainData = Terrain.activeTerrain.terrainData;
        EditorGUILayout.BeginVertical();
        vector2 = EditorGUILayout.BeginScrollView(vector2, false, true);
        for (int i = 0; i < spwanObjects.Count; i++)
        {
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.BeginVertical();
            spwanObjects[i].Object = EditorGUILayout.ObjectField("Object " + (i + 1), spwanObjects[i].Object, typeof(GameObject), true) as GameObject;
            //spwanObjects[i].MaxSlopeValue = EditorGUILayout.IntField("Max Slope Differance", spwanObjects[i].MaxSlopeValue); 
            spwanObjects[i].MaxSpwanCount = EditorGUILayout.IntField("Spwan Count", spwanObjects[i].MaxSpwanCount);
            if (GUILayout.Button("Spwan This Object", GUILayout.Height(30)))
            {
                FindParentForObj();
                for (int j = 0; j < spwanObjects[i].MaxSpwanCount; j++)
                {
                    //FindALLFlatSufaceWithRadius(10, spwanObjects[i]);

                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create New Object", GUILayout.Height(30)))
        {
            loded = false;
            //FindTextures();
        }
        if (GUILayout.Button("Remove All Object", GUILayout.Height(30)))
        {
            loded = false;
            RemoveAllObjects();
            //FindTextures();
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Load All Object", GUILayout.Height(30)))
        {
            loded = false;
            //FindTextures();
        }
        if (GUILayout.Button("Spwan All Object", GUILayout.Height(30)))
        {
            FindParentForObj();
            for (int j = 0; j < spwanObjects.Count; j++)
            {
                for (int k = 0; k < spwanObjects[j].MaxSpwanCount; k++)
                {
                    //FindALLFlatSufaceWithRadius(10, spwanObjects[j]);
                }


            }

        }
        GUILayout.Space(10);
    }

    private void RemoveAllObjects()
    {
        FindParentForObj();
        foreach (Transform gObj in SpwanObjParent)
        {
            DestroyImmediate(gObj.gameObject);
        }
    }

    Terrain terrain ;
    System.Random rand = new System.Random();
    Transform SpwanObjParent;
    int SpwanSlope;
    void FindALLFlatSufaceWithRadius(int radius,SpwanObject objSpwan)
    {
        ////SpwanSlope = objSpwan.MaxSlopeValue;
        //// terrain size x
        //int terrainWidth = (int)terrainData.size.x;
        //// terrain size z
        //int terrainLength = (int)terrainData.size.z;
        //// terrain x position
        //int terrainPosX = (int)terrain.transform.position.x;
        //// terrain z position
        //int terrainPosZ = (int)terrain.transform.position.z;

        ////Debug.LogError(" size is " + terrainWidth + " / " + terrainLength);
        //int posx, posz;
        //posx = rand.Next(terrainPosX, terrainPosX + terrainWidth);
        //// generate random z position
        //posz = rand.Next(terrainPosZ, terrainPosZ + terrainLength);
        


        //return FlatPosition;
    }



    void FindParentForObj()
    {
       GameObject ParentObj = GameObject.Find("SpwanedObjects");

        if (SpwanObjParent == null)
        {
            SpwanObjParent = new GameObject("SpwanedObjects").transform;
        }
        else
            SpwanObjParent= ParentObj.transform;
    }



}
[System.Serializable]
public class SpwanObject
{
    public Texture2D SpwanObjectTexture;
    public GameObject Object;
    public int MaxSpwanCount = 1000;
    public float MinScaleSize = .5f;
    public float MaxScaleSize = 2f;
    public float ScaleSize = 1f;
    //public float TargetStrength = .5f;
    //public int MaxSlopeValue;
    //public bool textureNeed = false;
    //public int textureIndex = 0;
}
