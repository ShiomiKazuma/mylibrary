using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マス目を管理するクラス
/// </summary>
[System.Serializable]
public class AStarGrid
{
    public enum GroundType
    {
        Wall,
        Road,
        Goal,
    }

    /// <summary>
    /// 座標
    /// </summary>
    [System.Serializable]
    public struct Coord
    {
        public int X;
        public int Y;

        public static Coord Zero = new Coord() { X = 0, Y = 0 };
        public static Coord One = new Coord() { X = 1, Y = 1 };
        public static Coord Left = new Coord() { X = -1, Y = 0 };
        public static Coord Up = new Coord() { X = 0, Y = 1 };
        public static Coord Right = new Coord() { X = 1, Y = 0 };
        public static Coord Down = new Coord() { X = 0, Y = -1 };

        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    /// <summary>
    /// セル
    /// </summary>
    [System.Serializable]
    public class Cell
    {
        public Coord Coord;
        public GroundType GroundType;
    }

    [SerializeField]
    private List<Cell> _cells;
    public List<Cell> Cells { get { return _cells; } }
    [SerializeField]
    private int _columnCount;
    public int ColumnCount { get { return _columnCount; } }
    [SerializeField]
    private int _rowCount;
    public int RowCount { get { return _rowCount; } }
    [SerializeField]
    private Coord _startCellCoord;
    public Cell StartCell { get { return GetCell(_startCellCoord); } set { _startCellCoord = value.Coord; } }
    [SerializeField]
    private Coord _goalCellCoord;
    public Cell GoalCell { get { return GetCell(_goalCellCoord); } set { _goalCellCoord = value.Coord; } }

    public AStarGrid(int columnCount, int rowCount)
    {
        _columnCount = columnCount;
        _rowCount = rowCount;
        _cells = new List<Cell>();
        for(int column = 0; column < columnCount; column++)
        {
            for (int row = 0; row < rowCount; row++)
            {
                var coord = new Coord(column, row);
                _cells.Add(new Cell() { Coord = coord });
            }
        }
    }

    /// <summary>
    /// Coordクラスでのセル取得
    /// </summary>
    public Cell GetCell(Coord coord)
    {
        return GetCell(coord.X, coord.Y);
    }

    /// <summary>
    /// 座標からセルを取得する
    /// </summary>
    public Cell GetCell(int x, int y)
    {
        if(IsValidCoord(x, y))
        {
            var index = x * _rowCount + y;
            return _cells[index];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 座標にマス目があるかを判定する処理
    /// </summary>
    public bool IsValidCoord(int x, int y)
    {
        return x >= 0 && x < _columnCount && y >= 0 && y < _rowCount;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public List<Cell> GetAdjacences(int x, int y)
    {
        var adjacences = new List<Cell>();
        var offsets = new Coord[] { Coord.Left, Coord.Up, Coord.Right, Coord.Down };
        for(int i = 0; i < offsets.Length; i++)
        {
            var cell = GetCell(x + offsets[i].X, y + offsets[i].Y);
            if(cell != null)
            {
                adjacences.Add(cell);
            }
        }
        return adjacences;
    }
}
