using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        // Setting a target script for custom editor.
        Spawner spawner = (Spawner)target;

        EditorGUILayout.LabelField("Set spawn period of the first object to all", EditorStyles.boldLabel);
        spawner.allSpawnPeriod = EditorGUILayout.Toggle("All Spawn Period", spawner.allSpawnPeriod);

        EditorGUILayout.Space(10);

        int listSize = spawner.MyList.Count;
        listSize = EditorGUILayout.IntField("List Size", listSize);

        // Check for local list size is equal to real spawn objects list size. 
        if (listSize != spawner.MyList.Count)
        {
            // Adding objects in list.
            while (listSize > spawner.MyList.Count)
            {
                SpawnObject sample = new SpawnObject();
                spawner.MyList.Insert(spawner.MyList.Count, sample);
            }
            // Or deleting objects from list.
            while (listSize < spawner.MyList.Count)
            {
                spawner.MyList.RemoveAt(spawner.MyList.Count - 1);
            }
        }
        
        for (int i=0; i < spawner.MyList.Count; i++)
        {
            // Creating fields for gameobject, spawn position and spawn period.
            GameObject buf = (GameObject)EditorGUILayout.ObjectField(spawner.MyList[i].obj, typeof(GameObject), true);
            spawner.MyList[i].spawnPos = (Transform)EditorGUILayout.ObjectField(spawner.MyList[i].spawnPos, typeof(Transform), true);
            spawner.MyList[i].spawnPeriod = EditorGUILayout.FloatField("Spawn Period", spawner.MyList[i].spawnPeriod);

            // Checking for adding a gameobject in field.
            if (buf != null)
            {
                Enemy enemy = buf.GetComponent<Enemy>();
                if (enemy != null && enemy.parameters.Count > 7)
                {
                    // If current object is different from the previous known object in list,
                    // then set new default parameters to it.
                    if (buf != spawner.MyList[i].obj)
                    {
                        spawner.MyList[i].p1Name = enemy.parameters[0].name;
                        spawner.MyList[i].p1Value = enemy.parameters[0].value;
                        spawner.MyList[i].p2Name = enemy.parameters[1].name;
                        spawner.MyList[i].p2Value = enemy.parameters[1].value;
                        spawner.MyList[i].p3Name = enemy.parameters[2].name;
                        spawner.MyList[i].p3Value = enemy.parameters[2].value;
                        spawner.MyList[i].p4Name = enemy.parameters[3].name;
                        spawner.MyList[i].p4Value = enemy.parameters[3].value;
                        spawner.MyList[i].p5Name = enemy.parameters[4].name;
                        spawner.MyList[i].p5Value = enemy.parameters[4].value;
                        spawner.MyList[i].p6Name = enemy.parameters[5].name;
                        spawner.MyList[i].p6Value = enemy.parameters[5].value;
                        spawner.MyList[i].p7Name = enemy.parameters[6].name;
                        spawner.MyList[i].p7Value = enemy.parameters[6].value;
                        spawner.MyList[i].p8Name = enemy.parameters[7].name;
                        spawner.MyList[i].p8Value = enemy.parameters[7].value;
                    }

                    // Setting gameobject from field to list.
                    spawner.MyList[i].obj = buf;

                    // Setting fields for every object's parameter.
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p1Name);
                    spawner.MyList[i].p1Value = EditorGUILayout.FloatField(spawner.MyList[i].p1Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p2Name);
                    spawner.MyList[i].p2Value = EditorGUILayout.FloatField(spawner.MyList[i].p2Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p3Name);
                    spawner.MyList[i].p3Value = EditorGUILayout.FloatField(spawner.MyList[i].p3Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p4Name);
                    spawner.MyList[i].p4Value = EditorGUILayout.FloatField(spawner.MyList[i].p4Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p5Name);
                    spawner.MyList[i].p5Value = EditorGUILayout.FloatField(spawner.MyList[i].p5Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p6Name);
                    spawner.MyList[i].p6Value = EditorGUILayout.FloatField(spawner.MyList[i].p6Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p7Name);
                    spawner.MyList[i].p7Value = EditorGUILayout.FloatField(spawner.MyList[i].p7Value);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(spawner.MyList[i].p8Name);
                    spawner.MyList[i].p8Value = EditorGUILayout.FloatField(spawner.MyList[i].p8Value);
                    GUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.Space(10);
        }
        // If something was changed, then mark as dirty (unsaved).
        if (GUI.changed) SetObjectDirty(spawner.gameObject);
    }

    // Marks object and scene as dirty (allows to save changes).
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}