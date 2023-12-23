using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosInput : MonoBehaviour
{
    /// <summary> マウスのローカル座標 </summary>
    Vector3 _localMousePos;
    /// <summary> マウスのワールド座標 </summary>
    Vector3 _worldMousePos;
    /// <summary> マウス座標のプロパティ </summary>
    public Vector3 GetMousePos()
    {
        return _worldMousePos;
    }
    
    // Update is called once per frame
    void Update()
    {
        //マウスのローカル座標を取得
        _localMousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        _worldMousePos = Camera.main.ScreenToWorldPoint(_localMousePos);
    }
}
