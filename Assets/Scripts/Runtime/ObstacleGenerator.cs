using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Transform> obstaclePoints;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private float obstacleSpawnChance = 0.3f;

    private Dictionary<float, List<Transform>> rowPoints = new Dictionary<float, List<Transform>>();
    private int lastSafeColumn = 1; // Start in center

    private void Start()
    {
        // Group points by Z position (rows)
        foreach (Transform point in obstaclePoints)
        {
            float zPos = point.position.z;
            if (!rowPoints.ContainsKey(zPos))
            {
                rowPoints[zPos] = new List<Transform>();
            }
            rowPoints[zPos].Add(point);
        }

        // Sort rows by Z position
        List<float> sortedZPositions = new List<float>(rowPoints.Keys);
        sortedZPositions.Sort();

        // Generate obstacles for each row in order

        if (transform.position.z == 0)
        {
            return;
        }
        foreach (float zPos in sortedZPositions)
        {
            GenerateObstaclesForRow(rowPoints[zPos]);
        }
    }

    private void GenerateObstaclesForRow(List<Transform> row)
    {
        if (row.Count != 3)
            return;

        // Sort points by X position (left to right)
        row.Sort((a, b) => a.position.x.CompareTo(b.position.x));

        // Determine next safe column ensuring it's reachable
        int nextSafeColumn = GetNextSafeColumn();
        lastSafeColumn = nextSafeColumn;

        // Place obstacles with random chance in non-safe columns
        for (int i = 0; i < 3; i++)
        {
            if (i != nextSafeColumn && Random.value < obstacleSpawnChance)
            {
                Vector3 spawnPosition = row[i].position;
                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private int GetNextSafeColumn()
    {
        List<int> possibleColumns = new List<int>();

        // Can only move one column left or right from current position
        if (lastSafeColumn > 0)
        {
            possibleColumns.Add(lastSafeColumn - 1); // Can move left
        }
        possibleColumns.Add(lastSafeColumn); // Can stay in current column
        if (lastSafeColumn < 2)
        {
            possibleColumns.Add(lastSafeColumn + 1); // Can move right
        }

        // Return random possible column
        return possibleColumns[Random.Range(0, possibleColumns.Count)];
    }
}
