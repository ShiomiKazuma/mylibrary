using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    PlayerMovementGrappling _pm;
    [Header("カメラ")]
    public Transform _cam;
    public Transform _gunTip;
    [Header("グラップリングが当たるレイヤー")]
    public LayerMask _grappleable;
    public LineRenderer _lr;

    [Header("グラッブできる最大距離")]
    public float _maxGrappleDistance;
    [Header("フックが伸びているアニメーションの時間")]
    public float _grappleDelayTime;
    [Header("どれだけ最高到達点を高くするか")]
    public float _overshootYAxis;
    Vector3 _grapplePoint;

    [Header("グラップリングのクールタイム")]
    public float _grapplingCT;
    float _grapplingCTTimer;

    [Header("グラップリングするインプットキー")]
    public KeyCode grappleKey = KeyCode.Mouse1;
    bool Isgrappling;
   
    void Start()
    {
        _pm = GetComponent<PlayerMovementGrappling>();
    }

    void Update()
    {
        if(Input.GetKeyDown(grappleKey))
            StartGrapple();
        //クールダウンタイマーを減らす処理
        if(_grapplingCTTimer > 0)
            _grapplingCTTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (Isgrappling)
        {
            Vector3 temp = _cam.position;
            temp.y = _cam.position.y - 2;
            _lr.SetPosition(0, temp);
        }

    }
    void StartGrapple()
    {
        if (_grapplingCTTimer > 0) return;
        //GetComponent<SwingGrap>().StopSwing();
        Isgrappling = true;
        _pm._freeze = true;
        RaycastHit hit;
        if(Physics.Raycast(_cam.position, _cam.forward, out hit, _maxGrappleDistance, _grappleable))
        {
            _grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        else
        {
            _grapplePoint = _cam.position + _cam.forward * _maxGrappleDistance;
            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }
        //フックを出現させる
        _lr.enabled = true;
        _lr.SetPosition(1, _grapplePoint);
    }

    void ExecuteGrapple()
    {
        _pm._freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = _grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + _overshootYAxis;
        if (grapplePointRelativeYPos < 0)
            highestPointOnArc = _overshootYAxis;

        _pm.JumpToPosition(_grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        _pm._freeze = false;
        Isgrappling = false;
        _grapplingCTTimer = _grapplingCT;
        _lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return Isgrappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }
}
