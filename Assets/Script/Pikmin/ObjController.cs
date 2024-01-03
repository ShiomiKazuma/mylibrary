using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    public bool IsSelected;
    PikminPlayer _pikuminPlayer;
    [SerializeField] int _weight;
    // Start is called before the first frame update
    void Start()
    {
        _pikuminPlayer = GameObject.Find("Player").GetComponent<PikminPlayer>();
    }

    public void Selected()
    {
        if(!IsSelected && _pikuminPlayer._followingPikmins.Count >= 1)
        {
            _pikuminPlayer._followingPikmins[0]._targetObject = this;
            _pikuminPlayer._followingPikmins.RemoveAt(0);
            IsSelected = true;
        }
    }
}
