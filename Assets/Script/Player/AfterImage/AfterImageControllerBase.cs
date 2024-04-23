using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 残像の操作用コンポーネント
/// </summary>
[RequireComponent(typeof(ObservableUpdateTrigger), typeof(ObservableDestroyTrigger))]
public class AfterImageControllerBase : MonoBehaviour
{
    [SerializeField, Header("発生させる残像オブジェクト")] protected AfterImageBase _afterImage = null;
    [SerializeField, Header("残像の親オブジェクト")] protected Transform _afterImageParent = null;
    [SerializeField, Header("事前生成数")] protected int _preLoadCount = 5;
    [SerializeField, Header("残像の生成間隔")] protected float _createIntervalTime = 0.1f;
    [SerializeField, Header("残像の生存時間")] protected float _afterImageLifeTime = 0.2f;
    [SerializeField, Header("残像を生成するか")] protected BoolReactiveProperty _isCreate = new BoolReactiveProperty(false);
    [SerializeReference, Header("発生させる残像オブジェクト"), ReadOnly] protected IAfterImageSetupParam _param = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
