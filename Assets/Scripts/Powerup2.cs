using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup2 : Powerup
{
    public MonsterSpawner spawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public override void Activate()
    {
        Debug.Log("activating powerup2");
        spawnerScript.Freeze();
    }
}
