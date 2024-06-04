using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    private const int Size = 3;
    private Image[,] _cells;
    [SerializeField] private Color _normalCell = Color.white;
    [SerializeField] private Color _selectedCell = Color.cyan;
    [SerializeField] private Sprite _circle = null;
    [SerializeField] private Sprite _cross = null;
    
    private int _selectedRow;
    private int _selectedColumn;

    /// <summary>
    /// セルの状態
    /// </summary>
    private CellState[,] _cellsState;
    public enum CellState
    {
        Empty,
        Circle,
        Cross
    }

    private TurnState _currentTurn;
    public enum TurnState
    {
        Circle,
        Cross
    }

    private bool IsJudge = false;
    void Start()
    {
        _currentTurn = TurnState.Circle;
        _cells = new Image[Size, Size];
        _cellsState = new CellState[Size, Size];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r},{c})");
                obj.transform.parent = transform;
                var cell = obj.AddComponent<Image>();
                _cells[r, c] = cell;
            }
        }
    }
    
    void Update()
    {
        if (IsJudge)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selectedColumn--; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { _selectedColumn++; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { _selectedRow--; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { _selectedRow++; }

        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= Size) { _selectedColumn = Size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= Size) { _selectedRow = Size - 1; }

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = _cells[r, c];
                cell.color =
                    (r == _selectedRow && c == _selectedColumn)
                        ? _selectedCell : _normalCell;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawMark();
            WinJudge();
        }
    }

    /// <summary>
    /// 記号を描画する
    /// </summary>
    private void DrawMark()
    {
        if (_cellsState[_selectedRow, _selectedColumn] == CellState.Empty)
        {
            if (_currentTurn == TurnState.Circle)
            {
                _cells[_selectedRow, _selectedColumn].sprite = _circle;
                _cellsState[_selectedRow, _selectedColumn] = CellState.Circle;
                _currentTurn = TurnState.Cross;
            }
            else
            {
                _cells[_selectedRow, _selectedColumn].sprite = _cross;
                _cellsState[_selectedRow, _selectedColumn] = CellState.Cross;
                _currentTurn = TurnState.Circle;
            }
        }
    }

    /// <summary>
    /// 勝敗の判定をするメソッド
    /// </summary>
    private void WinJudge()
    {
        
    }
}
