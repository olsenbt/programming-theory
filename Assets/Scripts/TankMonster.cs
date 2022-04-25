using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMonster : Monster
{
    public int health = 3;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // override
    public override void Die() {
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            health = 3; // reset health
        }
    }
}
