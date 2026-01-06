using UnityEngine;
using UnityEditor; 

public class LevelGenerator : EditorWindow
{

    GameObject brickPrefab;
    GameObject obstaclePrefab;
    Transform parentContainer; 

    float pathWidth = 4f; 
    float startZ = 10f;  
    float endZ = 100f;   
    int itemCount = 50;  

    [Range(0, 1)]
    float obstacleRatio = 0.3f; 

    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Gen");
    }

    void OnGUI()
    {
        GUILayout.Label("Config Level", EditorStyles.boldLabel);

        brickPrefab = (GameObject)EditorGUILayout.ObjectField("Brick Prefab", brickPrefab, typeof(GameObject), false);
        obstaclePrefab = (GameObject)EditorGUILayout.ObjectField("Obstacle Prefab", obstaclePrefab, typeof(GameObject), false);
        parentContainer = (Transform)EditorGUILayout.ObjectField("Parent Container", parentContainer, typeof(Transform), true);

        GUILayout.Space(10);

        pathWidth = EditorGUILayout.FloatField("Path Width (X)", pathWidth);
        startZ = EditorGUILayout.FloatField("Start Z", startZ);
        endZ = EditorGUILayout.FloatField("End Z", endZ);
        itemCount = EditorGUILayout.IntField("Item Count", itemCount);
        obstacleRatio = EditorGUILayout.Slider("Obstacle Ratio", obstacleRatio, 0f, 1f);

        GUILayout.Space(20);

        if (GUILayout.Button("GENERATE LEVEL", GUILayout.Height(40)))
        {
            GenerateLevel();
        }

        if (GUILayout.Button("CLEAR ALL CHILDREN", GUILayout.Height(30)))
        {
            ClearChildren();
        }
    }

    void GenerateLevel()
    {
        if (brickPrefab == null || obstaclePrefab == null)
        {
            Debug.LogError("Prefab is missing! My friend.");
            return;
        }

        if (parentContainer == null)
        {
            GameObject go = new GameObject("Level_Objects");
            parentContainer = go.transform;
            Undo.RegisterCreatedObjectUndo(go, "Create Parent");
        }

        for (int i = 0; i < itemCount; i++)
        {
            float randomX = Random.Range(-pathWidth, pathWidth);
            float randomZ = Random.Range(startZ, endZ);
            Vector3 spawnPos = new Vector3(randomX, 0.5f, randomZ); 

            GameObject prefabToSpawn = (Random.value < obstacleRatio) ? obstaclePrefab : brickPrefab;

            GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn, parentContainer);
            newObj.transform.position = spawnPos;

            if (prefabToSpawn == obstaclePrefab)
            {
                newObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            Undo.RegisterCreatedObjectUndo(newObj, "Spawn Object");
        }

        Debug.Log($"Set up {itemCount} item -> Done!");
    }

    void ClearChildren()
    {
        if (parentContainer == null) return;

        Selection.activeGameObject = null;

        EditorApplication.delayCall += () =>
        {
            if (parentContainer == null) return; 
            for (int i = parentContainer.childCount - 1; i >= 0; i--)
            {
                Transform child = parentContainer.GetChild(i);
                if (child != null)
                {
                    Undo.DestroyObjectImmediate(child.gameObject);
                }
            }

            Debug.Log("Clear All!");
        };
    }
}