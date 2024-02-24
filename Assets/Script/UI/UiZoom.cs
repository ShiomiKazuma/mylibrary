using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiZoom : MonoBehaviour
{
    [SerializeField, Tooltip("ズームしたいパネル")] RectTransform _zoomPanel;
    [SerializeField, Tooltip("拡大が完了する時間")] float _zoomTime;
    [SerializeField, Tooltip("拡大する大きさ")] float _scale;
    [SerializeField, Tooltip("部位ごとのRectTransform")] RectTransform[] _rectTransforms;
    [SerializeField, Tooltip("初期状態のパネル")] RectTransform _normalPanelSize;
    /// <summary>
    /// UIをズームするときに呼び出すメソッド
    /// </summary>
    /// <param name="num">部位の添え字</param>
    public void ZoomUi(int num)
    {
        //Sequenceの生成
        var sequence = DOTween.Sequence();
        //RectTransformでパネルを移動させる
        sequence.Append(_zoomPanel.DOAnchorPos(new Vector2(_rectTransforms[num].position.x, _rectTransforms[num].position.y), _zoomTime));
        //ズームさせる
        sequence.Join(_zoomPanel.DOScale(_scale, _scale));
        sequence.Play();
    }

    /// <summary>
    /// 通常のパネルに戻す
    /// </summary>
    public void NormalUi()
    {
        //Sequenceの生成
        var sequence = DOTween.Sequence();
        //RectTransformでパネルを移動させる
        sequence.Append(_zoomPanel.DOAnchorPos(new Vector2(_normalPanelSize.position.x, _normalPanelSize.position.y), _zoomTime));
        //ズームさせる
        sequence.Join(_zoomPanel.DOScale(1f, 1f));
        sequence.Play();
    }
}
