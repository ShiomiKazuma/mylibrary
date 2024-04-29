using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        /// <summary>プレイヤーのRigidbody </summary>
        private Rigidbody _rb;
        /// <summary>プレイヤーの移動方向 </summary>
        private Vector3 _dir;
        /// <summary>プレイヤーの進行方向を保存する </summary>
        private Quaternion _targetRotation;
        [Header("プレイヤーの回転速度")]
        [SerializeField, Tooltip("プレイヤーの回転速度")]
        private float _rotateSpeed = 10f;

        [Header("プレイヤーの移動速度")]
        [SerializeField, Tooltip("プレイヤーの移動速度")]
        private float _moveSpeed = 10f;

        [Header("接地判定")]
        [SerializeField, Tooltip("接地判定Y方向のオフセット"), Range(0, -1)]
        private float groundOffSetY = -0.14f;

        [Header("接地判定の半径")]
        [SerializeField, Tooltip("接地判定の半径"), Range(0, 1)]
        private float groundRadius = 0.5f;

        [Header("重力の大きさ")]
        [SerializeField, Header("重力の大きさ")] private float _gravityvalue = 10f;

        [Header("接地判定が反応するLayer")]
        [SerializeField, Tooltip("接地判定が反応するLayer")]
        LayerMask groundLayers;

        RaycastHit _slopeHit;
        private Vector3 _groundNormalVector;
        
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
            if(_h != 0 || _v != 0)
            {
                //移動処理をする
            }
            else
            {
                _dir = Vector3.zero;
                _targetRotation = this.transform.rotation;
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    //動く床の子オブジェクトにする
        //    if(other.tag == "MoveFloor")
        //    {
        //        this.gameObject.transform.parent = other.gameObject.transform;
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    //床から離れたら親から離れる
        //    if(other.tag == "MoveFloor")
        //    {
        //        this.gameObject.transform.parent = null;
        //    }
        //}
    }

}