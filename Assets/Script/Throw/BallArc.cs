using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArc : MonoBehaviour
{
    ///<summary>放物線の描画のON/OFF</summary>
    bool _drawArc = true;
    /// <summary> 放物線を構成する線分の数 </summary>
    [SerializeField] int _lineCount = 60;
    /// <summary> 放物線を何秒分計算するか </summary>
    [SerializeField] float _predictionTime = 6.0f;
    /// <summary> 放物線のマテリアル </summary>
    [SerializeField, Tooltip("放物線のマテリアル")] Material _arcMaterial;
    /// <summary> 放物線の幅 </summary>
    [SerializeField, Tooltip("放物線の幅")] float _arcWidth = 0.02f;
    /// <summary> 放物線のLineRenderer </summary>
    LineRenderer[] _lineRenderers;
    /// <summary> 弾の初速度や生成座標を持つコンポーネント </summary>
    ShotBullet _shotBullet;
    /// <summary> 弾の初速度 </summary>
    Vector3 _initialVelocity;
    /// <summary> 放物線の開始位置 </summary>
    Vector3 _arcStartPos;
    /// <summary> 着弾マーカーオブジェクトのPrefab </summary>
    [SerializeField, Tooltip("着弾地点に表示するマーカーのPrefab")]
    GameObject _pointerPrefab;
    /// <summary> 着弾マーカーオブジェクト</summary>
    GameObject _pointerObject;
    void Start()
    {
        //放物線のLineRendererオブジェクトを用意
        CreateLineRendererObject();

        //着弾マーカーのオブジェクトを用意
        _pointerObject = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
        _pointerObject.SetActive(false);

        //弾の初速度や生成座標を持つコンポーネントを代入
        _shotBullet = gameObject.GetComponent<ShotBullet>();
    }

    void Update()
    {
        //初速度を更新
        _initialVelocity = _shotBullet.ShootVelocity;
        //放物線の開始座標を更新
        _arcStartPos = _shotBullet.InstantiatePosition;

        if(_drawArc)
        {
            //放物線の表示
            float timeStep = _predictionTime / _lineCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            //線分の数だけループさせ、線分を一つずつ更新する
            for(int i = 0; i < _lineCount; i++)
            {
                //線の座標を更新
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                //衝突判定
                if(!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // 衝突したらその先の放物線は表示しない
                    }
                }
            }

            //マーカーの表示
            if(hitTime != float.MaxValue)
            {
                Vector3 hitPosition = GetArcPositionAtTime(hitTime);
                ShowPointer(hitPosition);
            }
        }
        else
        {
            //放物線とマーカーを非表示にする
            for(int i = 0; i < _lineRenderers.Length; i++)
            {
                _lineRenderers[i].enabled = false;
            }
            _pointerObject.SetActive(false);
        }
    }

    ///<summary> 指定時間に対するアーチの放物線上の座標を返す </summary>
    // <param name="time">経過時間</param>
    /// <returns>座標</returns>
    Vector3 GetArcPositionAtTime(float time)
    {
        return (_arcStartPos + ((_initialVelocity * time) + (0.5f * time * time) * Physics.gravity));
    }

    /// <summary> LineRendererの座標を更新</summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        _lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        _lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
        _lineRenderers[index].enabled = draw;
    }

    /// <summary> LineRendererオブジェクトを作成</summary>
    void CreateLineRendererObject()
    {
        // 親オブジェクトを作り、LineRendererを持つ子オブジェクトを作る
        GameObject arcObjectsParent = new GameObject("ArcObject");

        _lineRenderers = new LineRenderer[_lineCount];
        for (int i = 0; i < _lineCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + i);
            newObject.transform.SetParent(arcObjectsParent.transform);
            _lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            // 光源関連を使用しない
            _lineRenderers[i].receiveShadows = false;
            _lineRenderers[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            _lineRenderers[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            _lineRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // 線の幅とマテリアル
            _lineRenderers[i].material = _arcMaterial;
            _lineRenderers[i].startWidth = _arcWidth;
            _lineRenderers[i].endWidth = _arcWidth;
            _lineRenderers[i].numCapVertices = 5; //線の先端の丸さを調整する
            _lineRenderers[i].enabled = false;
        }
    }

    ///<summary> 指定座標にマーカーを表示 </summary>
    /// <param name="position"></param>
    void ShowPointer(Vector3 position)
    {
        _pointerObject.transform.position = position;
        _pointerObject.SetActive(true);
    }

    ///<summary> 2点間の線分で衝突判定し、衝突する時間を返す </summary>
    //<returns>衝突した時間(してない場合はfloat.MaxValue)</returns>
    float GetArcHitTime(float startTime, float endTime)
    {
        //LineCastする線分の始終点の座標
        Vector3 startPos = GetArcPositionAtTime(startTime);
        Vector3 endPos = GetArcPositionAtTime(endTime);

        //衝突判定
        RaycastHit hitInfo;
        if(Physics.Linecast(startPos, endPos, out hitInfo))
        {
            //衝突してColliderまでの距離から実際の衝突時間を算出
            float distance = Vector3.Distance(startPos, endPos);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }
}
