using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///　通常カメラとロックオンカメラの切り替えを制御するクラス
/// </summary>
public class PlayerLockonCamera : MonoBehaviour
{
    [SerializeField, Tooltip("メインカメラ")] Camera _mainCamera;
    [SerializeField, Tooltip("フリールックカメラ")] CinemachineFreeLook _freeLookCamera;
    [SerializeField, Tooltip("ロックオンカメラ")] CinemachineVirtualCamera _lockonCamera;
    int _lockonCameraActivePriority = 11;
    int _lockonCameraInactivePriority = 0;

    /// <summary>
    /// ロックオン時のVirtualCamera切り替えのメソッド
    /// </summary>
    /// <param name="target">ロックオンするオブジェクト</param>
    public void ActiveLockonCamera(GameObject target)
    {
        _lockonCamera.Priority = _lockonCameraActivePriority;
        _lockonCamera.LookAt = target.transform;
    }

    /// <summary>
    /// ロックオン解除時のVirtualCamera切り替えのメソッド
    /// </summary>
    public void InactiveLockonCamera()
    {
        _lockonCamera.Priority = _lockonCameraInactivePriority;
        _lockonCamera.LookAt = null;
    }

    /// <summary>
    /// ロックオンカメラのターゲットの座標を返すメソッド
    /// </summary>
    /// <returns>ロックオンカメラのターゲットの座標</returns>
    public Transform GetLookAtTransform()
    {
        return _lockonCamera.LookAt.transform;
    }
}
