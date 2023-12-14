using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArc : MonoBehaviour
{
    /// <summary> 放物線を構成する線分の数 </summary>
    [SerializeField] int _lineCount = 60;
    /// <summary> 放物線を何秒分計算するか </summary>
    float _predictionTime = 6.0f;
    /// <summary> 放物線のマテリアル </summary>
    [SerializeField, Tooltip("放物線のマテリアル")] Material _arcMaterial;
    /// <summary> 放物線の幅 </summary>
    [SerializeField, Tooltip("放物線の幅")] float _arcWidth = 0.02f;
    /// <summary> 放物線のLineRenderer </summary>
    LineRenderer[] _lineRenderers;
    /// <summary> 弾の初速度や生成座標を持つコンポーネント </summary>
    ShotBullet _shootBullet;
    /// <summary> 弾の初速度 </summary>
    Vector3 _initialVelocity;
    /// <summary> 放物線の開始位置 </summary>
    Vector3 _arcStartPos;
    /// <summary> 着弾マーカーオブジェクトのPrefab </summary>
    [SerializeField, Tooltip("着弾地点に表示するマーカーのPrefab")]
    GameObject _pointerPrefab;
    /// <summary> 着弾マーカーオブジェクト</summary>
    GameObject _pointerObject;
    void Start()
    {
        //放物線のLineRendererオブジェクトを用意
        CreateLineRendererObject();

        //着弾マーカーのオブジェクトを用意
        _pointerObject = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
        _pointerObject.SetActive(false);

        //弾の初速度や生成座標を持つコンポーネントを代入
        _shootBullet = gameObject.GetComponent<ShotBullet>();
    }

    /// <summary> LineRendererオブジェクトを作成</summary>
    void CreateLineRendererObject()
    {

    }
}
