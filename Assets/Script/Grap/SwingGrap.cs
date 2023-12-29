using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingGrap : MonoBehaviour
{
    [Header("インプットキー")] public KeyCode _swingKey = KeyCode.Mouse1;
    public LineRenderer _lr;
    public Transform _gunTip, _cam, _player;
    public LayerMask IsGrappleable;
    float _maxSwingDistance = 25f;
    Vector3 _swingPoint;
    SpringJoint _joint;
    private void Update()
    {
        if (Input.GetKeyDown(_swingKey)) StartSwing();
        if (Input.GetKeyUp(_swingKey)) StopSwing();
    }
    void StartSwing()
    {
        RaycastHit hit;
        if(Physics.Raycast(_cam.position, _cam.forward, out hit, _maxSwingDistance, IsGrappleable))
        {
            _swingPoint = hit.point;
            _joint = _player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _swingPoint;
            float distanceFromPoint = Vector3.Distance(_player.position, _swingPoint);

            _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.minDistance = distanceFromPoint * 0.25f;

            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;
        }
    }

    void StopSwing()
    {
        _lr.positionCount = 0;
        Destroy(_joint);
    }
}
