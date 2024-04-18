using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunsinRenderTexter : MonoBehaviour
{
    // 描画したいレンダーテクスチャリスト
    public RenderTexture[] WritetexList;
    // 残像を描画するイメージリスト
    public RawImage[] ImagList;
    int _count = 0;
    Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _count++;
        // レンダーテクスチャリストの内の一枚を最新の描画にセットする
        _camera.targetTexture = WritetexList[_count % WritetexList.Length];
        // 残像を描画するイメージリストを順番に入れ替えていく
        for (int i = 0; i < ImagList.Length; i++)
        {
            int cal = (_count % WritetexList.Length + i) % ImagList.Length;
            ImagList[i].texture = WritetexList[cal];
        }
    }
}
