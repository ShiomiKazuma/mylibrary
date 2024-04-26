using UnityEngine;
using UnityEngine.UIElements;

public class GridGenerator : MonoBehaviour
{
    [Header("縦の大きさ")]
    [SerializeField] int _vertical = 8;
    [Header("横の大きさ")]
    [SerializeField] int _horizontal = 8;
    [Header("マス目のプレハブ")]
    [SerializeField] GameObject _massPrefab;
    [Header("マス目のサイズ")]
    [SerializeField] float _massSize;
    [Header("マスの左下の座標")]
    [SerializeField] Transform _underLeft;
    [Header("マス目の間隔")]
    [SerializeField] float _massOffset = 0.2f;
    [Header("グリッドの親オブジェクト")]
    [SerializeField] GameObject _gridParent;

    private void Start()
    {
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        //マス目を配置する
        for(int i = 0; i < _horizontal; i++)
        {
            for(int j = 0; j < _vertical; j++)
            {
                //マス目の間隔を調整する
                float massOffsetX = _massOffset * i, massOffsetZ = _massOffset * j;
                Vector3 pos = new Vector3(_underLeft.position.x + _massSize * i + massOffsetX, _underLeft.position.y, _underLeft.position.z + _massSize * j + massOffsetZ);
                var grid = Instantiate(_massPrefab, pos, Quaternion.identity);
                grid.transform.SetParent(_gridParent.transform);
            }
        }
    }
}
