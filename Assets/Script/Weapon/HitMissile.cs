using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 必ず命中するミサイル
/// </summary>
public class HitMissile : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _position;
    private Transform _target;
    /// <summary>着弾時間 </summary>
    private float _period;

    // Update is called once per frame
    void Update()
    {
        var acceleration = Vector3.zero;

        //ここに与えたい外力(AddForce)を記述する
        var diff = _target.position - _position;
        acceleration += (diff - _velocity * _period) * 2f / (_period * _period);

        _period -= Time.deltaTime;
        if (_period < 0f)
            return;

        _velocity += acceleration * Time.deltaTime;
        _position += _velocity * Time.deltaTime;
        transform.position = _position;
    }
}
