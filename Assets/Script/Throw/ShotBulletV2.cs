using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBulletV2 : MonoBehaviour
{
    /// <summary> 弾のPrefab </summary>
    [SerializeField, Tooltip("弾のPrefab")] GameObject _bulletPrefab;
    /// <summary> 発射位置のオブジェクト </summary>
    [SerializeField, Tooltip("発射位置のオブジェクト")] GameObject _barrelObject;
    /// <summary> 弾を生成する位置情報 </summary>
    Vector3 _instantiatePosition;
    /// <summary> 発射位置のオブジェクト </summary>
    [SerializeField, Tooltip("弾が着地する距離をきめる")]
    float _bulletDistance = 10f;
    /// <summary> 発射角度 </summary>
    float _radius;
    /// <summary> 弾の生成座標の読み取り </summary>
    public Vector3 InstantiatePosition
    {
        get { return _instantiatePosition; }
    }

    /// <summary> 弾の速さ </summary>
    [SerializeField, Range(1.0f, 20.0f), Tooltip("弾の射出する速度")]
    float _speed = 1.0f;

    /// <summary> 弾の初速度 </summary>
    Vector3 _shootVelocity;
    /// <summary> 弾の初速度の読み取り </summary>
    public Vector3 ShootVelocity
    {
        get { return _shootVelocity; }
    }

    void Update()
    {
        //角度を更新
        CheckRadius();
        //弾の初速度を更新
        //Vector3 dir = Vector3.forward;
        float vz = _speed * Mathf.Cos(_radius * Mathf.Deg2Rad);
        float vy = _speed * Mathf.Sin(_radius * Mathf.Deg2Rad);
       
        _shootVelocity = transform.TransformDirection(new Vector3(0, vy, vz));
        //Debug.Log(_shootVelocity);
        //_shootVelocity = new Vector3(0, vy, vz);
        // _shootVelocity = _barrelObject.transform.up * _speed;
        // _shootVelocity = _barrelObject.transform.up * _speed;

        //弾の生成座標を更新
        _instantiatePosition = _barrelObject.transform.position;

        // 発射
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 弾を生成して飛ばす
            GameObject obj = Instantiate(_bulletPrefab, _instantiatePosition, Quaternion.identity);
            Rigidbody rid = obj.GetComponent<Rigidbody>();
            rid.AddForce(_shootVelocity * rid.mass, ForceMode.Impulse);
            
            // 5秒後に消える
            Destroy(obj, 5.0F);
        }
    }

    void CheckRadius()
    {
        float time; // 滞空時間
        //角度45度の時の飛距離を求める
        time = 2 * _speed * Mathf.Sin(45 * Mathf.Deg2Rad) / 9.8f;
        float distance = _speed * _speed * Mathf.Sin(45 * 2 * Mathf.Deg2Rad) / 9.8f;
        if(distance <= _bulletDistance)
        {
            _radius = 45f;
        }
        else
        {
            //着弾時の距離から角度を求める
            //_radius = Mathf.Asin((_bulletDistance * 9.8f) / (_speed * _speed) / 2);
            _radius = 44.9f;
            bool Is = false;
            for (float marge = 0.01f; marge <= 1 && Is == false; marge += 0.01f)
            {
                for (; Is == false && _radius <= 90; _radius += 0.01f)
                {
                    float currentDis = _speed * _speed * Mathf.Sin(_radius * 2 * Mathf.Deg2Rad) / 9.8f;
                    if (Mathf.Abs(_bulletDistance - currentDis) <= marge)
                        Is = true;
                }
            }
            Debug.Log(_radius);
        }
    }
}
