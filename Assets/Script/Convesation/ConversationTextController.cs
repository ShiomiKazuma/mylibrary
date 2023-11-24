using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTextController : MonoBehaviour
{
    [Header("�V�i���I")]
    public string[] _scenarios;
    [Header("Ui�e�L�X�g�{�b�N�X")]
    public Text _uiText; //uiText�ւ̎Q��
    [SerializeField, Header("1�����̕\���ɂ����鎞��")]
    [Range(0.001f, 0.3f)]
    float _intervalForCharacterDisplay = 0.05f; //1�����̕\���ɂ����鎞��
    /// <summary>���݂̍s�ԍ� </summary>
    int _currentLine = 0;
    /// <summary>���݂̕����� </summary>
    string _currentText = string.Empty;
    /// <summary>�\���ɂ����鎞�� </summary>
    float _timeUntiDisplay = 0f;
    /// <summary>������̕\�����J�n�������� </summary>
    float _timeElapsed = 1f;
    /// <summary>�\�����̕����� </summary>
    int _lastUpdateCharacter = -1;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > _timeElapsed + _timeUntiDisplay; }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        //�����̕\�����������Ă���Ȃ�N���b�N���Ɏ��̍s��\������
        if(IsCompleteDisplayText)
        {
            //���݂̍s�ԍ������X�g�܂ōs���ĂȂ���ԂŃN���b�N����ƁA�e�L�X�g���X�V����
            if (_currentLine < _scenarios.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
            else
            {
                //�������ĂȂ��Ȃ當�������ׂ�
                if(Input.GetMouseButtonDown(0))
                {
                    _timeUntiDisplay = 0;
                }
            }
        }
        //�N���b�N����o�߂������Ԃ��z��\�����Ԃ̉�%���m�F���A�\�����������o��
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntiDisplay) * _currentText.Length);

        //�\�����������O��̕\���������ƈقȂ�e�L�X�g���X�V����
        if(displayCharacterCount != _lastUpdateCharacter)
        {
            _uiText.text = _currentText.Substring(0, displayCharacterCount);
            _lastUpdateCharacter = displayCharacterCount;
        }
    }

    //�e�L�X�g���X�V����
    void SetNextLine()
    {
        _currentText = _scenarios[_currentLine];
        _currentLine++;

        //�z��\�����Ԃƌ��݂̎������L���b�V��
        _timeUntiDisplay = _currentText.Length * _intervalForCharacterDisplay;
        _timeElapsed = Time.time;

        //�����J�E���g��������
        _lastUpdateCharacter = -1;
    }
}