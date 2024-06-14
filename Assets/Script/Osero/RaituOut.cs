using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum ColorState
{
    White,
    Black
}
public class RaituOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int _row = 5;
    [SerializeField]
    private int _column = 5;
    private Cell[,] _cells;
    private bool IsClear;

    private void Awake()
    {
        _cells = new Cell[_row, _column];
    }
    private void Start()
    {
        var layout = GetComponent<GridLayoutGroup>();
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = _column;

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                GameObject cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                Debug.Log(cell.gameObject);
                InstatnceCell(r, c, cell);
            }
        }
        
        InitializeCells();
    }

    public void InstatnceCell(int r, int c, GameObject cell)
    {
        _cells[r, c]._gameObject = cell.gameObject;
        _cells[r, c]._x = c;
        _cells[r, c]._y = r;
        _cells[r, c].Color = ColorState.White;
        _cells[r, c]._image = cell.GetComponent<Image>();
        _cells[r, c]._image.color = UnityEngine.Color.black;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsClear)
            return;
        var cell = eventData.pointerCurrentRaycast.gameObject;
        int r = 0;
        int c = 0;
        TryGetCell(cell, ref r, ref c);
        CellCheck(r, c);
        if (CheckClear())
        {
            IsClear = true;
            Debug.Log("クリア");
        }
    }

    private void TryGetCell(GameObject cell, ref int row, ref int column)
    {
        row = 0; column = 0;

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                if (_cells[r, c]._gameObject == cell)
                {
                    row = r;
                    column = c;
                    return;
                }
            }
        }
    }
    private void CellCheck(int r, int c)
    {
        //クリックされたものを判定
        CellChange(r, c);
        //上下左右の色を反転させる
        if (r + 1 < _row)
            CellChange(r + 1, c);
        if(r - 1 >= 0)
            CellChange(r - 1, c);
        if(c + 1 < _column)
            CellChange(r, c + 1);
        if(c - 1 >= 0)
            CellChange(r, c - 1);
    }

    /// <summary>
    /// セルを反転させる
    /// </summary>
    /// <param name="r"></param>
    /// <param name="c"></param>
    private void CellChange(int r, int c)
    {
        var image = _cells[r, c]._image;
        image.color = image.color == UnityEngine.Color.white ? UnityEngine.Color.black : UnityEngine.Color.white;
        _cells[r, c].Color = _cells[r, c].Color == ColorState.White ? ColorState.Black : ColorState.White;
    }

    /// <summary>
    /// クリア判定
    /// </summary>
    public bool CheckClear()
    {
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                if (_cells[r, c].Color != ColorState.Black)
                    return false;
            }
        }

        return true;
    }

    private void InitializeCells()
    {
        int count = 0;
        do
        {
            for (var r = 0; r < _row; r++)
            {
                for (var c = 0; c < _column; c++)
                {
                    if (Random.value > 0.1) { continue; }

                    // クリア済みの状態からボタンを押していく
                    CellCheck(r, c);
                    count++;
                }
            }
        } while (CheckClear() && count >= 2);
    }
    public struct Cell
    {
        public int _x;
        public int _y;
        public ColorState Color;
        public Image _image;
        public GameObject _gameObject;
    }
}

