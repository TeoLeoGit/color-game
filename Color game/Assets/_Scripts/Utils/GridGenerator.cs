using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab for each grid cell
    public int columns = 9;
    public int rows = 9;

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
                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
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