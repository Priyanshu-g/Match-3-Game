using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMaster : MonoBehaviour
{

    public BallSpawnerScript[] spawners;
    public bool[] spawned;
    public int[] mostRecents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawned[i] = spawners[i].spawnCom;
            mostRecents[i] = spawners[i].mostRecent;
        }
    }
}
