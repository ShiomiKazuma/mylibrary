using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// ステートマシンクラス
/// class定義の基底クラスの切り出し
/// </summary>
public class StateMachine<TOwner>
{
    ///<summary>
    ///ステートを表すクラス 
    ///各ステートクラスはこのクラスを継承する
    ///</summary>
    public abstract class StateBase : MonoBehaviour
    {
        /// <summary>このステートを管理しているステートマシン </summary>
        public StateMachine<TOwner> StateMachine;
        /// <summary>ステートの遷移情報 </summary>
        public readonly Dictionary<int, StateBase> Transitions = new Dictionary<int, StateBase>();
        /// <summary>このステートのオーナー</summary>
        protected TOwner Owner => StateMachine.Owner;

        /// <summary> ステートを開始した時に呼ばれる処理 </summary>
        public virtual void OnStart() { }
        /// <summary> ステートで毎フレーム呼ばれる処理 </summary>
        public virtual void OnUpdate() { }
        /// <summary> ステートを終了した時に呼ばれる </summary>
        public virtual void OnEnd() { }
    }
    ///<summary>このステートマシンのオーナー </summary>
    public TOwner Owner { get; }
    ///<summary>現在のステート </summary>
    private StateBase _currentState;
    ///<summary>全てのステート定義 </summary>
    private readonly LinkedList<StateBase> _states = new LinkedList<StateBase>(); 
    ///<summary>ステートマシンのコンストラクタ </summary>
    ///<param name="owner">ステートマシンを使用するオーナー</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    private T Add<T>() where T : StateBase, new()
    {
        //ステートを追加
        var newState = new T
        {
            StateMachine = this
        };
        _states.AddLast(newState);
        return newState;
    }

    private T GetOrAdd<T>() where T : StateBase, new()
    {
        //追加されていれば返却
        foreach(var state in _states)
        {
            if (state is T result)
                return result;
        }
        //無ければ追加
        return Add<T>();
    }

    /// <summary>
    /// イベントIDに対応した遷移情報を登録
    /// </summary>
    /// <typeparam name="TFrom">遷移元ステート</typeparam>
    /// <typeparam name="TTo">遷移先ステート</typeparam>
    /// <param name="eventId">イベントID</param>
    public void AddTransition<TFrom, TTo>(int eventId) 
        where TFrom: StateBase, new()
        where TTo: StateBase, new()
    {
        //既にイベントIDが登録済みならエラー
        var from = GetOrAdd<TFrom>();
        if(from.Transitions.ContainsKey(eventId))
        {
            Debug.Log("already register eventId : " + eventId);
            return;
        }
        //指定のイベントIDで追加する
        var to = GetOrAdd<TTo>();
        from.Transitions.Add(eventId, to);
    }

    /// <summary>
    /// ステート開始処理
    /// </summary>
    /// <typeparam name="T">開始するステート</typeparam>
    public void OnStart<T>() where T : StateBase, new()
    {
        _currentState = GetOrAdd<T>();
        _currentState.OnStart();
    }

    // <summary>
    /// ステート更新処理
    /// </summary>
    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    /// <summary>
    /// イベント発行
    /// 指定されたIDのステートに切り替える
    /// </summary>
    /// <param name="eventId">イベントID</param>
    public void DispatchEvent(int eventId)
    {
        //イベントIDからステート取得
        if(!_currentState.Transitions.TryGetValue(eventId, out var nextState))
        {
            Debug.Log("Not found eventID : " + eventId);
            return;
        }
        //ステートを切り替える
        _currentState.OnEnd();
        nextState.OnStart();
        _currentState = nextState;
    }
}
