using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNode
{
    protected GameObject _owner;
    public GameObject Owner;
    
    private int _index = -1;
    public int Index;

    protected BaseNode _parentNode;
    public BaseNode ParentNode;
    
    protected string _name;
    public string Name;
    
    // 現在のステータス
    protected BehaviorStatus _status = BehaviorStatus.Inactive;
    public BehaviorStatus Status
    {
        get { return _status; }
    }
}
