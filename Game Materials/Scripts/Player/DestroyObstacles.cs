using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    [SerializeField]
    private float radiusOfDestroy;

    public void DestroyObstacle()
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
