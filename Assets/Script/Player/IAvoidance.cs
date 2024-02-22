using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAvoidance
{
    /// <summary>
    /// 回避成功処理をする
    /// </summary>
    void Avoidance();

    /// <summary>
    /// ジャスト回避成功処理をする
    /// </summary>
    void JustAvoidance();

    /// <summary>
    /// 回避始めの処理をする
    /// </summary>
    void StartAvoidance();

    /// <summary>
    /// 回避終わりの処理をする
    /// </summary>
    void EndAvoidance();
}
