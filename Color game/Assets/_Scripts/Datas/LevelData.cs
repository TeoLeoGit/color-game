using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    OnColumn,
    OnRow,
    Bomb
}

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] List<AttackPattern> _attackPatterns;
}

[Serializable]
public class AttackPattern 
{
    public AttackType type;
}

