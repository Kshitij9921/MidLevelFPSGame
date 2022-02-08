using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    public GameObject Player;
    public GameObject zombiePrefab;
    public int number;
    public float spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        if (!Player.activeSelf)
        {
            return;
        }
        //we want to randomly spawn the zombies
        for (int i = 0; i < number; i++)
        {
            Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab, hit.position, Quaternion.identity);
            }
             
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
