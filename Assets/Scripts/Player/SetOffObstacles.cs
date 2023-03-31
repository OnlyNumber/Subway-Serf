using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOffObstacles : MonoBehaviour
{
    [SerializeField]
    private float radiusOfDestroy;

    public void SetOffObstacleAround()
    {
        Collider[] _hits = Physics.OverlapSphere(transform.position, radiusOfDestroy);
        
        foreach(Collider hit in _hits)
        {
            if(hit.gameObject.CompareTag("Obstacle"))
            {
                hit.gameObject.SetActive(false);
            }
        }


    }
}
