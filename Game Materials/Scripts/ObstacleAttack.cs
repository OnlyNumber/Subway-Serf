using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttack
{

    public SpawnManager.ObstacleType[] pointsObstacle;

   // public float waitTime;

    

    public ObstacleAttack(params SpawnManager.ObstacleType[] points )
    {
        //waitTime = time;

        pointsObstacle = new SpawnManager.ObstacleType[points.Length];

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
