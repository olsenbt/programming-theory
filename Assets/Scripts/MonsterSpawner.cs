using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float spawnRadius = 5.0f;
    public bool isFrozen = false;
    public float powerup2Duration = 10.0f;
    public int wave;

    public List<int> monsters;
    public List<GameObject> activeMonsters;

    private int normal;
    private int jump;
    private int tank;


    // Start is called before the first frame update
    void Start()
    {
        activeMonsters = new List<GameObject>();
        wave = 0;
        normal = 0;
        jump = 0;
        tank = 0;

        StartCoroutine(StartWaveOne());
    }

    IEnumerator StartWaveOne() {
        yield return new WaitForSeconds(5.0f);
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnMonster(int monsterType) {
        GameObject monster = ObjectPool.Instance.GetEnemy(monsterType);
        activeMonsters.Add(monster);
        PositionMonster(monster);
        monster.SetActive(true);
        if (monsterType == 2) {
            monster.GetComponent<JumpMonster>().LoadMonster();
        }
    }

    void PositionMonster(GameObject monster) {
        float y = 1.1f;

        float angle = Random.Range((Mathf.PI / 4), (3 * Mathf.PI / 4)); // pi/4 - 3pi/4

        float x = Mathf.Cos(angle) * spawnRadius;
        float z = Mathf.Sin(angle) * spawnRadius;

        Vector3 location = new Vector3(x, y, z);

        Vector3 relativePos = new Vector3(0, y, 0) - location;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        monster.gameObject.transform.position = location;
        monster.gameObject.transform.rotation = rotation;
    }

    public void SpawnPowerup(int powerupType, Vector3 location) {
        GameObject powerup = ObjectPool.Instance.GetPowerup(powerupType);
        powerup.gameObject.transform.position = location;
        powerup.SetActive(true);
    }

    public void Freeze() {
        StartCoroutine(FreezeDuration());
    }

    IEnumerator FreezeDuration() {
        isFrozen = true;
        yield return new WaitForSeconds(powerup2Duration);
        isFrozen = false;
    }

    public void SpawnWave() {
        Debug.Log("SpawnWave()");
        wave++;
        monsters = GenerateWave();
        Debug.Log("wave " + wave);
        Debug.Log("normal: " + normal);
        Debug.Log("jump: " + jump);
        Debug.Log("tank:" + tank);
        StartCoroutine(SpawnCooldown());
    }

    IEnumerator SpawnCooldown() {

        Debug.Log("starting spawning loop");
        float pause = 1.0f;
        //float pause = Random.Range(1, 5);
        //pause *= wave % 5 * .5f;

        Debug.Log("spawning monster " + monsters[0]);
        SpawnMonster(monsters[0]);
        monsters.Remove(monsters[0]); // remove
        // wait before spawning more
        yield return new WaitForSeconds(pause);

        Debug.Log(monsters.Count + " monsters remaining");
        Debug.Log(monsters);
        if (monsters.Count > 0)
        {
            Debug.Log("keep spawning");
            StartCoroutine(SpawnCooldown()); // recursive
        }
        else {
            Debug.Log("Done spawning");
        }
    }

    // reterns a list of monsters in the wave randomized
    // ex: monsters {1, 2, 1, 1, 3, 3}
    List<int> GenerateWave() {
        int adjustedWave;
        if (wave % 5 == 0)
        {
            adjustedWave = 1;
        }
        else {
            adjustedWave = wave % 5;
        }

        // waves 1-5      normal only
        normal = Random.Range(1, 4);


        if (wave > 5) { // waves 6-10   jump + normal
            jump = Random.Range(1, 4);
        }

        if (wave > 10) { // waves 11 - 15   tank, jump, normal
            tank = Random.Range(1, 4);
        }

        // adjust difficulty by wave

        normal = (int) Mathf.Ceil(normal * adjustedWave * .5f);
        jump = (int) Mathf.Ceil(jump * adjustedWave * .5f);
        tank = (int) Mathf.Ceil(tank * adjustedWave * .5f);
        List<int> monsters = new List<int>();

        // add elements to list
        for (int i = 0; i < normal; i++) {
            monsters.Add(1);
        }

        for (int i = 0; i < jump; i++) {
            monsters.Add(2);
        }

        for (int i = 0; i < tank; i++) {
            monsters.Add(3);
        }


        System.Random random = new System.Random();

        int n = monsters.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(random.NextDouble() * (n - i));
            int temp = monsters[r];
            monsters[r] = monsters[i];
            monsters[i] = temp;
        }

        return monsters;
    }

    public void TestEndWave() {
        if (activeMonsters.Count == 0)
        {
            Debug.Log("0 monsters, new wave");
            if (wave <= 15)
            {
                SpawnWave();
            }
            else {
                Debug.Log("You win!");
            }
        }
    }
}
