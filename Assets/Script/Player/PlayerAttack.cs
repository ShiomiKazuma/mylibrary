using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃範囲のTrigger")]
    Collider _attackRange;
    [SerializeField, Tooltip("敵のHpゲージ")]
    GameObject _enemyGaze;
    /// <summary>敵Hpゲージのスライダー </summary>
    Slider _enemySlider;
    [SerializeField, Tooltip("当たり判定があるタグ")]
    string _enemyTagName;
    [SerializeField, Tooltip("敵に与えるダメージ")]
    float _damage;

    private void Awake()
    {
        //当たり判定を無効にする
        _attackRange.enabled = false;
        //敵のゲージを取得
        _enemySlider = _enemyGaze.GetComponent<Slider>();
    }

    /// <summary>
    /// 攻撃を始めるときに呼び出すメソッド
    /// </summary>
    /// <param name="damage">与えるダメージ</param>
    public void AttackStart(float damage)
    {
        _damage = damage;
        _attackRange.enabled = true;
    }

    /// <summary>
    /// 攻撃が終わった時に呼び出すメソッド
    /// </summary>
    public void AttackEnd()
    {
        _attackRange.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //もし、当たったものが敵だったらattack処理をする
        if(other.gameObject.tag == _enemyTagName)
        {
            Attack(_damage);
        }
    }

    /// <summary>
    /// 攻撃処理のメソッド
    /// </summary>
    /// <param name="damage">ダメージ</param>
    void Attack(float damage)
    {
        _enemySlider.value -= damage;
    }
}
