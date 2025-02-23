using System.Collections.Generic;
using UnityEngine;

public class RowGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject rowPrefab;

    [SerializeField]
    private float distanceBetweenRows = 10f;

    [SerializeField]
    private int initialRows = 5;

    [SerializeField]
    private int maxRows = 10;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float destroyDistance = 20f; // Distance behind player before destroying

    private float nextSpawnZ = 0f;
    private Queue<GameObject> activeRows = new Queue<GameObject>();

    private void Start()
    {
        // Generate initial rows
        for (int i = 0; i < initialRows; i++)
        {
            SpawnRow();
        }
    }

    private void Update()
    {
        // Spawn new rows ahead
        if (
            player.position.z + (distanceBetweenRows * 2)
            > nextSpawnZ - (maxRows * distanceBetweenRows)
        )
        {
            SpawnRow();
        }

        // Remove old rows that are far behind the player
        while (activeRows.Count > 0)
        {
            GameObject oldestRow = activeRows.Peek();
            if (oldestRow.transform.position.z < player.position.z - destroyDistance)
            {
                activeRows.Dequeue();
                Destroy(oldestRow);
            }
            else
            {
                break;
            }
        }
    }

    private void SpawnRow()
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, nextSpawnZ);
        GameObject newRow = Instantiate(rowPrefab, spawnPosition, Quaternion.identity);
        activeRows.Enqueue(newRow);
        nextSpawnZ += distanceBetweenRows;
    }
}
