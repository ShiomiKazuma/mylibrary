using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaderMap : MonoBehaviour
{
    [SerializeField] Transform _enemy;
    [SerializeField, Tooltip("敵の位置")] Transform _player;
    [SerializeField] Image _center;
    [SerializeField] Image _target;
    [SerializeField] float _raderLength = 30f;
    [SerializeField, Tooltip("半径")] float _radius = 6f;
    RectTransform _rectTransform;
    Vector2 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = _target.GetComponent<RectTransform>();
        _offset = _center.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyDir = _enemy.position;
        //敵の高さとプレイヤーの高さを合わせる
        enemyDir.y = _player.position.y;
        enemyDir = _enemy.position - _player.position;

        enemyDir = Quaternion.Inverse(_player.rotation) * enemyDir;
        enemyDir = Vector3.ClampMagnitude(enemyDir, _raderLength);

        _rectTransform.anchoredPosition = new Vector2(enemyDir.x * _radius + _offset.x, enemyDir.z * _radius + _offset.y);
    }
}
