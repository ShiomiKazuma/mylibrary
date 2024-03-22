using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField, Tooltip("ノーツの速度")] float _noteSpeed = 5f;
    private void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * _noteSpeed;
    }
}
