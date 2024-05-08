using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerGuidance : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")] private float _moveSpeed = 10f;
    [SerializeField, Tooltip("目的地")] private GameObject _goal;
    [SerializeField, Tooltip("生成したいパーティクル")] private ParticleSystem _particleSystem;
    private NavMeshAgent _agent;
    private bool _active = false;
    // Start is called before the first frame update
    void Start()
    {
        //agentに目的地、速度を設定
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _goal.transform.position;
        _agent.speed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
