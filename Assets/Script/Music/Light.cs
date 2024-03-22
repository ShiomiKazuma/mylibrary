using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField, Tooltip("スピード")] float _speed = 3f;
    [SerializeField, Tooltip("")] int _num = 0;
    Renderer _rendere;
    float _alfa = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rendere = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(_rendere.material.color.a <= 0))
        {
            _rendere.material.color = new Color(_rendere.material.color.r, _rendere.material.color.g, _rendere.material.color.b, _alfa);
        }

        if(_num == 1)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                colorChange();
            }   
        }

        if (_num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                colorChange();
            }
        }

        if (_num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                colorChange();
            }
        }

        if (_num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                colorChange();
            }
        }

        _alfa -= _speed * Time.deltaTime;
    }

    /// <summary>
    /// ボタンが押された時に色を変える
    /// </summary>
    void colorChange()
    {
        _alfa = 0.3f;
        _rendere.material.color = new Color(_rendere.material.color.r, _rendere.material.color.g, _rendere.material.color.b, _alfa);
    }
}
