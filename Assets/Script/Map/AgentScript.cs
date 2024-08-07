﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AgentScript : MonoBehaviour
{
    /// <summary>マップに表示するイメージの位置 </summary>
    [NonSerialized] public RectTransform RectTransform;
    /// <summary>表示するレーダーマップ </summary>
    [NonSerialized] public RaderMap RaderMap;
    /// <summary> マップに表示するイメージ</summary>
    [SerializeField] public Image Image;
    /// <summary>ロックオン状態 </summary>
    public bool IsRockon = false;
    /// <summary>マップに表示するイメージのデフォルトの色 </summary>
    [SerializeField] public Color _defultColor;
    /// <summary>マップに表示するイメージのロックオン時の色 </summary>
    [SerializeField] public Color _rockonColor;

    private void Awake()
    {
        //レーダーテストを検索する
        RaderMap = GameObject.Find("RaderTest").GetComponent<RaderMap>();
    }

    private void Start()
    {
        RaderMap.GenerateEnemy(this.gameObject);
    }

    public void EnemyDestory()
    {
        RaderMap.DestroyEnemy(this.gameObject);
        Destroy(this.gameObject);
    }
}
