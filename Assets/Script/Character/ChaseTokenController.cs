using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTokenController : MonoBehaviour
{
    public Transform _targetTransform; //ターゲットの座標情報
    Vector3 _destination; //目的地
    [SerializeField] float _distance; //ターゲットとの距離
    [SerializeField] float _rate; //時間調整用のパラメータ

    // Update is called once per frame
    void Update()
    {
        if (_targetTransform == null)
            return;
        else
        {
            SetDestination(_targetTransform.position);
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            transform.position = Vector3.Lerp(this.gameObject.transform.position, _targetTransform.position - dir * _distance, Time.deltaTime * _rate);
            Quaternion setRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, 120.0f * 0.1f * Time.deltaTime);
        }
    }

    ///<summary>目的地を設定するメソッド</summary>
    public void SetDestination(Vector3 position)
    {
        _destination = position;
    }

    ///<summary>目的地を取得するメソッド</summary>
    public Vector3 GetDestination()
    {
        return _destination;
    }
}
