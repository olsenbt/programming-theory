using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // pool variables
    public List<GameObject> pooledEnemyOne;
    public List<GameObject> pooledEnemyTwo;
    public List<GameObject> pooledEnemyThree;
    public List<GameObject> pooledCannonball;
    public List<GameObject> pooledPowerupOne;
    public List<GameObject> pooledPowerupTwo;


    // gameobjects
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;
    public GameObject cannonball;

    public GameObject powerupOne;
    public GameObject powerupTwo;


    // pool config
    public int numBalls;
    public int numEnemy;
    public int numPowerups;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledEnemyOne = new List<GameObject>();
        pooledEnemyTwo = new List<GameObject>();
        pooledEnemyThree = new List<GameObject>();

        pooledPowerupOne = new List<GameObject>();
        pooledPowerupTwo = new List<GameObject>();

        SetupPool(pooledEnemyOne, enemyOne, numEnemy);
        SetupPool(pooledEnemyTwo, enemyTwo, numEnemy);
        SetupPool(pooledEnemyThree, enemyThree, numEnemy);

        SetupPool(pooledCannonball, cannonball, numBalls);

        SetupPool(pooledPowerupOne, powerupOne, numPowerups);
        SetupPool(pooledPowerupTwo, powerupTwo, numPowerups);
    }

    private void SetupPool(List<GameObject> pooledObjects, GameObject pooledObject, int amountToPool)
    {
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(pooledObject);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetEnemy(int enemyType) {
        GameObject enemy = null;
        List<GameObject> enemyList = null;

        if (enemyType == 1)
        {
            enemyList = pooledEnemyOne;

        }
        else if (enemyType == 2)
        {
            enemyList = pooledEnemyTwo;
        }
        else if (enemyType == 3)
        {
            enemyList = pooledEnemyThree;
        } else {
            Debug.LogError("ObjectPool.cs: Invalid enemy type");
        }


        // find inactive enemy
        int i = 0;
        while (enemyList[i].activeInHierarchy) {
            i++;
        }

        enemy = enemyList[i];

        // remove from front of list, add to end of list
        enemyList.RemoveAt(0);
        enemyList.Insert(enemyList.Count - 1, enemy);

        return enemy;
    }

    public GameObject GetCannonball() {
        // find inactive cannonball
        int i = 0;
        while (pooledCannonball[i].activeInHierarchy) {
            i++;
        }
        return pooledCannonball[i];
    }

    public GameObject GetPowerup(int type) {
        GameObject powerup = null;
        List<GameObject> powerupList = null;

        if (type == 1)
        {
            powerupList = pooledPowerupOne;

        }
        else if (type == 2)
        {
            powerupList = pooledPowerupTwo;
        }
        else
        {
            Debug.LogError("ObjectPool.cs: Invalid powerup type");
        }


        // find inactive enemy
        int i = 0;
        while (powerupList[i].activeInHierarchy)
        {
            i++;
        }

        powerup = powerupList[i];

        // remove from front of list, add to end of list
        powerupList.RemoveAt(0);
        powerupList.Insert(powerupList.Count - 1, powerup);

        return powerup;
    }
}
