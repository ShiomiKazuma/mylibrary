using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingMaterial : MonoBehaviour
{
    Material _material = null;

    [Header("色変更スパン")]
    public float ChangeColorTime = 0.1f;

    [Header("変更の滑らかさ")]
    public float Smooth = 0.01f;

    [Header("色彩")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;

    [Header("彩度")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;

    [Header("明度")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;

    [Header("色彩 MAX")]
    [Range(0, 1)] public float HSV_HueMax = 1.0f;

    [Header("色彩 MIN")]
    [Range(0, 1)] public float HSV_HueMin = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        HSV_Hue = HSV_HueMin;
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_HueMax)
            HSV_Hue = HSV_HueMin;

        _material.color = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);

        yield return new WaitForSeconds(ChangeColorTime);
        StartCoroutine(ChangeColor());
    }
}
