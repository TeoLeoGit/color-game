using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public static event Action<Vector2, BlockType> OnChangeGroundColor;
    public static event Action<Vector2, BlockType> OnWarnAttackOnBlock;
    public static event Action<Vector2> OnAttackOnBlock;



    public static void ChangeGroundColor(Vector2 gridPos, BlockType type)
    {
        OnChangeGroundColor?.Invoke(gridPos, type);
    }

    public static void WarnAttackOnBlock(Vector2 gridPos, BlockType type)
    {
        OnWarnAttackOnBlock?.Invoke(gridPos, type);
    }

    public static void AttackOnBlock(Vector2 gridPos)
    {
        OnAttackOnBlock?.Invoke(gridPos);
    }
}
