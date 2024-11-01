using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float tossHeight = 0.25f; // The height of the toss
    public float duration = 0.2f;   // Total duration of the tween

    [SerializeField] SpriteRenderer _blockSprite;
    [SerializeField] SpriteRenderer _paletteSprite;
    [SerializeField] Transform _spritesTransform;


    [SerializeField] LayerMask _damagableLayer;
    private ColorType _blockColorType;
    private float _glitchDuration = 0.1f; // Time between color changes
    public int _gridX;
    public int _gridY;

    private void Awake()
    {
        GameController.OnChangeGroundColor += ChangeBlockColor;
        GameController.OnWarnAttackOnBlock += WarnAttack;
        GameController.OnAttackOnBlock += TriggerAttack;
    }

    public void SetGrid(int x, int y)
    {
        _gridX = x;
        _gridY = y;
        _blockSprite.sortingOrder = -y;
        _paletteSprite.sortingOrder = -y + 1;
    }

    private void OnDestroy()
    {
        GameController.OnChangeGroundColor -= ChangeBlockColor;
        GameController.OnWarnAttackOnBlock -= WarnAttack;
        GameController.OnAttackOnBlock -= TriggerAttack;
    }

    public void WarnAttack(Vector2 gridPos, ColorType blockType)
    {
        if (_gridX == gridPos.x && _gridY == gridPos.y)
        {
            _blockColorType = blockType;
            _paletteSprite.color = AssetManager.GetBlockColor(blockType);
            StartCoroutine(IGlitch());
        }
    }

    public void CallColumnAttack()
    {
        GameController.CallGridCollumnAttack(_gridX);
    }

    private void ChangeBlockColor(Vector2 gridPos, ColorType newType)
    {
        if (_gridX == gridPos.x && _gridY == gridPos.y)
        {
            _blockColorType = newType;
            _paletteSprite.sprite = AssetManager.GetBlockSprite(newType);
        }
    }

    private IEnumerator IGlitch()
    {
        var blockColor = _paletteSprite.color;
        for (int i = 0; i < 2; i++)
        {
            _paletteSprite.color = Color.white;
            yield return new WaitForSeconds(_glitchDuration);
            _paletteSprite.color = blockColor;
            yield return new WaitForSeconds(_glitchDuration);
        }
    }

    public void TriggerAttack(Vector2 gridPos)
    {
        if (_gridX == (int)gridPos.x && _gridY == (int)gridPos.y)
        {
            TossAndFall();
        }
    }

    void TossAndFall()
    {
        Vector3 originalPosition = _spritesTransform.position;

        // Create a sequence for the toss and fall effect
        Sequence sequence = DOTween.Sequence();

        // Tween to move up
        sequence.Append(_spritesTransform.DOMoveY(originalPosition.y + tossHeight, duration / 2)
            .SetEase(Ease.OutQuad));

        // Tween to move down
        sequence.Append(_spritesTransform.DOMoveY(originalPosition.y, duration / 2)
            .SetEase(Ease.InQuad));

        CheckAndDamagePlayer();
    }

    void CheckAndDamagePlayer()
    {
        var hit = Physics2D.BoxCast(transform.position, Vector2.one, 0f, Vector2.right, 0f, _damagableLayer);
        if (hit.collider != null)
        {
            GameController.DamagePlayer(_blockColorType, 2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector2.one);
    }
}
