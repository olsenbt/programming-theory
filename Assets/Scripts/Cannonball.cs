using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float bound = 30.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -bound || transform.position.x > bound)
        {
            Destroy();
        }
        else if(transform.position.z < -bound || transform.position.z > bound) {
            Destroy();
        }
    }

    void Destroy() {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); // slow ball
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Monster")) {
            collision.gameObject.GetComponent<Monster>().Die();
            Destroy();
        } 
        
    }
}
