using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour
{
    /// <summary>�Q��̃I�u�W�F�N�g�̃v���n�u </summary>
    public GameObject FlockPrefab;
    /// <summary>�Q��̃I�u�W�F�N�g�̐� </summary>
    static int _flockNum = 100;
    /// <summary>�t�B�[���h�̂P�ӂ̑傫�� </summary>
    public static int FieldSize;
    /// <summary>�Q��S�̂̔z�� </summary>
    public static GameObject[] AllFlock = new GameObject[_flockNum];
    /// <summary>�Q��S�̂̔z�� </summary>
    
    //�����ʒu
    private void Start()
    {
        for(int i = 0; i < _flockNum; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-FieldSize, FieldSize), Random.Range(-FieldSize, FieldSize), Random.Range(-FieldSize, FieldSize));
            AllFlock[i] = (GameObject)Instantiate(FlockPrefab, pos, Quaternion.identity);
        }
    }
}
