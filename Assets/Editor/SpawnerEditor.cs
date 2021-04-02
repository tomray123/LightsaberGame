using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Spawner spawner = (Spawner)target;

        EditorGUILayout.LabelField("Set spawn period of the first object to all", EditorStyles.boldLabel);
        spawner.allSpawnPeriod = EditorGUILayout.Toggle("All Spawn Period", spawner.allSpawnPeriod);

        /*EditorGUILayout.LabelField("Set time before the first shot of the first object to all", EditorStyles.boldLabel);
        spawner.allTimeToInitialShoot = EditorGUILayout.Toggle("All Time To Initial Shoot", spawner.allTimeToInitialShoot);

        EditorGUILayout.LabelField("Set time interval for shooting of the first object to all", EditorStyles.boldLabel);
        spawner.allShootPeriod = EditorGUILayout.Toggle("All Shoot Period", spawner.allShootPeriod);*/

        EditorGUILayout.Space(10);

        int listSize = spawner.MyList.Count;
        listSize = EditorGUILayout.IntField("List Size", listSize);

        if (listSize != spawner.MyList.Count)
        {
            while (listSize > spawner.MyList.Count)
            {
                SpawnObject sample = new SpawnObject();
                spawner.MyList.Insert(spawner.MyList.Count, sample);
            }
            while (listSize < spawner.MyList.Count)
            {
                spawner.MyList.RemoveAt(spawner.MyList.Count - 1);
            }
        }
        /*
        GameObject obj1 = spawner.MyList[0].obj;
        GameObject obj2 = spawner.MyList[1].obj; ;
        obj1 = (GameObject)EditorGUILayout.ObjectField(obj1, typeof(GameObject), false);
        spawner.MyList[0].obj = obj1;
        obj2 = (GameObject)EditorGUILayout.ObjectField(obj2, typeof(GameObject), false);
        spawner.MyList[1].obj = obj2;
        */
        
        for (int i=0; i < spawner.MyList.Count; i++)
        {
            GameObject buf = (GameObject)EditorGUILayout.ObjectField(spawner.MyList[i].obj, typeof(GameObject), true);
            spawner.MyList[i].spawnPos = (Transform)EditorGUILayout.ObjectField(spawner.MyList[i].spawnPos, typeof(Transform), true);
            spawner.MyList[i].spawnPeriod = EditorGUILayout.FloatField("Spawn Period", spawner.MyList[i].spawnPeriod);
            if (buf != null)
            {
                Enemy enemy = buf.GetComponent<Enemy>();
                if (enemy != null && enemy.parameters.Count > 7)
                {

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

                    spawner.MyList[i].obj = buf;

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

    }
}