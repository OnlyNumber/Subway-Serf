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

    //[SerializeField]
    //private List<ObstacleAttack> []obstacleAttackList = new List<ObstacleAttack>[3];
    
    

    [SerializeField]
    private Transform[] spawnPoints;


    private enum ObstacleType
    {
        n = 0,
        o = 1,
        O = 2,
        A = 3
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        onIsGameStart += GameStart;

        onIsGameFinish += StopSpawning;

        

        //StartCoroutine(SpawnObstacles());

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

        /*foreach (var item in obstaclesPool)
        {
            Debug.Log(item.name);
        }*/

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

            /*while(true)
            {
                randRange = Random.Range(0, obstaclesPool.Length);

                if(obstaclesPool[randRange].activeInHierarchy == false)
                {


                    break;
                }


            }
            obstaclesPool[randRange].SetActive(true);

            obstaclesPool[randRange].transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            obstaclesPool[randRange].transform.rotation = spawnPoints[0].rotation;
            */



            /*foreach (var item in RandomObstacleAttackGenerator().pointsObstacle)
            {
                Debug.Log(item);
            }*/



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
        //GameObject obst;

        Debug.Log("Start Wave");

        for (int i = 0; i < attack.pointsObstacle.Length; i++)
        {

            switch(attack.pointsObstacle[i])
            {
                case 'A':
                    {
                        SetObstacle(FindUnActiveObstacle('o'), spawnPoints[i].transform);

                        /*obst = FindUnActiveObstacle('o');
                        obst.transform.position = spawnPoints[i].transform.position;
                        obst.transform.rotation = spawnPoints[i].transform.rotation;

                        obst.SetActive(true);*/

                        SetObstacle(FindUnActiveObstacle('O'), spawnPoints[i].transform);

                        /*obst = FindUnActiveObstacle('O');
                        obst.transform.position = spawnPoints[i].transform.position;
                        obst.transform.rotation = spawnPoints[i].transform.rotation;

                        obst.SetActive(true);*/


                        break;

                    }

                case 'n':
                    {
                        break;
                    }


                default:
                    {
                        SetObstacle(FindUnActiveObstacle(attack.pointsObstacle[i]), spawnPoints[i].transform);


                        /*obst = FindUnActiveObstacle(attack.pointsObstacle[i]);
                        obst.transform.position = spawnPoints[i].transform.position;
                        obst.transform.rotation = spawnPoints[i].transform.rotation;

                        obst.SetActive(true);*/
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
        //Debug.Log("Start random");

        bool isCompleted = false;

        char[] obstaclesType = new char[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {

            switch (Random.Range(0, 4))
            {
                case 0:
                    {
                        obstaclesType[i] = 'n';
                            break;
                    }
                case 1:
                    {
                        obstaclesType[i] = 'A';
                        break;
                    }
                case 2:
                    {
                        obstaclesType[i] = 'o';
                        break;
                    }
                case 3:
                    {
                        obstaclesType[i] = 'O';
                        break;
                    }


            }
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(obstaclesType[i] != 'A')
            {
                isCompleted = true;
            }
        }

        if(isCompleted == false)
        {
            obstaclesType[Random.Range(0, obstaclesType.Length)] = 'O';
        }

        

        return new ObstacleAttack(obstaclesType);
    }


    /*private IEnumerator ObstacleAttack(List<ObstacleAttack> obstacleAttackList)
    {
        foreach (ObstacleAttack item in obstacleAttackList)
        {
            
            yield return new WaitForSeconds(item.waitTime);

            //ObstacleWave();


        }

    }*/

    private GameObject FindUnActiveObstacle(char type)
    {
        //Debug.Log("Start find");

        while (true)
        {
            //randRange = Random.Range(0, obstaclesPool.Length);

            foreach (var item in obstaclesPool)
            {
                if (item.activeInHierarchy == false )
                {
                    if(item.name == "JumpObstacle(Clone)" && type == 'o' ||
                        item.name == "SlideObstacle(Clone)" && type == 'O')
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

        //Debug.Log("End find");


    }






}
