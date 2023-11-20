using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySightController : MonoBehaviour
{
    [Tooltip("���_�̒��S�ɂȂ�I�u�W�F�N�g")]
    [SerializeField] Transform _lookTarget;
    [Tooltip("����p")]
    [SerializeField, Range(0, 180)] float _sightAngle;
    [Tooltip("���E�̋���")]
    [SerializeField] float _sightDistance;
    [Tooltip("���E�ɓ������Ƃ��ɕ\������ UI")]
    [SerializeField] Text _visibleMessage;
    /// <summary>�����������I�u�W�F�N�g </summary>
    Transform _target;
    /// <summary>�����t���O </summary>
    bool _isVisible = false;

    // Update is called once per frame
    void Update()
    {
        //���݂̔����t���O��IsVisible�̃t���O
        if(_isVisible  ^ IsVisible())
        {
            _isVisible = !_isVisible;
            _visibleMessage.enabled = _isVisible; //���������烁�b�Z�[�W��\������
        }
    }

    bool IsVisible()
    {
        //�����������I�u�W�F�N�g�������ꍇ�̓v���C���[��T���Ċ��蓖�Ă�
        if(!_target)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player)
                _target = player.transform;
        }

        Vector3 look = _lookTarget.position - this.transform.position; //���_�����x�N�g��
        Vector3 target = _target.position - this.transform.position;
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // ����p�i�̔����j�̗]��
        float cosTarget = Vector3.Dot(look, target) / (look.magnitude * target.magnitude);  // �^�[�Q�b�g�ւ̊p�x�̗]��
        return cosTarget > cosHalfSight && target.magnitude < _sightDistance;   // �^�[�Q�b�g�ւ̊p�x�����E�̊p�x��菬�����A�����������E���߂��Ȃ猩���Ă���Ɣ��肵�� true ��Ԃ�
        // �Ȃ��H �� �p�x�� 0 ~ 180�i0 ~ �΁j�̎��A�� > �� �� cos�� < cos�� �����藧����
    }
}
