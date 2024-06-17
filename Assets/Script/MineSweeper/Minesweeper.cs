using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Minesweeper : MonoBehaviour
{
    [SerializeField] private int _row = 1;
    [SerializeField] private int _column = 1;
    [SerializeField] private int _mineCount = 10;
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
        
        for (var i = 0; i < _mineCount; i++)
        {
            while (true)
            {
                var r = Random.Range(0, _row);
                var c = Random.Range(0, _column);
                if (_cells[r, c].CellState != CellState.Mine)
                {
                    var cell = _cells[r, c];
                    cell.CellState = CellState.Mine;
                    break; 
                } //地雷が被らないようにする
            }
        }
    }
}
