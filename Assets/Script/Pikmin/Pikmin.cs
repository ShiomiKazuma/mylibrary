using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pikmin : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] bool IsFollow;
    public PikminPlayer _controller;
    public Transform _playerGathPos;
    public Transform _homPos;
    public ObjController _targetObject;
    public bool _gohome;
    public bool IsIdle;

    private void Update()
    {
        if(IsFollow)
        {
            _agent.SetDestination(_playerGathPos.position);
        }

        if(_gohome)
        {
            _agent.SetDestination(_homPos.transform.position);
            if(Vector3.Distance(transform.position, _homPos.transform.position) <= 0.85f)
            {
                IsFollow = false;
                _gohome = false;
                IsIdle = false;
            }
        }

        if(_targetObject != null)
        {
            _agent.SetDestination(_targetObject.transform.position);
            if (Vector3.Distance(transform.position, _targetObject.transform.position) <= 0.75f)
            {
                _targetObject.transform.position = transform.position + Vector3.forward * 0.9f;
                _targetObject.transform.SetParent(transform);
                Destroy(_targetObject.GetComponent<GameObject>());
                _gohome = true;
            }
        }

        if(IsIdle)
        {
            _agent.Stop();
        }
    }
}
