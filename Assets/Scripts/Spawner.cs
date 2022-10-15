using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject testObject;
    public GameObject[] spawners;
    public float waitingTime = 1f;
    public GameObject[] Meteors;

    private EndGameManager endManager;

    // Start is called before the first frame update
    void Start()
    {
        endManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<EndGameManager>();
        InvokeRepeating("SpawnMeteor", 1f, MainMenu.spawnRate);
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Spawns meteor
    void SpawnMeteor()
    {
       
        if(endManager.gameOver == false)
        {
            for (int i = 0; i < 3; i ++)
            {
                var whichSpawner = spawners[GetRandomNumber(0,16)];
                var whichMeteor = Meteors[Random.Range(0, 4)];
                Instantiate(whichMeteor, whichSpawner.transform.position, Quaternion.identity);
            }
        }
    }

    private int lastNumber = -1;

    // Returns random number without chance of repeat number
    private int GetRandomNumber(int min, int max)
    {
        int rand = Random.Range(min, max);
        while(rand == lastNumber)
        {
            rand = Random.Range(min, max);
        }
        lastNumber = rand;
        return rand;
    }
}
