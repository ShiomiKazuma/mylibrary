using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        //向きをカメラの正面に合わせる
        transform.rotation = Camera.main.transform.rotation;
    }
}
