using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTokenController1NavMesh : MonoBehaviour
{
    public Transform _targetTransform; //ターゲットの座標情報
    NavMeshAgent _navMeshAgent;
    Vector3 _destination; //目的地
    [SerializeField] float _distance; //ターゲットとの距離
    //[SerializeField] float _rate; //時間調整用のパラメータ

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_targetTransform == null)
            return;
        else
        {
            SetDestination(_targetTransform.position);
            _navMeshAgent.SetDestination(GetDestination());
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
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
