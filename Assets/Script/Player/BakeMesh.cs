using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMesh : MonoBehaviour
{
    [SerializeField, Tooltip("ベイクする元のオブジェクト")]
    private SkinnedMeshRenderer _baseMeshObj;
    [SerializeField, Tooltip("ベイクしたメッシュを格納するリスト")]
    private GameObject _bakeList;
    /// <summary>
    /// BakeMeshObjをインスタンスした際のSkinnedMeshRendererリスト
    /// </summary>
    private List<SkinnedMeshRenderer> _bakeCloneMeshList;

    [SerializeField, Tooltip("残像数")]
    private readonly int _cloneCount = 4;
    [SerializeField, Tooltip("残像を更新するフレーム")]
    private readonly int _flameCountMax = 4;
    private int _flameCount = 0;

    private void Start()
    {
        _bakeCloneMeshList = new List<SkinnedMeshRenderer>();

        // 残像を複製
        for (int i = 0; i < _cloneCount; i++)
        {
            var obj = Instantiate(_baseMeshObj);
            _bakeCloneMeshList.Add(obj.GetComponent<SkinnedMeshRenderer>());
        }
    }

    private void FixedUpdate()
    {
        //_flameCountMaxフレームに一度更新
        _flameCount++;
        if(_flameCount % _flameCountMax != 0)
            return;

        //BakeしたMeshを１つ前にずらしていく
        for(int i = _bakeCloneMeshList.Count - 1; i >= 1; i--)
        {
            _bakeCloneMeshList[i].sharedMesh = _bakeCloneMeshList[i - 1].sharedMesh;
            //位置と回転をコピー
            _bakeCloneMeshList[i].transform.position = _bakeCloneMeshList[i - 1].transform.position;
            _bakeCloneMeshList[i].transform.rotation = _bakeCloneMeshList[i - 1].transform.rotation;
        }

        //今のスキンメッシュをBakeする
        Mesh mesh = new Mesh();
        _baseMeshObj.BakeMesh(mesh);
        _bakeCloneMeshList[0].sharedMesh = mesh;

        //位置と回転をコピー
        _bakeCloneMeshList[0].transform.position = transform.position;
        _bakeCloneMeshList[0].transform.rotation = transform.rotation;
    }
}
