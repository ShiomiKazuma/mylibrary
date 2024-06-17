using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    [SerializeField] private int _row = 1;
    [SerializeField] private int _column = 1;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField] private Cell _cellPrefab = null;

    private void Start()
    {
        var _cells = new Cell[_row, _column];
        var parent = _gridLayoutGroup.gameObject.transform;
        for (int r = 0; r < _row; r++)
        {
            for (int c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
    }
}
