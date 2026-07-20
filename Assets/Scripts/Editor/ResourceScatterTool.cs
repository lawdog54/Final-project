using UnityEngine;
using UnityEditor;

public class ResourceScatterTool : EditorWindow
{
    private Terrain targetTerrain;

    private GameObject prefabToScatter;

    private Transform parentObject;

    private int amount = 20;

    private float minDistance = 8f;

    [MenuItem("Tools/Terrain/Resource Scatter Tool")]

    public static void ShowWindow()
    {
        GetWindow<ResourceScatterTool>("Resource Scatter Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scatter Resources on Terrain", EditorStyles.boldLabel);
        targetTerrain = (Terrain)EditorGUILayout.ObjectField("Target Terrain", targetTerrain, typeof(Terrain), true);
        prefabToScatter = (GameObject)EditorGUILayout.ObjectField("Prefab to Scatter", prefabToScatter, typeof(GameObject), false);
        parentObject = (Transform)EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(Transform), true);
        amount = EditorGUILayout.IntField("Amount", amount);
        minDistance = EditorGUILayout.FloatField("Minimum Distance", minDistance);

        if (GUILayout.Button("Scatter Resources"))
        {
            ScatterResources();
        }

    }

    private void ScatterResources()
    {
        if (targetTerrain == null || prefabToScatter == null ||parentObject == null)
        {
            Debug.LogError("Please assign a target terrain, a prefab to scatter, and a parent object.");
            return;
        }

       TerrainData terrainData = targetTerrain.terrainData;
       Vector3 terrainPosition = targetTerrain.transform.position;
        Vector3 terrainSize = terrainData.size;
        int placedCount = 0;
        int attempts = 0;
        int maxAttempts = amount * 50; // Limit the number of attempts to avoid infinite loops

        while (placedCount < amount && attempts < maxAttempts)
        {
            attempts++;
            float randomX = Random.Range(0f, terrainSize.x);
            float randomZ = Random.Range(0f, terrainSize.z);

            float y = terrainData.GetInterpolatedHeight(randomX / terrainSize.x, randomZ / terrainSize.z);

            Vector3 spawnPosition = new Vector3(terrainPosition.x + randomX, terrainPosition.y + y, terrainPosition.z + randomZ);

            if (!IsFarEnough(spawnPosition))
            {
                continue;
            }
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefabToScatter);
            Undo.RegisterCreatedObjectUndo(newObject, "Scatter Resource");
            newObject.transform.position = spawnPosition;
            newObject.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            newObject.transform.SetParent(parentObject);

            placedCount++;
           
            
        }
        Debug.Log("placed " + placedCount + " resources");

       
    }

    private bool IsFarEnough(Vector3 position)
    {
        foreach (Transform child in parentObject)
        {
            if (Vector3.Distance(position, child.position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }


}
