using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererDraw : MonoBehaviour
{
    [SerializeField, Tooltip("使用するLineRenderer")] private LineRenderer _lineRenderer;
    [SerializeField, Tooltip("Rayを飛ばすオブジェクト")] private GameObject _origin;
    [SerializeField, Tooltip("Rayの長さ")] private float _rayDistance = 100f;

    /// <summary>頂点の数 </summary>
    private int _posCount = 0;
    /// <summary>頂点を生成する最低間隔 </summary>
    private float _interval = 0.1f;
    /// <summary>ボタンを押しているかのフラグ </summary>
    private bool IsInput = false;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //ラインレンダラーを初期化
            _lineRenderer.positionCount = 0;

            _posCount = 0;
            IsInput = false;
            Debug.Log("線を消す");
        }

        if (Input.GetMouseButtonDown(0) || IsInput)
        {
            IsInput = true;
            //Rayを飛ばすスタート位置を決める
            var rayStartPosition = _origin.transform.position;
            //マウスでRayを飛ばす方向を決める
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Hitしたオブジェクト格納用
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                SetPosition(hit.point);
            }  
        }
    }

    /// <summary>
    /// 線を伸ばすメソッド
    /// </summary>
    /// <param name="pos">マウスの位置</param>
    private void SetPosition(Vector3 pos)
    {
        if (!PosCheck(pos))
            return;
        Debug.Log("線を書いた");
        _posCount++;
        _lineRenderer.positionCount = _posCount;
        _lineRenderer.SetPosition(_posCount - 1, pos);
            
    }

    /// <summary>
    /// 頂点を増やしてもよいかを判定するメソッド
    /// </summary>
    /// <param name="pos">現在のマウスポジション</param>
    /// <returns>頂点を増やすかの判定</returns>
    private bool PosCheck(Vector3 pos)
    {
        if (_posCount == 0)
            return true;

        float distance = Vector3.Distance(_lineRenderer.GetPosition(_posCount - 1), pos);
        if (distance > _interval)
            return true;
        else
            return false;
    }
}
