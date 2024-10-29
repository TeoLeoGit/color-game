using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Yellow = 1,
    Red = 2,
}

public class AssetManager : MonoBehaviour
{
    [SerializeField] AssetData assetData;

    private static Dictionary<BlockType, Sprite> _blockSpriteMap = new();

    public static Sprite GetBlockSprite(BlockType type)
    {
        return _blockSpriteMap[type];
    }

    private void Awake()
    {
        foreach (var data in assetData.GetBlockSpriteData())
        {
            _blockSpriteMap.Add(data.blockType, data.blockSprite);
        }
    }
}
