using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
    public Ground cellPrefab; // Prefab for each grid cell
    public int columns = 9;
    public int rows = 9;
    [SerializeField] GridAttackPlanner _attackPlanner;

    public void GenerateGrid()
    {
        // Clear existing children
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Generate grid
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                var instant = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                instant.SetGrid(x, y);
            }
        }
        _attackPlanner.SetGrid(columns, rows);
    }
}

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridGenerator gridGenerator = (GridGenerator)target;
        if (GUILayout.Button("ADD"))
        {
            gridGenerator.GenerateGrid();
        }
    }
}