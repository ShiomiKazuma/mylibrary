using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaderMap : MonoBehaviour
{
    /// <summary>
    /// 敵のリスト
    /// </summary>
    List<AgentScript> _enemys = new List<AgentScript> ();
    [SerializeField, Tooltip("敵")] Transform _enemy;
    [SerializeField, Tooltip("プレイヤーの位置")] Transform _player;
    [SerializeField, Tooltip("UIの真ん中")] Image _center;
    [SerializeField] Image _target;
    [SerializeField, Tooltip("敵をまとめる親オブジェクト")] GameObject _dest;
    [SerializeField, Tooltip("レーダーの大きさ")] float _raderLength = 30f;
    [SerializeField, Tooltip("半径")] float _radius = 6f;
    Transform[] _points;
    List<RectTransform> _targetsDot = new List<RectTransform> ();
    RectTransform _rectTransform;
    Vector2 _offset;
    // Start is called before the first frame update
    void Start()
    {
        //敵をすべて取得する
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in objects)
        {
            AgentScript agent = obj.GetComponent<AgentScript>();
            agent.RaderMap = this;
            agent.Image = Instantiate(_target, _center.transform.parent).GetComponent<RectTransform>();
            _enemys.Add(obj.GetComponent<AgentScript>());
        }//エネミーを取得する

        //_rectTransform = _target.GetComponent<RectTransform>();
        _offset = _center.GetComponent<RectTransform>().anchoredPosition;
        _points = _dest.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _enemys.Count; i++)
        {
            //Imageがなければ生成
            if (!_enemys[i].Image)
                _enemys[i].Image = Instantiate(_target, _center.transform.parent).GetComponent<RectTransform>();

            Vector3 enemyDir = _enemys[i].transform.position;
            //敵の高さとプレイヤーの高さを合わせる
            enemyDir.y = _player.position.y;
            enemyDir = _enemys[i].transform.position - _player.position;

            enemyDir = Quaternion.Inverse(_player.rotation) * enemyDir; // ベクトルをプレイヤーに合わせて回転
            enemyDir = Vector3.ClampMagnitude(enemyDir, _raderLength); // ベクトルの長さを制限

            //赤点の位置を決める
            _enemys[i].Image.anchoredPosition = new Vector2(enemyDir.x * _radius + _offset.x, enemyDir.z * _radius + _offset.y);
        }
        //Vector3 enemyDir = _enemy.position;
        ////敵の高さとプレイヤーの高さを合わせる
        //enemyDir.y = _player.position.y;
        //enemyDir = _enemy.position - _player.position;

        //enemyDir = Quaternion.Inverse(_player.rotation) * enemyDir;
        //enemyDir = Vector3.ClampMagnitude(enemyDir, _raderLength);

        //_rectTransform.anchoredPosition = new Vector2(enemyDir.x * _radius + _offset.x, enemyDir.z * _radius + _offset.y);
    }
}
