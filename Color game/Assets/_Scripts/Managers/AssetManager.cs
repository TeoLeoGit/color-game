using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Yellow = 1,
    Red = 2,
    Green = 3,
    Blue = 4,
}

public class AssetManager : MonoBehaviour
{
    [SerializeField] AssetData assetData;

    private static Dictionary<BlockType, Sprite> _blockSpriteMap = new();
    private static Dictionary<BlockType, Sprite> _blockNotiColor = new();
    private static Dictionary<BlockType, Color> _blockTypeToColor = new();

    
    public static Sprite GetBlockSprite(BlockType type)
    {
        return _blockSpriteMap[type];
    }

    public static Color GetBlockColor(BlockType type)
    {
        return _blockTypeToColor[type];
    }

    private void Awake()
    {
        foreach (var data in assetData.GetBlockSpriteData())
        {
            _blockSpriteMap.Add(data.blockType, data.blockSprite);
        }

        _blockTypeToColor.Add(BlockType.Yellow, Color.yellow);
        _blockTypeToColor.Add(BlockType.Red, Color.red);
        _blockTypeToColor.Add(BlockType.Green, Color.green);
        _blockTypeToColor.Add(BlockType.Blue, Color.blue);
    }
}
