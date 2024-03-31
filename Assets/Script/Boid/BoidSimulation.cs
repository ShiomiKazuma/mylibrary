using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BoidSimulation : MonoBehaviour
{
    [SerializeField, Tooltip("群衆の総数")] int _boidCount = 100;
    [SerializeField, Tooltip("群衆のPrefab")] GameObject _boidPrefab;
    [SerializeField, Tooltip("群衆のパラメータ")] Boidparam _boidParam;

    List<Boid> _boidList = new List<Boid>();
    public ReadOnlyCollection<Boid> BoidList
    {
        get { return _boidList.AsReadOnly(); }
    }
    
    /// <summary>
    /// 群衆に加える処理
    /// </summary>
    private void AddBoid()
    {
        var go = Instantiate(_boidPrefab, Random.insideUnitSphere, Random.rotation);
        go.transform.SetParent(transform);
        var boid = go.GetComponent<Boid>();
        boid._simulation = this;
        boid.Boidparam = _boidParam;
        _boidList.Add(boid);
    }

    /// <summary>
    /// リストの末尾の群衆オブジェクトを取り除く
    /// </summary>
    private void RemoveBoid()
    {
        if (_boidList.Count == 0)
            return;

        var lastIndex = _boidList.Count - 1;
        var boid = _boidList[lastIndex];
        Destroy(boid.gameObject);
        _boidList.RemoveAt(lastIndex);
    }

    private void Update()
    {
        while (_boidList.Count < _boidCount)
        {
            AddBoid();
        }
        while (_boidList.Count > _boidCount)
        {
            RemoveBoid();
        }
    }

    private void OnDrawGizmos()
    {
        if (!_boidParam)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * _boidParam.WallScale);
    }
}
