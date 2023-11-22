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
    bool _firstDitectPlayer = false;
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
        _firstDitectPlayer = false;
        //発見したいオブジェクトが無い場合はプレイヤーを探して割り当てる
        if (!_target)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player)
                _target = player.transform;
        }

        Vector3 look = _lookTarget.position - this.transform.position; //視点方向ベクトル
        Vector3 target = _target.position - this.transform.position;
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // 視野角（の半分）の余弦
        float cosTarget = Vector3.Dot(look, target) / (look.magnitude * target.magnitude);  // ターゲットへの角度の余弦
        //rayを飛ばし間に壁が無いかを確認する処理
        RaycastHit hit;
        Vector3 dir = (_target.position - this.transform.position).normalized; //ターゲットの方向を取得
        Ray ray = new Ray(this.transform.position, dir); //rayを飛ばす
        //最初にrayがあたったオブジェクトを調べる
        if(Physics.Raycast(ray.origin, ray.direction * _sightDistance, out hit))
        {
            if (hit.collider.tag == "Player") //最初にあたったオブジェクトのタグがプレイヤーであるかを判定する
                _firstDitectPlayer = true;
        }
        return cosTarget > cosHalfSight && target.magnitude < _sightDistance && _firstDitectPlayer;   // ターゲットへの角度が視界の角度より小さく、かつ距離が視界より近いなら見えていると判定して true を返す
        // なぜ？ ⇒ 角度が 0 ~ 180（0 ~ π）の時、Θ > Γ ⇔ cosΘ < cosΓ が成り立つから
    }

    private void OnDrawGizmos()
    {
        //視覚範囲を描く
        Vector3 lookAtDirection = (_lookTarget.position - this.transform.position).normalized; //正面
        Vector3 rightBorder = Quaternion.Euler(0, _sightAngle / 2, 0) * lookAtDirection;    // 右端
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _sightAngle / 2, 0) * lookAtDirection;    // 左端
        Gizmos.color = Color.green; //正面は緑で描く
        Gizmos.DrawRay(this.transform.position, lookAtDirection * _sightDistance);
        Gizmos.color = Color.red;　//視界の両端を赤で描く
        Gizmos.DrawRay(this.transform.position, rightBorder * _sightDistance);
        Gizmos.DrawRay(this.transform.position, leftBorder * _sightDistance);
    }
}
