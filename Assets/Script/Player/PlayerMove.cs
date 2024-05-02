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
        private LayerMask groundLayers;

        [Header("法線を取るRayの長さ")]
        [SerializeField, Tooltip("法線を取るRayの長さ"), Range(0, 1)]
        private float _normalRayLength = 0.3f;

        private RaycastHit _slopeHit;
        private Vector3 _groundNormalVector;
        
        private float _h, _v;
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

        private void FixedUpdate()
        {
            if(_h != 0 || _v != 0)
            {
                //移動処理をする
                Vector2 velocity = new Vector2(_h, _v);
                OnMove(velocity);
            }
            else
            {
                _dir = Vector3.zero;
                _targetRotation = this.transform.rotation;
            }
        }

        /// <summary>
        /// プレイヤーの移動処理のメソッド
        /// </summary>
        /// <param name="vec">移動のインプット処理</param>
        private void OnMove(Vector2 vec)
        {
            //プレイヤーの移動方向を決める
            _dir = new Vector3(vec.x, 0, vec.y);
            //
            _dir = Camera.main.transform.TransformDirection(_dir);
            _dir.y = 0;
            _dir = _dir.normalized;

            //滑らかに進行方向に回転させる
            if(_dir.magnitude > 0)
            {
                _targetRotation = Quaternion.LookRotation(_dir, Vector3.up);
            }

            //回転をさせる
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed);

            _rb.AddForce(GetSlopeMoveDirection(_dir * _moveSpeed,NormalRay()), ForceMode.Force);

            if(GroundCheck())
            {
                Debug.Log("接地");
                
            }
            else
            {
                //地面から浮いた際に重力をかける
                _rb.AddForce(new Vector3(0, -_gravityvalue));
            }
        }

        /// <summary>プレイヤーの接地判定を判定するメソッド </summary>
		/// <returns>接地判定</returns>	
		private bool GroundCheck()
        {
            //オフセットを計算して接地判定の球の位置を設定する
            Vector3 spherePosition =
                new Vector3(transform.position.x, transform.position.y - groundOffSetY, transform.position.z);

            //接地判定を返す
            return Physics.CheckSphere(spherePosition, groundRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }

        /// <summary>
        /// 法線を返すメソッド
        /// </summary>
        /// <returns></returns>
        private Vector3 NormalRay()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down * _normalRayLength);
            if (Physics.Raycast(ray, out hit, _normalRayLength, groundLayers))
            {
                Debug.Log("レイ" + hit.normal);
                Debug.DrawRay(transform.position, Vector3.down * _normalRayLength, Color.cyan);
                return hit.normal;
            }
            return Vector3.zero;
        }

        /// <summary>
        /// プレイヤーの速度制限をするメソッド
        /// </summary>
        private void SpeedControl()
        {
            if(_rb.velocity.magnitude > _moveSpeed)
            {
                Debug.Log("速度制限");
                _rb.velocity = GetSlopeMoveDirection(_dir * _moveSpeed, NormalRay());
            }
        }
        /// <summary>
		/// 傾斜に合わせたベクトルに変えるメソッド
		/// </summary>
		/// <returns>傾斜に合わせたベクトル</returns>
		Vector3 GetSlopeMoveDirection(Vector3 dir, Vector3 normalVector)
        {
            return Vector3.ProjectOnPlane(dir, normalVector);
        }

        private void OnCollisionStay(Collision collision)
        {
            // 衝突した面の、接触した点における法線を取得
            _groundNormalVector = collision.contacts[0].normal;
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