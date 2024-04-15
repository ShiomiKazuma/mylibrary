using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Min(0), Tooltip("接地判定用の中心点のオフセット")]
    public float GroundCheckOffsetY = 0.3f;
    [Min(0), Tooltip("接地判定用の中心点のオフセット")]
    public float GroundCheckDistance = 0;
    [Min(0), Tooltip("接地判定の半径")]
    public float GroundCheckRadius = 0.2f;
    [Tooltip("このレイヤーのオブジェクトにレイが当たった時に接地したと判定する")]
    public LayerMask GroundLayers = 0;

    Vector3 _groundNormal;
    RaycastHit _hit;
    
    /// <summary>
    /// 接地判定のメソッド
    /// </summary>
    /// <returns>接地しているかの判定</returns>
    private bool CheckGroundStatus()
    {
        if (Physics.SphereCast(transform.position + Vector3.up * GroundCheckOffsetY, GroundCheckRadius, 
            Vector3.down, out _hit, GroundCheckOffsetY + GroundCheckDistance, 
            GroundLayers, QueryTriggerInteraction.Ignore))
        {
            _groundNormal = _hit.normal;
            return true;
        }
        else
        {
            _groundNormal = Vector3.zero;
            return false;
        }
    }

    [Tooltip("このレイヤーのオブジェクトにレイが当たった時に壁があると判定する")]
    public LayerMask WallLayers = 0;

    private Vector3 _wallNormal;

    private bool CheckWallStatus(float offsetY, float distance, Vector3 direction)
    {
        if (Physics.Raycast(transform.position + Vector3.up * offsetY, direction, out _hit, 
            distance + 1, WallLayers, QueryTriggerInteraction.Ignore))
        {
            _wallNormal = _hit.normal;
            return true;
        }
        else
            return false;
    }
}
