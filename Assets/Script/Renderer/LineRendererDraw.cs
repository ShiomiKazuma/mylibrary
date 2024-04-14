using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererDraw : MonoBehaviour
{
    [SerializeField, Tooltip("使用するLineRenderer")] private LineRenderer _lineRenderer;
    [SerializeField, Tooltip("使用するカメラ")] private Camera _camera;
    /// <summary>頂点の数 </summary>
    private int _posCount = 0;
    /// <summary>頂点を生成する最低間隔 </summary>
    private float _interval = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            SetPosition(mousePos);
        else if (Input.GetMouseButtonUp(0))
            _posCount = 0;
    }

    /// <summary>
    /// 線を伸ばすメソッド
    /// </summary>
    /// <param name="pos">マウスの位置</param>
    private void SetPosition(Vector2 pos)
    {
        if (!PosCheck(pos))
            return;

        _posCount++;
        _lineRenderer.positionCount = _posCount;
        _lineRenderer.SetPosition(_posCount - 1, pos);
            
    }

    /// <summary>
    /// 頂点を増やしてもよいかを判定するメソッド
    /// </summary>
    /// <param name="pos">現在のマウスポジション</param>
    /// <returns>頂点を増やすかの判定</returns>
    private bool PosCheck(Vector2 pos)
    {
        if (_posCount == 0)
            return true;

        float distance = Vector2.Distance(_lineRenderer.GetPosition(_posCount - 1), pos);
        if (distance > _interval)
            return true;
        else
            return false;
    }
}
