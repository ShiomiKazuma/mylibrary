using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ロックオン機能を制御するクラス
/// </summary>
public class PlayerLockon : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーカメラ")] PlayerLockonCamera _playerCamera;
    [SerializeField, Tooltip("基準")] Transform _originTransform;
    [SerializeField, Tooltip("ロックオン可能範囲")] float _lockonRange = 20f;
    [SerializeField, Tooltip("ロックオンするレイヤー")] LayerMask _lockonLayer = 0;
    [SerializeField, Tooltip("障害物のレイヤー")] LayerMask _lockonObstacleLayer = 0;
    [SerializeField, Tooltip("ロックオンカーソル")] GameObject _lockonCursor;

    float _lockonFactor = 0.3f;
    float _lockonThreshold = 0.5f;
    bool IslockonInput = false;
    public bool IsLockon = false;


    Camera _mainCamera;
    Transform _cameraTrn;
    GameObject _targetObj;

    // Start is called before the first frame update
    void Start()
    {
        //変数の初期化（メインカメラの値を変数に代入）
        _mainCamera = Camera.main;
        _cameraTrn = _mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //ロックオンボタンを押したのかを判定
        if(IslockonInput)
        {
            //すでにロックオン済みなら解除する
            if(IsLockon)
            {
                IsLockon = false;
                _lockonCursor.SetActive(false);
                IslockonInput = false;
                _playerCamera.InactiveLockonCamera();
                _targetObj = null;
                return;
            }
        }

        // ロックオン対象の検索、いるならロックオン、いないならカメラ角度をリセット
    }

    /// <summary>
    /// ロックオンボタン入力時の処理(InputSystem)
    /// </summary>
    /// <param name="context">入力状態(InputSystem)</param>
    public void OnLockon(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            //ボタンが押されたときの処理
            case InputActionPhase.Performed:
                IslockonInput = true;
                break;

            //ボタンが離された時の処理
            case InputActionPhase.Canceled:
                break;
        }
    }

    /// <summary>
    /// ロックオン対象の計算処理を行い取得する
    /// </summary>
    /// <returns>ロックオンしたオブジェクト</returns>
    GameObject GetLockontarget()
    {
        //SpereCastAllを使ってPlayer周辺のEnemyを取得しListに格納
        RaycastHit[] hits = Physics.SphereCastAll(_originTransform.position, _lockonRange, Vector3.up, 0, _lockonLayer);

        if(hits?.Length == 0)
        {
            //範囲内にターゲットなし
            return null;
        }

        //hitsのリスト全てにrayを飛ばし、射線が通るものだけをList化
        List<GameObject> hitObjects = MakeListRaycastHit(hits);
        if(hitObjects?.Count == 0)
        {
            //射線が通ったターゲットなし
            return null;
        }

        //hitObjectsのリスト全てのベクトルとカメラのベクトルを比較し、画面中央に一番近いものを探す
        var tumpledData = 

    }

    /// <summary>
    /// リスト全てにrayを飛ばし射線が通るものだけをList化
    /// </summary>
    /// <param name="hits">範囲内のList</param>
    /// <returns></returns>
    List<GameObject> MakeListRaycastHit(RaycastHit[] hits)
    {
        List<GameObject> hitObjects = new List<GameObject>();
        RaycastHit hit;
        for(var i = 0; i < hits.Length; i++)
        {
            var direction = hits[i].collider.gameObject.transform.position - (_originTransform.position);
            if(Physics.Raycast(_originTransform.position, direction, out hit, _lockonRange, _lockonObstacleLayer))
            {
                if(hit.collider.gameObject == hits[i].collider.gameObject)
                {
                    hitObjects.Add(hit.collider.gameObject);
                }
            }
        }
        return hitObjects;
    }

    /// <summary>
    /// リスト全てのベクトルとカメラのベクトルを比較して、画面中央に一番近いものを探す
    /// </summary>
    /// <param name="hitObjects"></param>
    /// <returns></returns>
    (float, GameObject) GetOptimalEnemy(List<GameObject> hitObjects)
    {
        float degreep = Mathf.Atan2(_cameraTrn.forward.x, _cameraTrn.forward.z);
        float degreemum = Mathf.PI * 2;
        GameObject target = null;

        foreach(var enemy in hitObjects)
        {
            // pos: 敵からカメラへ向けたベクトル
            // pos2: カメラから敵に向けたベクトル(水平方向に制限して正規化)
            Vector3 pos = _cameraTrn.position - enemy.transform.position;
            Vector3 pos2 = enemy.transform.position - _cameraTrn.position;
            pos2.y = 0f;
            pos2.Normalize();

            // degree: pos2のX,Z成分からなる角度. カメラの前方からどれだけ回転しているか
            float degree = Mathf.Atan2(pos2.x, pos2.z);
            // degreeを-180°～180°に正規化
            degree = degreeNormalize(degree, degreep);

            // pos.magnitude: 敵とカメラの距離
            // pos.magnitudeに応じて角度に重みをかけ、距離が近いほど角度の重みが大きく選好される
            degree = degree + degree * (pos.magnitude / 500) * _lockonFactor;
            // Mathf.Abs(degreemum): 以前に記録された最小角度差の絶対値
            // Mathf.Abs(degree): 現在の角度差の絶対値
            if (Mathf.Abs(degreemum) >= Mathf.Abs(degree))
            {
                degreemum = degree;
                target = enemy;
            }
        }
        return (degreemum, target);
    }

    float degreeNormalize(float degree, float degreep)
    {

    }
}
