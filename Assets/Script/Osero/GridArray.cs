using UnityEngine;
using UnityEngine.UI;

public class GridArray : MonoBehaviour
{
    [SerializeField] int _row = 5;
    [SerializeField] int _column = 5;
    private int _currentRow = 0;
    private int _currentColumn = 0;
    private int _preRow = 0;
    private int _preColumn = 0;
    int[,] _grid;
    Image[,] _gridImage;
    private void Start()
    {
        _grid = new int[_row, _column];
        _gridImage = new Image[_row, _column];
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                _gridImage[c, r] = image;
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            Check("left");
            CheckGrid(_currentColumn, _currentRow, "left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            Check("right");
            CheckGrid(_currentColumn, _currentRow, "right");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {
            Check("up");
            CheckGrid(_currentColumn, _currentRow, "up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            Check("down");
            CheckGrid(_currentColumn, _currentRow, "down");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _grid[_currentColumn, _currentRow] = 1;
            _gridImage[_currentColumn, _currentRow].enabled = false;
            Check("right");
            CheckGrid(_currentColumn, _currentRow, "right");
        }

        _gridImage[_preColumn, _preRow].color = Color.white;
        _gridImage[_currentColumn, _currentRow].color = Color.red;
        _preRow = _currentRow;
        _preColumn = _currentColumn;
    }

    private void CheckGrid(int column, int row, string arrow)
    {
        if (_grid[column, row] == 0)
        {
            return;
        }
        else
        {
            if (arrow == "left")
            {
                Check("left");
                CheckGrid(_currentColumn, _currentRow, "left");
            }
            else if (arrow == "up")
            {
                Check("up");
                CheckGrid(_currentColumn, _currentRow, "up");
            }
            else if (arrow == "right")
            {
                Check("right");
                CheckGrid(_currentColumn, _currentRow, "right");
            }
            else
            {
                Check("down");
                CheckGrid(_currentColumn, _currentRow, "down");
            }
        }
    }

    private void Check(string arrow)
    {
        if (arrow == "left")
        {
            if (_currentColumn - 1 < 0)
                _currentColumn = _column - 1;
            else
                _currentColumn--;
        }
        else if (arrow == "up")
        {
            if (_currentRow - 1 < 0)
                _currentRow = _row - 1;
            else
                _currentRow--;
        }
        else if (arrow == "right")
        {
            if (_currentColumn + 1 >= _column)
                _currentColumn = 0;
            else
                _currentColumn++;
        }
        else
        {
            if (_currentRow + 1 >= _row)
                _currentRow = 0;
            else
                _currentRow++;
        }
    }
}