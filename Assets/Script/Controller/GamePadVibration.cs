using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadVibration : MonoBehaviour
{
    public void GameVibration(float time)
    {
        StartCoroutine(Vibration(time));
    }

    IEnumerator Vibration(float time)
    {
        Gamepad gamepad = Gamepad.current;
        if(gamepad != null)
        {
            //コントローラーを振動させる
            gamepad.SetMotorSpeeds(1.0f, 1.0f);
            yield return new WaitForSeconds(time);
            //コントローラーの振動を止める
            gamepad.SetMotorSpeeds(0f, 0f);
        }//ゲームパットなら振動させる
    }
}
