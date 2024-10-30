using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public static event Action<Vector2, ColorType> OnChangeGroundColor;
    public static event Action<Vector2, ColorType> OnWarnAttackOnBlock;
    public static event Action<Vector2> OnAttackOnBlock;
    public static event Action<float> OnPlayerHealthUpdate;
    public static event Action<ColorType> OnChangePlayerColor;
    public static event Action<ColorType, float> OnDamagePlayer;



    public static void ChangeGroundColor(Vector2 gridPos, ColorType type)
    {
        OnChangeGroundColor?.Invoke(gridPos, type);
    }

    public static void WarnAttackOnBlock(Vector2 gridPos, ColorType type)
    {
        OnWarnAttackOnBlock?.Invoke(gridPos, type);
    }

    public static void AttackOnBlock(Vector2 gridPos)
    {
        OnAttackOnBlock?.Invoke(gridPos);
    }

    public static void UpdatePlayerHealth(float updateAmount)
    {
        OnPlayerHealthUpdate?.Invoke(updateAmount);
    }

    public static void ChangePlayerColor(ColorType colorType)
    {
        OnChangePlayerColor?.Invoke(colorType);
    }

    public static void DamagePlayer(ColorType colorType, float damage)
    {
        OnDamagePlayer?.Invoke(colorType, damage);
    }
}
