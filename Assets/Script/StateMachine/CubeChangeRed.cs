using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static StateMachine<CubeTest>;

public class CubeChangeRed : StateBase
{
    private float _timer;
    private float _count = 2f;
    public override void OnStart()
    {
        _timer = 0;
        Owner.gameObject.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Change Red");
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > _count)
            StateMachine.DispatchEvent((int)CubeTest.EventType.ColorChangeBlue);
    }

    public override void OnEnd()
    {
        Debug.Log("Red Finish");
    }
}