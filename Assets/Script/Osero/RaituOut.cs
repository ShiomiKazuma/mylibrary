using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Color
{
	White,
    Black
}

public class Cell
{
    public int _x;
    public int _y;
    public Color _color;
    public Image _imagge;
}
public class RaituOut : MonoBehaviour, IPointerClickHandler
{
    private Cell[,] _cells;
    [SerializeField]
    private int _row = 5;
    [SerializeField]
    private int _column = 5;

    private bool IsClear;
    private void Start()
    {
        _cells = new Cell[_row, _column];
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c]._x = c;
                _cells[r, c]._y = r;
                _cells[r, c]._color = Color.White;
                _cells[r, c]._imagge = cell.GetComponent<Image>();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsClear)
            return;
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var str = cell.name;
        str.Replace("Cell{", "");
        str.Replace(")", "");
        var ints = Array.ConvertAll(str.Split(","), int.Parse);
       
        if (CheckClear())
        {
            IsClear = true;
            Debug.Log("クリア");
        }
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
                if (_cells[r, c]._color != Color.Black)
                    return false;
            }
        }

        return true;
    }
}