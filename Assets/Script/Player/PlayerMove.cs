using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3f;
    Rigidbody _rb;
    float _h, _v;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 dir =  new Vector3(_h, 0, _v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        _rb.velocity = dir * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //動く床の子オブジェクトにする
        if(other.tag == "MoveFloor")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //床から離れたら親から離れる
        if(other.tag == "MoveFloor")
        {
            this.gameObject.transform.parent = null;
        }
    }
}
