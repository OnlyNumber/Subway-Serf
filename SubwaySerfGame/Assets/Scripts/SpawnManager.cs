using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public delegate void OnGameChange();

    public OnGameChange onIsGameStart;
    public OnGameChange onIsGameFinish;

    [SerializeField]
    private int sizeOfObstaclePool;

    [SerializeField]
    private bool IsGame;

    public bool ISGAME
    {
        get
        {
            return IsGame;
        }
        set
        {
            
            IsGame = value;
            if (IsGame == true)
            {
                onIsGameStart?.Invoke();
            }
            else
            {
                onIsGameFinish?.Invoke();
            }
        }
    }

    [SerializeField]
    private float WaitTimeBeforeSpawn;
    
    [SerializeField]
    private float TimeForSpawnObstacle;

    private GameObject[] obstaclesPool;

    [SerializeField]
    private GameObject[] obstaclePrefab;

    [SerializeField]
    private Transform[] spawnPoints;

    public enum ObstacleType
    {
        NoObstacle = 0,
        JumpObstacle = 1,
        SlideObstacle = 2,
        ClosedObstacle = 3
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        onIsGameStart += GameStart;

        onIsGameFinish += StopSpawning;

        obstaclesPool = new GameObject[sizeOfObstaclePool];

        for (int i = 0; i < sizeOfObstaclePool; i++)
        {

            if (i %2 == 0)
            {
                obstaclesPool[i] = Instantiate(obstaclePrefab[0]);
            }
            else
            {
                obstaclesPool[i] = Instantiate(obstaclePrefab[1]);
                
            }

            obstaclesPool[i].SetActive(false);
        }

    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(WaitTimeBeforeSpawn);
        
        StartCoroutine(SpawnObstacles());
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }
    
    IEnumerator SpawnObstacles()
    {
        //int randRange;

        while (IsGame)
        {

            ObstacleWave(RandomObstacleAttackGenerator());

            yield return new WaitForSeconds(TimeForSpawnObstacle);

        }
    }

    private void GameStart()
    {
        StartCoroutine(WaitTime());
    }

    public void SetOffAllObstacles()
    {

        for (int i = 0; i < obstaclesPool.Length; i++)
        {
            obstaclesPool[i].gameObject.SetActive(false);
        }
    }

    private void ObstacleWave(ObstacleAttack attack)
    {
        Debug.Log("Start Wave");

        for (int i = 0; i < attack.pointsObstacle.Length; i++)
        {

            switch(attack.pointsObstacle[i])
            {
                case ObstacleType.ClosedObstacle:
                    {
                        SetObstacle(FindUnActiveObstacle(ObstacleType.SlideObstacle), spawnPoints[i].transform);

                        SetObstacle(FindUnActiveObstacle(ObstacleType.JumpObstacle), spawnPoints[i].transform);

                        break;

                    }

                case ObstacleType.NoObstacle:
                    {
                        break;
                    }


                default:
                    {
                        SetObstacle(FindUnActiveObstacle(attack.pointsObstacle[i]), spawnPoints[i].transform);

                        break;
                    }
            
            }

            
        }

    }

    private void SetObstacle(GameObject obst, Transform point)
    {
        obst.transform.position = point.position;
        obst.transform.rotation = point.rotation;

        obst.SetActive(true);
    }


    private ObstacleAttack RandomObstacleAttackGenerator()
    {
        bool isCompleted = false;

        ObstacleType[] obstaclesType = new ObstacleType[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {

            obstaclesType[i] = (ObstacleType)Random.Range(0, 4);
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(obstaclesType[i] != ObstacleType.ClosedObstacle)
            {
                isCompleted = true;
            }
        }

        if(isCompleted == false)
        {
            obstaclesType[Random.Range(0, obstaclesType.Length)] = ObstacleType.SlideObstacle;
        }

        

        return new ObstacleAttack(obstaclesType);
    }

    private GameObject FindUnActiveObstacle(ObstacleType type)
    {
        while (true)
        {
            foreach (var item in obstaclesPool)
            {
                if (item.activeInHierarchy == false )
                {
                    if(item.name == "JumpObstacle(Clone)" && type == ObstacleType.JumpObstacle ||
                        item.name == "SlideObstacle(Clone)" && type == ObstacleType.SlideObstacle)
                    {
                        return item;
                    }
                    else
                    {
                        Debug.Log("Eterity");
                    }


                }
            }
        }

    }
}
