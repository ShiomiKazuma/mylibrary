using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBulletV2 : MonoBehaviour
{
    /// <summary> �e��Prefab </summary>
    [SerializeField, Tooltip("�e��Prefab")] GameObject _bulletPrefab;
    /// <summary> ���ˈʒu�̃I�u�W�F�N�g </summary>
    [SerializeField, Tooltip("���ˈʒu�̃I�u�W�F�N�g")] GameObject _barrelObject;
    /// <summary> �e�𐶐�����ʒu��� </summary>
    Vector3 _instantiatePosition;
    /// <summary> ���ˈʒu�̃I�u�W�F�N�g </summary>
    [SerializeField, Tooltip("�e�����n���鋗�������߂�")]
    float _bulletDistance = 10f;
    /// <summary> ���ˊp�x </summary>
    float _radius;
    /// <summary> �e�̐������W�̓ǂݎ�� </summary>
    public Vector3 InstantiatePosition
    {
        get { return _instantiatePosition; }
    }

    /// <summary> �e�̑��� </summary>
    [SerializeField, Range(1.0f, 20.0f), Tooltip("�e�̎ˏo���鑬�x")]
    float _speed = 1.0f;

    /// <summary> �e�̏����x </summary>
    Vector3 _shootVelocity;
    /// <summary> �e�̏����x�̓ǂݎ�� </summary>
    public Vector3 ShootVelocity
    {
        get { return _shootVelocity; }
    }

    void Update()
    {
        //�e�̏����x���X�V

        float vz = _speed * Mathf.Cos(_radius * Mathf.Deg2Rad);
        float vy = _speed * Mathf.Sin(_radius * Mathf.Deg2Rad);
        _shootVelocity = new Vector3(0, vy, vz);
       // _shootVelocity = _barrelObject.transform.up * _speed;

        //�e�̐������W���X�V
        _instantiatePosition = _barrelObject.transform.position;

        // ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �e�𐶐����Ĕ�΂�
            GameObject obj = Instantiate(_bulletPrefab, _instantiatePosition, Quaternion.identity);
            Rigidbody rid = obj.GetComponent<Rigidbody>();
            rid.AddForce(_shootVelocity * rid.mass, ForceMode.Impulse);

            // 5�b��ɏ�����
            Destroy(obj, 5.0F);
        }
    }

    void CheckMaxRadius()
    {
        float time; // �؋󎞊�
        //�p�x45�x�̎��̔򋗗������߂�
        time = 2 * _speed * Mathf.Sin(45 * Mathf.Deg2Rad) / 9.8f;
        float x = _speed * _speed * Mathf.Sin(45 * 2 * Mathf.Deg2Rad) / 9.8f;
        if(x > _bulletDistance)
        {
            _radius = 45f;
        }
    }
}
