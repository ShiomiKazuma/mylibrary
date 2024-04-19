using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RockonSensor : MonoBehaviour
{
    [Tooltip("視点の中心になるオブジェクト")]
    [SerializeField] Transform _lookTarget;
    [Tooltip("視野角")]
    [SerializeField, Range(0, 180)] float _sightAngle;
    [Tooltip("視界の距離")]
    [SerializeField] float _sightDistance;
    /// <summary>現在Rockonしているオブジェクト </summary>
    private GameObject _nowTarget;
    /// <summary>感知できる敵のリスト </summary>
    private List<GameObject> _enemyList;

    void Start()
    {
        _nowTarget = null;
        _enemyList = new List<GameObject>();
    }

    //colliderを使う場合の索敵処理

    private void OnTriggerStay(Collider col)
    {
        //敵が索敵範囲内に入ったら処理を行う
        if(col.tag == "Enemy" && !_enemyList.Contains(col.gameObject))
        {
            _enemyList.Add(col.gameObject);
        }

        if(_nowTarget == null)
        {
            _nowTarget = col.gameObject;
        }
    }

    //敵が索敵範囲を出たら行う処理
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Enemy" && _enemyList.Contains(col.gameObject))
        {
            if(col.gameObject == _nowTarget)
            {
                _nowTarget = null;
            }
        }
        _enemyList.Remove(col.gameObject);
    }

    //colliderを使う場合の処理終了

    //colliderを使わない処理を追加

    /// <summary>
    /// 現在のロックオンオブジェクトを返すメソッド
    /// </summary>
    /// <returns>ロックオンされているオブジェクト</returns>
    public GameObject GetNowTarget()
    {
        return _nowTarget;
    }

    /// <summary>
    /// ロックオンオブジェクトがない場合にセットするメソッド
    /// </summary>
    public void SetNowTarget()
    {
        foreach(var enemy in _enemyList)
        {
            if(_nowTarget == null)
            {
                _nowTarget = enemy;
            }
        }
    }

    /// <summary>
    /// ロックオンオブジェクトを切り替える
    /// </summary>
    public void RockonSwitch()
    {
        if(_enemyList.IndexOf(_nowTarget) != _enemyList.Count - 1)
        {
            _nowTarget = _enemyList[_enemyList.IndexOf(_nowTarget) + 1];
        }
        else
        {
            _nowTarget = _enemyList[0];
        }
    }

    /// <summary>
    /// マルチロック時に索敵範囲のエネミーを返す
    /// </summary>
    /// <returns>索敵範囲のエネミー</returns>
    public List<GameObject> GetEnemyList()
    {
        return _enemyList;
    }

    public void DefaultRockon()
    {
        //近い順に敵をソートする
        SortEnemyList();
        for(int i = 0; i < _enemyList.Count; i++)
        {
            if (IsVisible(_enemyList[i]))
            {
                _nowTarget = _enemyList[i];
                break;
            }
        }
    }

    /// <summary>
    /// 自分から近い順にリストをソートする
    /// </summary>
    public  void SortEnemyList()
    {
        List<GameObject> sortedList = _enemyList.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToList();
        _enemyList.Clear();
        foreach(var enemy in sortedList)
        {
            _enemyList.Add(enemy);
        }
    }

    /// <summary>
    /// 視野角内に敵がいるかを判定
    /// </summary>
    /// <returns>視野角内にいるかの判定結果</returns>
    private bool IsVisible(GameObject target)
    {
        Vector3 look = _lookTarget.position - this.transform.position; //視点方向ベクトル
        Vector3 enmey = target.transform.position - this.transform.position;
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // 視野角（の半分）の余弦
        float cosTarget = Vector3.Dot(look, enmey) / (look.magnitude * enmey.magnitude);  // ターゲットへの角度の余弦

        return cosTarget > cosHalfSight && enmey.magnitude < _sightDistance;
    }
}
