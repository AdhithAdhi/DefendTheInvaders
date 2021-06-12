using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEditor.AnimatedValues;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor
{
    public class MeshCombiner : EditorWindow
    {
       // public GameObject  = null;
       // public GameObject target = null;
        public Transform targetObject = null;
        public Transform EmptyObject = null;
        public bool is32bit = false;


        [MenuItem("Custom Manager/Mesh Combine")]
        static void Init()
        {
            MeshCombiner window =
                (MeshCombiner)EditorWindow.GetWindow(typeof(MeshCombiner));
            window.titleContent.text = "Mesh Combine";
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Target Object To Combine", EditorStyles.boldLabel);
            targetObject = EditorGUILayout.ObjectField("Target To Cobine", targetObject, typeof(Transform), true) as Transform;
            EditorGUILayout.LabelField("To store resulting combined mesh", EditorStyles.boldLabel);
            EmptyObject = EditorGUILayout.ObjectField("Result Store At", EmptyObject, typeof(Transform), true) as Transform;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            is32bit = EditorGUILayout.Toggle("Is32Bit", is32bit);
            EditorGUILayout.HelpBox("Enable this if you get Capacity exceed limit", MessageType.Warning, true);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);
            if (GUILayout.Button("Attach", GUILayout.Height(30)))
            {
                Attach();
            }
            GUILayout.Space(10);
        }
        void Attach()
        {
            if (targetObject == null)
                return;
            CombineMeshes();


        }

        public void CombineMeshes()
        {
            List<Material> materials = new List<Material>();
            MeshFilter[] meshFilters = targetObject.GetComponentsInChildren<MeshFilter>();
            //Debug.LogError(meshFilters.Length);
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                var mat = targetObject.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial;
                if (!materials.Contains(mat))
                    materials.Add(mat);
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }
            var format = is32bit ? IndexFormat.UInt32 : IndexFormat.UInt16;
            Mesh combinedMesh = new Mesh { indexFormat = format };
            combinedMesh.name = EmptyObject.name;
            EmptyObject.gameObject.AddComponent<MeshFilter>().mesh = combinedMesh;
            EmptyObject.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, true);
            var smr = EmptyObject.gameObject.AddComponent<MeshRenderer>();
            //smr = EmptyObject.gameObject.GetComponent<MeshFilter>().mesh;

            smr.sharedMaterials = materials.ToArray();
            EmptyObject.gameObject.SetActive(true);
            targetObject.gameObject.SetActive(false);

        }
    }

    
}
