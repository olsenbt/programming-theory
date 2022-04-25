using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup1 : Powerup
{
    public CanonController cannonScript;
    // Start is called before the first frame update
    void Start()
    {
        cannonScript = GameObject.Find("Main Camera").GetComponent<CanonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public override void Activate()
    {
        cannonScript.EnableTripleShot();
    }
}
