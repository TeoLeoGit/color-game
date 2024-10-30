using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAttackPlanner : MonoBehaviour
{
    private List<List<Ground>> _grid;
    private int _columnCount = 0;
    private int _rowCount = 0;

    public void SetGrid(List<List<Ground>> grid, int columnCount, int rowCount)
    {
        _grid = grid;
        _columnCount = columnCount;
        _rowCount = rowCount;
    }

    public void StartGridAttack()
    {
        //Notify color
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _rowCount; j++)
            {
                _grid[i][j].WarnAttack();
            }
        }
        //Attack
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _rowCount; j++)
            {
                _grid[i][j].TriggerAttack();
            }
        }
    }

    public void StartPathAttack()
    {

    }

    public void StartBombAttack()
    {

    }
}
