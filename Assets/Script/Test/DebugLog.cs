using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public int PushedCount = 0;
    /// <summary>
    /// エディタ拡張でのボタンテスト用
    /// </summary>
   public void DebugAction()
    {
        PushedCount++;
        Debug.Log("DebugActionが呼ばれました" + PushedCount);
    }
}
