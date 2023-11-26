using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField, Header("�n�_")] Transform _sitenn;
    [SerializeField, Header("�I�_")] Transform _shuuten;
    [SerializeField, Header("�����X�s�[�h"), Range(0, 1)] float _speed = 0.5f;
    private void FixedUpdate()
    {
        //�n�_�������̏ꍇ
        if(this.transform.position.x <= _sitenn.position.x)
        {
            _speed = -_speed;
        }
        else if(this.transform.position.x >= _shuuten.position.x)
        {
            _speed = -_speed;
        }
        transform.Translate(new Vector3(_speed, 0, 0));
    }
}
