using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControl : MonoBehaviour
{
    [SerializeField] GameObject _parent; //�܂Ƃߗp�I�u�W�F�N�g
    [SerializeField] GameObject _model; //�ǔ��L����
    GameObject _token; //�����L����
    public List<GameObject> _tokenList; //���X�g

    /// <summary> �ǔ��L�����𐶐����邽�߂̃��\�b�h </summary>
    public void OnGenerate()
    {
        _token = Instantiate(_model, transform.position, transform.rotation);
        _token.SetActive(true);
        _token.transform.SetParent(_parent.transform);
        _tokenList.Add(_token);
        //_token.GetComponent<ChaseTokenController>()._targetTransform = this.gameObject.transform;
        //����ɂ���
        if(_tokenList.IndexOf(_token) == 0)
        {
            _token.GetComponent<ChaseTokenController>()._targetTransform = this.gameObject.transform;
        }
        else
        {
            _token.GetComponent<ChaseTokenController>()._targetTransform = _tokenList[_tokenList.IndexOf(_token) - 1].transform;
        }
    }

    /// <summary> �ǔ��L�������폜���邽�߂̃��\�b�h </summary>
    public void OnRemove()
    {
        _tokenList.Clear();
        for(int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject child = _parent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }
}
