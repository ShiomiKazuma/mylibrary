using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosInput : MonoBehaviour
{
    /// <summary> �}�E�X�̃��[�J�����W </summary>
    Vector3 _localMousePos;
    /// <summary> �}�E�X�̃��[���h���W </summary>
    Vector3 _worldMousePos;
    /// <summary> �}�E�X���W�̃v���p�e�B </summary>
    public Vector3 GetMousePos()
    {
        return _worldMousePos;
    }
    
    // Update is called once per frame
    void Update()
    {
        //�}�E�X�̃��[�J�����W���擾
        _localMousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�
        _worldMousePos = Camera.main.ScreenToWorldPoint(_localMousePos);
    }
}
