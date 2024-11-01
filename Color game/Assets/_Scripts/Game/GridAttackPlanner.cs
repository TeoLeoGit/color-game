using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GridAttackPlanner : MonoBehaviour
{
    [SerializeField] private int _columnCount;
    [SerializeField] private int _rowCount;

    public List<Transform> firstRowCells = new();

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
        while (true)
        {
            GameController.CallBossAttacK(firstRowCells);
            yield return new WaitForSeconds(15);
            continue;
            //Change player color.
            ColorType playerColor = (ColorType)Random.Range(1, 5);
            GameController.ChangePlayerColor(playerColor);

            for (int i = 0; i < _columnCount; i++)
            {
                StartCoroutine(IWarnAttackOnColumn(i));
            }

            yield return new WaitForSeconds(0.2f * _columnCount); 
            //Attack
            for (int i = 0; i < _columnCount; i++)
            {
                StartCoroutine(IAttackOnColumn(i));
            }
            yield return new WaitForSeconds(0.2f * _columnCount);
        }
    }

    IEnumerator IWarnAttackOnColumn(int column)
    {
        var wait = new WaitForSeconds(0.3f);
        var blockType = (ColorType)Random.Range(1, 5);
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
