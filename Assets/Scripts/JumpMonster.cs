using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMonster : Monster
{
    public float jumpDelay = 2.0f;
    public float jumpForce = 2.0f;
    public Vector3 jump;

    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        StartCoroutine("JumpLoop");
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Jump() {
        gameObject.GetComponent<Rigidbody>().AddForce(jump * jumpForce, ForceMode.Impulse);
    }

    public void LoadMonster() {
        StartCoroutine("JumpLoop");
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpDelay);
            Jump();
        }
    }
}
