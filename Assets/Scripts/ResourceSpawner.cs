using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public Terrain targetTerrain;
    public GameObject prefabToScatter;
    public Transform parentObject;

    public int amount = 20;
    public float minDistance = 8f;

    void Start()
    {
        ClearResources();
        ScatterResources();
    }

    void ScatterResources()
    {
        TerrainData terrainData = targetTerrain.terrainData;
        Vector3 terrainPosition = targetTerrain.transform.position;
        Vector3 terrainSize = terrainData.size;

        int placedCount = 0;
        int attempts = 0;
        int maxAttempts = amount * 50;

        while (placedCount < amount && attempts < maxAttempts)
        {
            attempts++;

            float randomX = Random.Range(0f, terrainSize.x);
            float randomZ = Random.Range(0f, terrainSize.z);

            float y = terrainData.GetInterpolatedHeight(
                randomX / terrainSize.x,
                randomZ / terrainSize.z);

            Vector3 spawnPosition = new Vector3(
                terrainPosition.x + randomX,
                terrainPosition.y + y,
                terrainPosition.z + randomZ);

            if (!IsFarEnough(spawnPosition))
                continue;

            GameObject obj = Instantiate(
                prefabToScatter,
                spawnPosition,
                Quaternion.Euler(0f, Random.Range(0f, 360f), 0f),
                parentObject);

            placedCount++;
        }

        Debug.Log("Spawned " + placedCount + " resources.");
    }

    bool IsFarEnough(Vector3 position)
    {
        foreach (Transform child in parentObject)
        {
            if (Vector3.Distance(position, child.position) < minDistance)
                return false;
        }

        return true;
    }


    void ClearResources()
{
    for (int i = parentObject.childCount - 1; i >= 0; i--)
    {
        Destroy(parentObject.GetChild(i).gameObject);
    }
}
}