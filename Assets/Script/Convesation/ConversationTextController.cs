using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTextController : MonoBehaviour
{
    [Header("シナリオ")]
    public string[] _scenarios;
    [Header("Uiテキストボックス")]
    public Text _uiText; //uiTextへの参照
    [SerializeField, Header("1文字の表示にかかる時間")]
    [Range(0.001f, 0.3f)]
    float _intervalForCharacterDisplay = 0.05f; //1文字の表示にかかる時間
    /// <summary>現在の行番号 </summary>
    int _currentLine = 0;
    /// <summary>現在の文字列 </summary>
    string _currentText = string.Empty;
    /// <summary>表示にかかる時間 </summary>
    float _timeUntiDisplay = 0f;
    /// <summary>文字列の表示を開始した時間 </summary>
    float _timeElapsed = 1f;
    /// <summary>表示中の文字数 </summary>
    int _lastUpdateCharacter = -1;
    /// <summary>最後まで終了したのかのフラグ </summary>
    bool _isFinish = false;
    [SerializeField, Header("すべて表示された後に待機する時間")]
    float _waitTime = 0.5f;
    /// <summary>待機している時間 </summary>
    float _waitInterval;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > _timeElapsed + _timeUntiDisplay; }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetNextLine();
        _waitInterval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFinish)
        {
            //文字の表示が完了しているなら次の行を表示する
            if (IsCompleteDisplayText && _currentLine < _scenarios.Length)
            {
                //現在の行番号がラストまで行ってない状態でクリックすると、テキストを更新する
                //if (_currentLine < _scenarios.Length && Input.GetMouseButtonDown(0))
                //{
                //    SetNextLine();
                //}
                //else
                //{
                //    //完了してないなら文字をすべて
                //    if(Input.GetMouseButtonDown(0))
                //    {
                //        _timeUntiDisplay = 0;
                //    }
                //}

                //自動でテキストを更新する
                if (_waitTime > _waitInterval)
                {
                    _waitInterval += Time.deltaTime;
                }
                else
                {
                    SetNextLine();
                    _waitInterval = 0;
                }

            }
            else if(_currentLine == _scenarios.Length && IsCompleteDisplayText)
            {
                //もし最後まで表示されていたら
                _uiText.text = "終了";
                _isFinish = true;
            }
        }
        

        if(!_isFinish)
        {
            //クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntiDisplay) * _currentText.Length);

            //表示文字数が前回の表示文字数と異なるテキストを更新する
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _uiText.text = _currentText.Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
            }
        }
    }

    //テキストを更新する
    void SetNextLine()
    {
        _currentText = _scenarios[_currentLine];
        _currentLine++;

        //想定表示時間と現在の時刻をキャッシュ
        _timeUntiDisplay = _currentText.Length * _intervalForCharacterDisplay;
        _timeElapsed = Time.time;

        //文字カウントを初期化
        _lastUpdateCharacter = -1;
    }
}
