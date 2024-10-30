using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetData", menuName = "ScriptableObjects/AssetData")]
public class AssetData : ScriptableObject
{
    [SerializeField] List<BlockSprite> _blockSprites = new List<BlockSprite>();

    public List<BlockSprite> GetBlockSpriteData()
    {
        return _blockSprites;
    }
}

[Serializable]
public class BlockSprite
{
    public ColorType blockType;
    public Sprite blockSprite;
} 