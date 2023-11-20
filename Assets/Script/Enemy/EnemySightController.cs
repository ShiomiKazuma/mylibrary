using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySightController : MonoBehaviour
{
    [Tooltip("視点の中心になるオブジェクト")]
    [SerializeField] Transform _lookTarget;
    [Tooltip("視野角")]
    [SerializeField, Range(0, 180)] float _sightAngle;
    [Tooltip("視界の距離")]
    [SerializeField] float _sightDistance;
    [Tooltip("視界に入ったときに表示する UI")]
    [SerializeField] Text _visibleMessage;
    /// <summary>発見したいオブジェクト </summary>
    Transform _target;
    /// <summary>発見フラグ </summary>
    bool _isVisible = false;

    // Update is called once per frame
    void Update()
    {
        //現在の発見フラグとIsVisibleのフラグ
        if(_isVisible  ^ IsVisible())
        {
            _isVisible = !_isVisible;
            _visibleMessage.enabled = _isVisible; //発見したらメッセージを表示する
        }
    }

    bool IsVisible()
    {
        //発見したいオブジェクトが無い場合はプレイヤーを探して割り当てる
        if(!_target)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player)
                _target = player.transform;
        }

        Vector3 look = _lookTarget.position - this.transform.position; //視点方向ベクトル
        Vector3 target = _target.position - this.transform.position;
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // 視野角（の半分）の余弦
        float cosTarget = Vector3.Dot(look, target) / (look.magnitude * target.magnitude);  // ターゲットへの角度の余弦
        return cosTarget > cosHalfSight && target.magnitude < _sightDistance;   // ターゲットへの角度が視界の角度より小さく、かつ距離が視界より近いなら見えていると判定して true を返す
        // なぜ？ ⇒ 角度が 0 ~ 180（0 ~ π）の時、Θ > Γ ⇔ cosΘ < cosΓ が成り立つから
    }
}
