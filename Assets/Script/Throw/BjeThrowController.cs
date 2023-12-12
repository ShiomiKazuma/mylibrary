using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BjeThrowController : MonoBehaviour
{
    GameObject _pointer;
    [SerializeField] Material _material;
    [SerializeField] float _distance = 10; //�I�_�̋���
    [SerializeField] float _dropHeight = 5; //�J�n�_���I�_�̍������ǂꂾ�������邩
    [SerializeField] int _positionCount = 10;
    [SerializeField] float _width = 0.1f;
    [SerializeField] GameObject _line;
    LineRenderer _lRender;
    // Start is called before the first frame update
    void Start()
    {
        _pointer = this.gameObject;
        InitLine();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    void InitLine()
    {
        //���C���I�u�W�F�N�g�����̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
        _line.transform.parent = _pointer.transform;

        _lRender = _line.GetComponent<LineRenderer>();
        _lRender.receiveShadows = false; //���C�e�B���O�Ŏ󂯂�e�̉e�����I�t�ɂ���
        _lRender.shadowCastingMode = ShadowCastingMode.Off; //�V���h�E���L���X�g���Ȃ�
        _lRender.loop = false;
        _lRender.positionCount = _positionCount;
        _lRender.startWidth = _width;
        _lRender.endWidth = _width;
        _lRender.material = _material;
    }
    void DrawLine()
    {
        Vector3 startPos = _pointer.transform.position; //�n�_
        Vector3 centerPos = _pointer.transform.position + _pointer.transform.forward * _distance / 2; //�r���_
        Vector3 endPos = _pointer.transform.position + _pointer.transform.forward * _distance; //�I�_
        //�I�_�̍����𒲐�����
        endPos.y = startPos.y - _dropHeight;
    }
}
