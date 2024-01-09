using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikminCall : MonoBehaviour
{
    [SerializeField] GameObject _center;
    [SerializeField] float _radius;

    public void Call()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_center.transform.position, _radius);
        foreach(Collider c in hitColliders)
        {
            if(c.TryGetComponent<Pikmin>(out var pikmin))
            {
                //ÉvÉåÉCÉÑÅ[ÇÃóÒÇ…í«â¡Ç≥ÇÍÇÈ

            }
        }
    }
}
