using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートマシン
/// </summary>
public class StateMachine<TOwner>
{
    ///<summary>ステートを表すクラス </summary>
    public abstract class State
    {
        /// <summary>このステートを管理しているステートマシン </summary>
        protected StateMachine<TOwner> StateMachine => stateMachine;
        internal StateMachine<TOwner> stateMachine;
        /// <summary>遷移の一覧 </summary>
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();
        /// <summary>このステートのオーナー</summary>
        protected TOwner Owner => stateMachine.Owner;

        /// <summary>ステート開始</summary>
        internal void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        /// <summary> ステートを開始した時に呼ばれる </summary>
        protected virtual void OnEnter(State prevState) { }

        /// <summary> ステート更新 </summary>
        internal void Update()
        {
            OnUpdate();
        }
        /// <summary> 毎フレーム呼ばれる</summary>
        protected virtual void OnUpdate() { }

        /// <summary> ステート終了 </summary>
        internal void Exit(State nextState)
        {
            OnExit(nextState);
        }
        /// <summary> ステートを終了した時に呼ばれる</summary>
        protected virtual void OnExit(State nextState) { }
    }


    ///<summary>このステートマシンのオーナー </summary>
    public TOwner Owner { get; }

    ///<summary>ステートマシンのコンストラクタ </summary>
    ///<param name="owner">ステートマシンのオーナー</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

}
