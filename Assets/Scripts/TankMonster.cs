using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMonster : Monster // INHERITANCE
{
    public int health = 3;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // override
    public override void Die() { // POLYMORPHISM
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            health = 3; // reset health
        }
    }
}
