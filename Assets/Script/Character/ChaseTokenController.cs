using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTokenController : MonoBehaviour
{
    public Transform _targetTransform; //�^�[�Q�b�g�̍��W���
    Vector3 _destination; //�ړI�n
    [SerializeField] float _distance; //�^�[�Q�b�g�Ƃ̋���
    [SerializeField] float _rate; //���Ԓ����p�̃p�����[�^

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

    ///<summary>�ړI�n��ݒ肷�郁�\�b�h</summary>
    public void SetDestination(Vector3 position)
    {
        _destination = position;
    }

    ///<summary>�ړI�n���擾���郁�\�b�h</summary>
    public Vector3 GetDestination()
    {
        return _destination;
    }
}