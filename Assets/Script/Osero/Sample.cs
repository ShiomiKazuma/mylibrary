using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] int _row = 5;
    [SerializeField] int _horizontal = 5;
    private int _currentRow = 0;
    private int _currentColumn = 0;
    private void Start()
    {
        for (var r = 0; r < 5; r++)
        {
            for (var c = 0; c < 5; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            _currentRow = (_currentRow - 1) % _horizontal;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            _currentRow = (_currentRow + 1) % _horizontal;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {
            _currentColumn = (_currentColumn - 1) % _row;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            _currentColumn = (_currentColumn + 1) % _row;
        }
    }
}