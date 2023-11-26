using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField, Header("始点")] Transform _sitenn;
    [SerializeField, Header("終点")] Transform _shuuten;
    [SerializeField, Header("動くスピード"), Range(0, 1)] float _speed = 0.5f;
    private void FixedUpdate()
    {
        //始点よりも左の場合
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
