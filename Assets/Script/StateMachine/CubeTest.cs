using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    /// <summary>
    /// イベント定義
    /// </summary>
    public enum EventType
    {
        ColorChangeRed,
        ColorChangeBlue,
    }

    private StateMachine<CubeTest> _stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        //ステートマシンの定義
        _stateMachine = new StateMachine<CubeTest>(this);
        _stateMachine.AddTransition<CubeChangeRed, CubeChangeBlue>((int)EventType.ColorChangeBlue);
        _stateMachine.AddTransition<CubeChangeBlue, CubeChangeRed>((int)EventType.ColorChangeRed);
        //ステート開始
        _stateMachine.OnStart<CubeChangeRed>();
    }

    // Update is called once per frame
    void Update()
    {
        //ステート更新
        _stateMachine.OnUpdate();
    }
}
