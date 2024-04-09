using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class SimpleOccluderController : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーが隠れた時の透明度"), Range(0f, 1f)] float _transparency = 0.2f;
    [SerializeField, Tooltip("通常の透明度"), Range(0f, 1f)] float _normal = 1f;

    private void OnTriggerEnter(Collider other)
    {
        Renderer r = other.gameObject.GetComponent<Renderer>();
        
    }

    /// <summary>
    /// オブジェクトのアルファ値を変えるメソッド
    /// </summary>
    /// <param name="renderer">アルファ値を変更するマテリアル</param>
    /// <param name="targetAlpha">変更するアルファ値</param>
    private void ChangeAlpha(Renderer renderer, float targetAlpha)
    {
        if(renderer)
        {
            Material m = renderer.material;
            Color c = m.color;
            c.a = targetAlpha;
            m.color = c;
        }
    }
}
