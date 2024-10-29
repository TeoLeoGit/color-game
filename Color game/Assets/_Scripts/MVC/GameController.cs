using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public static event Action<Vector2, BlockType> OnChangeGroundColor;
    

    public static void ChangeGroundColor(Vector2 gridPos, BlockType type)
    {
        OnChangeGroundColor?.Invoke(gridPos, type);
    }
}
