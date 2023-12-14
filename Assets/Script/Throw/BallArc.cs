using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArc : MonoBehaviour
{
    ///<summary>�������̕`���ON/OFF</summary>
    bool _drawArc = true;
    /// <summary> ���������\����������̐� </summary>
    [SerializeField] int _lineCount = 60;
    /// <summary> �����������b���v�Z���邩 </summary>
    [SerializeField] float _predictionTime = 6.0f;
    /// <summary> �������̃}�e���A�� </summary>
    [SerializeField, Tooltip("�������̃}�e���A��")] Material _arcMaterial;
    /// <summary> �������̕� </summary>
    [SerializeField, Tooltip("�������̕�")] float _arcWidth = 0.02f;
    /// <summary> ��������LineRenderer </summary>
    LineRenderer[] _lineRenderers;
    /// <summary> �e�̏����x�␶�����W�����R���|�[�l���g </summary>
    ShotBullet _shotBullet;
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
        _shotBullet = gameObject.GetComponent<ShotBullet>();
    }

    void Update()
    {
        //�����x���X�V
        _initialVelocity = _shotBullet.ShootVelocity;
        //�������̊J�n���W���X�V
        _arcStartPos = _shotBullet.InstantiatePosition;

        if(_drawArc)
        {
            //�������̕\��
            float timeStep = _predictionTime / _lineCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            //�����̐��������[�v�����A����������X�V����
            for(int i = 0; i < _lineCount; i++)
            {
                //���̍��W���X�V
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                //�Փ˔���
                if(!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // �Փ˂����炻�̐�̕������͕\�����Ȃ�
                    }
                }
            }

            //�}�[�J�[�̕\��
            if(hitTime != float.MaxValue)
            {
                Vector3 hitPosition = GetArcPositionAtTime(hitTime);
                ShowPointer(hitPosition);
            }
        }
        else
        {
            //�������ƃ}�[�J�[���\���ɂ���
            for(int i = 0; i < _lineRenderers.Length; i++)
            {
                _lineRenderers[i].enabled = false;
            }
            _pointerObject.SetActive(false);
        }
    }

    ///<summary> �w�莞�Ԃɑ΂���A�[�`�̕�������̍��W��Ԃ� </summary>
    // <param name="time">�o�ߎ���</param>
    /// <returns>���W</returns>
    Vector3 GetArcPositionAtTime(float time)
    {
        return (_arcStartPos + ((_initialVelocity * time) + (0.5f * time * time) * Physics.gravity));
    }

    /// <summary> LineRenderer�̍��W���X�V</summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        _lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        _lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
        _lineRenderers[index].enabled = draw;
    }

    /// <summary> LineRenderer�I�u�W�F�N�g���쐬</summary>
    void CreateLineRendererObject()
    {
        // �e�I�u�W�F�N�g�����ALineRenderer�����q�I�u�W�F�N�g�����
        GameObject arcObjectsParent = new GameObject("ArcObject");

        _lineRenderers = new LineRenderer[_lineCount];
        for (int i = 0; i < _lineCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + i);
            newObject.transform.SetParent(arcObjectsParent.transform);
            _lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            // �����֘A���g�p���Ȃ�
            _lineRenderers[i].receiveShadows = false;
            _lineRenderers[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            _lineRenderers[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            _lineRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // ���̕��ƃ}�e���A��
            _lineRenderers[i].material = _arcMaterial;
            _lineRenderers[i].startWidth = _arcWidth;
            _lineRenderers[i].endWidth = _arcWidth;
            _lineRenderers[i].numCapVertices = 5; //���̐�[�̊ۂ��𒲐�����
            _lineRenderers[i].enabled = false;
        }
    }

    ///<summary> �w����W�Ƀ}�[�J�[��\�� </summary>
    /// <param name="position"></param>
    void ShowPointer(Vector3 position)
    {
        _pointerObject.transform.position = position;
        _pointerObject.SetActive(true);
    }

    ///<summary> 2�_�Ԃ̐����ŏՓ˔��肵�A�Փ˂��鎞�Ԃ�Ԃ� </summary>
    //<returns>�Փ˂�������(���ĂȂ��ꍇ��float.MaxValue)</returns>
    float GetArcHitTime(float startTime, float endTime)
    {
        //LineCast��������̎n�I�_�̍��W
        Vector3 startPos = GetArcPositionAtTime(startTime);
        Vector3 endPos = GetArcPositionAtTime(endTime);

        //�Փ˔���
        RaycastHit hitInfo;
        if(Physics.Linecast(startPos, endPos, out hitInfo))
        {
            //�Փ˂���Collider�܂ł̋���������ۂ̏Փˎ��Ԃ��Z�o
            float distance = Vector3.Distance(startPos, endPos);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }
}
