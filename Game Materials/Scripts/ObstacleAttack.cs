using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttack
{

    public char[] pointsObstacle;

   // public float waitTime;

    

    public ObstacleAttack(params char[] points )
    {
        //waitTime = time;

        pointsObstacle = new char[points.Length];

        for (int i = 0; i < pointsObstacle.Length; i++)
        {
            
            if(points.Length < i )
            {
                return;
            }

            pointsObstacle[i] = points[i];
            

        }

    }


}
