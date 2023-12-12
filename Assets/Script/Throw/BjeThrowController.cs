using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BjeThrowController : MonoBehaviour
{
    GameObject _pointer;
    [SerializeField] Material _material;
    [SerializeField] float _distance = 10; //終点の距離
    [SerializeField] float _dropHeight = 5; //開始点より終点の高さをどれだけ下げるか
    [SerializeField] int _positionCount = 10;
    [SerializeField] float _width = 0.1f;
    [SerializeField] GameObject _line;
    LineRenderer _lRender;
    // Start is called before the first frame update
    void Start()
    {
        _pointer = this.gameObject;
        InitLine();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    void InitLine()
    {
        //ラインオブジェクトをこのオブジェクトの子オブジェクトにする
        _line.transform.parent = _pointer.transform;

        _lRender = _line.GetComponent<LineRenderer>();
        _lRender.receiveShadows = false; //ライティングで受ける影の影響をオフにする
        _lRender.shadowCastingMode = ShadowCastingMode.Off; //シャドウをキャストしない
        _lRender.loop = false;
        _lRender.positionCount = _positionCount;
        _lRender.startWidth = _width;
        _lRender.endWidth = _width;
        _lRender.material = _material;
    }
    void DrawLine()
    {
        Vector3 startPos = _pointer.transform.position; //始点
        Vector3 centerPos = _pointer.transform.position + _pointer.transform.forward * _distance / 2; //途中点
        Vector3 endPos = _pointer.transform.position + _pointer.transform.forward * _distance; //終点
        //終点の高さを調整する
        endPos.y = startPos.y - _dropHeight;
    }
}
