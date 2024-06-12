using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaituOut : MonoBehaviour, IPointerClickHandler
{
    private Image[,] _cells;
    [SerializeField]
    private int _row = 5;
    [SerializeField]
    private int _column = 5;
    private void Start()
    {
        _cells = new Image[_row, _column];
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c] = cell.GetComponent<Image>();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var str = cell.name;
        str.Replace("Cell{", "");
        str.Replace(")", "");
        var ints = Array.ConvertAll(str.Split(","), int.Parse);
        int r = ints[0];
        int c = ints[1];

        //上下左右を変える

        //黒と白の色を反転させる
        //if (image.color == Color.black)
        //    image.color = Color.white;
        //else
        //    image.color = Color.black;
    }
}