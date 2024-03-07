using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompasTest : MonoBehaviour
{
    [SerializeField, Tooltip("コンパスのイメージ")] Image _compasImage;
    [SerializeField, Tooltip("Player")] Transform _player;
    [SerializeField, Tooltip("基準の角度")] float _angleOffset = 0f;
    RectTransform _rt;
    Quaternion _q;
    Quaternion _offset;
    // Start is called before the first frame update
    void Start()
    {
        _rt = _compasImage.GetComponent<RectTransform>();
        //offsetの初期値を上にする
        _offset = Quaternion.AngleAxis(_angleOffset, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        _q = _player.rotation * _offset;
        _q.z = _q.y;
        _q.y = 0f;
        //そのままだと反対に回るので
        _q.z = -_q.z;
        _rt.rotation = _q;
    }
}
