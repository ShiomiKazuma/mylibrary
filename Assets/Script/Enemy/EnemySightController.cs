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
    bool _firstDitectPlayer = false;
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
        _firstDitectPlayer = false;
        //�����������I�u�W�F�N�g�������ꍇ�̓v���C���[��T���Ċ��蓖�Ă�
        if (!_target)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player)
                _target = player.transform;
        }

        Vector3 look = _lookTarget.position - this.transform.position; //���_�����x�N�g��
        Vector3 target = _target.position - this.transform.position;
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // ����p�i�̔����j�̗]��
        float cosTarget = Vector3.Dot(look, target) / (look.magnitude * target.magnitude);  // �^�[�Q�b�g�ւ̊p�x�̗]��
        //ray���΂��Ԃɕǂ����������m�F���鏈��
        RaycastHit hit;
        Vector3 dir = (_target.position - this.transform.position).normalized; //�^�[�Q�b�g�̕������擾
        Ray ray = new Ray(this.transform.position, dir); //ray���΂�
        //�ŏ���ray�����������I�u�W�F�N�g�𒲂ׂ�
        if(Physics.Raycast(ray.origin, ray.direction * _sightDistance, out hit))
        {
            if (hit.collider.tag == "Player") //�ŏ��ɂ��������I�u�W�F�N�g�̃^�O���v���C���[�ł��邩�𔻒肷��
                _firstDitectPlayer = true;
        }
        return cosTarget > cosHalfSight && target.magnitude < _sightDistance && _firstDitectPlayer;   // �^�[�Q�b�g�ւ̊p�x�����E�̊p�x��菬�����A�����������E���߂��Ȃ猩���Ă���Ɣ��肵�� true ��Ԃ�
        // �Ȃ��H �� �p�x�� 0 ~ 180�i0 ~ �΁j�̎��A�� > �� �� cos�� < cos�� �����藧����
    }

    private void OnDrawGizmos()
    {
        //���o�͈͂�`��
        Vector3 lookAtDirection = (_lookTarget.position - this.transform.position).normalized; //����
        Vector3 rightBorder = Quaternion.Euler(0, _sightAngle / 2, 0) * lookAtDirection;    // �E�[
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _sightAngle / 2, 0) * lookAtDirection;    // ���[
        Gizmos.color = Color.green; //���ʂ͗΂ŕ`��
        Gizmos.DrawRay(this.transform.position, lookAtDirection * _sightDistance);
        Gizmos.color = Color.red;�@//���E�̗��[��Ԃŕ`��
        Gizmos.DrawRay(this.transform.position, rightBorder * _sightDistance);
        Gizmos.DrawRay(this.transform.position, leftBorder * _sightDistance);
    }
}
