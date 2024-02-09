using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour
{
    /// <summary>群れのオブジェクトのプレハブ </summary>
    public GameObject FlockPrefab;
    /// <summary>群れのオブジェクトの数 </summary>
    static int _flockNum = 100;
    /// <summary>フィールドの１辺の大きさ </summary>
    public static int FieldSize;
    /// <summary>群れ全体の配列 </summary>
    public static GameObject[] AllFlock = new GameObject[_flockNum];
    /// <summary>群れ全体の配列 </summary>
    
    //初期位置
    private void Start()
    {
        for(int i = 0; i < _flockNum; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-FieldSize, FieldSize), Random.Range(-FieldSize, FieldSize), Random.Range(-FieldSize, FieldSize));
            AllFlock[i] = (GameObject)Instantiate(FlockPrefab, pos, Quaternion.identity);
        }
    }
}
