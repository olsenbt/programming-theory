using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void Rotate() {
        float degreeStep = 45.0f;
        float step = degreeStep * Time.deltaTime;

        transform.Rotate(Vector3.up, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            Activate();
            Destroy(); // hides powerup
        }
    }

    public virtual void Activate() {
        // activates current ability
    }


    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}

/* Powerups
 * 
 * Freeze: freezes all current enemy players for some duration: chance 10%
 * 
 * Triple shot: all shots are tripled for some duration: chance 10%
 * 
 * Laser: cannonballs switch out for a 
 * 
 * Nuke: destroy all enemies currently on screen. chance: 5%
 * 
 */
