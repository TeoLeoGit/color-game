using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] SpriteRenderer _blockSprite;
    private BlockType _blockType;
    private Vector2 _gridPosition;

    private void Awake()
    {
        GameController.OnChangeGroundColor += ChangeBlockColor;
    }

    public void SetGrid(Vector2 gridPos)
    {
        _gridPosition = gridPos;
    }

    private void OnDestroy()
    {
        GameController.OnChangeGroundColor -= ChangeBlockColor;
    }

    private void ChangeBlockColor(Vector2 gridPos, BlockType newType)
    {
        if (_gridPosition == gridPos)
        {
            Debug.Log("dfsfds");
            _blockType = newType;
            _blockSprite.sprite = AssetManager.GetBlockSprite(newType);
        }
    }
}
