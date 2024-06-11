using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaituOut : MonoBehaviour, IPointerClickHandler
{
    private Image[,] _cells;
    private void Start()
    {
        for (var r = 0; r < 5; r++)
        {
            for (var c = 0; c < 5; c++)
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
        var image = cell.GetComponent<Image>();
        //黒と白の色を反転させる
        if (image.color == Color.black)
            image.color = Color.white;
        else
            image.color = Color.black;
    }
}