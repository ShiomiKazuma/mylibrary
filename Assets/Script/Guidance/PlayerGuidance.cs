using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerGuidance : MonoBehaviour
{
    [SerializeField, Tooltip("�ړ����x")] private float _moveSpeed = 10f;
    [SerializeField, Tooltip("�ړI�n")] private GameObject _goal;
    [SerializeField, Tooltip("�����������p�[�e�B�N��")] private ParticleSystem _particleSystem;
    private NavMeshAgent _agent;
    private bool _active = false;
    // Start is called before the first frame update
    void Start()
    {
        //agent�ɖړI�n�A���x��ݒ�
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _goal.transform.position;
        _agent.speed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
