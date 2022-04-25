using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2.0f;
    public MonsterSpawner spawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnerScript.isFrozen)
        {
            Move();
        }
        else {
            Freeze();
        }
    }

    public void Move()
    {
        gameObject.transform.position += transform.forward * speed;
        //sgameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    public void Freeze() {
        //gameObject.transform.position += transform.forward * speed * 0;
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 0;
    }


    public virtual void Die() {
        spawnerScript.activeMonsters.Remove(gameObject);
        gameObject.SetActive(false);
        SpawnPowerup(Drops()); // spawns a powerup based on drop results
        spawnerScript.TestEndWave();
    }


    // determines whether or not monster drops any powerups
    public int Drops()
    {
        float random = Random.Range(0, 1.0f); // from 0 - 0.99-
        Debug.Log("Drop: " + random);
        if (random <= 0.15) // 15% chance
        {
            return 1; // drop type 1
        }
        else if (random <= 0.30)
        {
            return 2; // drop type 2
        }
        return 0; // 85% chance of dropping nothing
    }

    public void SpawnPowerup(int type)
    {
        if (type == 0) // no powerup
        {
            return;
        }
        // is valid powerup, spawn it
        spawnerScript.SpawnPowerup(type, gameObject.transform.position);
    }
}
