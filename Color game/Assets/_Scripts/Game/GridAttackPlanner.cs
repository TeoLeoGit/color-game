using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GridAttackPlanner : MonoBehaviour
{
    [SerializeField] private int _columnCount;
    [SerializeField] private int _rowCount;

    public void SetGrid(int columns, int rows)
    {
        _columnCount = columns;
        _rowCount = rows;
    }

    private void Start()
    {
        StartCoroutine(IStartGridAttack());
    }

   IEnumerator IStartGridAttack()
    {
        //Notify color
        for (int i = 0; i < _columnCount; i++)
        {
            StartCoroutine(IWarnAttackOnColumn(i));
        }

        yield return new WaitForSeconds(0.2f * _columnCount); 
        //Attack
        for (int i = 0; i < _columnCount; i++)
        {
            for (int j = 0; j < _columnCount; j++)
            {
                StartCoroutine(IAttackOnColumn(i));
            }
        }
    }

    IEnumerator IWarnAttackOnColumn(int column)
    {
        var wait = new WaitForSeconds(0.3f);
        var blockType = (BlockType)Random.Range(1, 5);
        for (int j = 0; j < _rowCount; j++)
        {
            GameController.WarnAttackOnBlock(new Vector2(column, j), blockType);
            yield return wait;
        }
    }

    IEnumerator IAttackOnColumn(int column)
    {
        var wait = new WaitForSeconds(0.15f);
        for (int j = 0; j < _rowCount; j++)
        {
            GameController.AttackOnBlock(new Vector2(column, j));
            yield return wait;
        }
    }

    public void StartPathAttack()
    {

    }

    public void StartBombAttack()
    {

    }
}
