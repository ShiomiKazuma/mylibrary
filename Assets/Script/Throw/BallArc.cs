using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArc : MonoBehaviour
{
    /// <summary> ���������\����������̐� </summary>
    [SerializeField] int _lineCount = 60;
    /// <summary> �����������b���v�Z���邩 </summary>
    float _predictionTime = 6.0f;
    /// <summary> �������̃}�e���A�� </summary>
    [SerializeField, Tooltip("�������̃}�e���A��")] Material _arcMaterial;
    /// <summary> �������̕� </summary>
    [SerializeField, Tooltip("�������̕�")] float _arcWidth = 0.02f;
    /// <summary> ��������LineRenderer </summary>
    LineRenderer[] _lineRenderers;
    /// <summary> �e�̏����x�␶�����W�����R���|�[�l���g </summary>
    ShotBullet _shootBullet;
    /// <summary> �e�̏����x </summary>
    Vector3 _initialVelocity;
    /// <summary> �������̊J�n�ʒu </summary>
    Vector3 _arcStartPos;
    /// <summary> ���e�}�[�J�[�I�u�W�F�N�g��Prefab </summary>
    [SerializeField, Tooltip("���e�n�_�ɕ\������}�[�J�[��Prefab")]
    GameObject _pointerPrefab;
    /// <summary> ���e�}�[�J�[�I�u�W�F�N�g</summary>
    GameObject _pointerObject;
    void Start()
    {
        //��������LineRenderer�I�u�W�F�N�g��p��
        CreateLineRendererObject();

        //���e�}�[�J�[�̃I�u�W�F�N�g��p��
        _pointerObject = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
        _pointerObject.SetActive(false);

        //�e�̏����x�␶�����W�����R���|�[�l���g����
        _shootBullet = gameObject.GetComponent<ShotBullet>();
    }

    /// <summary> LineRenderer�I�u�W�F�N�g���쐬</summary>
    void CreateLineRendererObject()
    {

    }
}
