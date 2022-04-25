using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public float rotateSpeed = 3.0f;
    public GameObject cannonball;
    public GameObject barrelEnd;
    public float cannonballSpeed = 5.0f;
    public bool canShoot = true;
    public float cooldownLength = 0.05f;
    public float powerup1Duration = 5.0f;

    //powerup
    public bool tripleShot = false;


    // object pooling
    public GameObject[] cannonballs;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // controlls
        if (Input.GetKeyDown(KeyCode.Space) && canShoot) {
            Shoot();
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, -rotateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(0, rotateSpeed, 0);
        }

        /*float input = Input.GetAxis("Horizontal");
        if (input > .5 || input < -.5) {
            transform.Rotate(0, input * rotateSpeed, 0);
        }*/
        
    }

    void Shoot() {
        GameObject ball = ObjectPool.Instance.GetCannonball();
        ball.transform.position = barrelEnd.transform.position;
        ball.transform.rotation = barrelEnd.transform.rotation;
        ball.gameObject.GetComponent<Renderer>().material.color = Color.white;
        ball.SetActive(true);

        if (tripleShot) {
            TripleShot();
        }

        ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * cannonballSpeed);

        canShoot = false;
        StartCoroutine(ShotCooldown());
    }

    void TripleShot() {
        Vector3 positionOffset = new Vector3(1, 0, 0);
        Quaternion rotationOffset = new Quaternion(0, 5, 0, 0);
        Quaternion negRotationOffset = new Quaternion(0, -5, 0, 0);


        GameObject ball2 = ObjectPool.Instance.GetCannonball();
        ball2.transform.position = barrelEnd.transform.position + positionOffset;
        ball2.transform.rotation = barrelEnd.transform.rotation * rotationOffset;
        ball2.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        ball2.SetActive(true);

        GameObject ball3 = ObjectPool.Instance.GetCannonball();
        ball3.transform.position = barrelEnd.transform.position - positionOffset;
        ball3.transform.rotation = barrelEnd.transform.rotation * negRotationOffset;
        ball3.gameObject.GetComponent<Renderer>().material.color = Color.red;
        ball3.SetActive(true);

        ball2.GetComponent<Rigidbody>().AddForce(ball2.transform.forward * -cannonballSpeed);
        ball3.GetComponent<Rigidbody>().AddForce(ball3.transform.forward * -cannonballSpeed);
    }

    IEnumerator ShotCooldown() {
        yield return new WaitForSeconds(cooldownLength);
        canShoot = true;
    }

    IEnumerator Powerup1Length() {
        tripleShot = true;
        yield return new WaitForSeconds(powerup1Duration);
        tripleShot = false;
    }

    public void EnableTripleShot() {
        StartCoroutine(Powerup1Length());
    }
}
