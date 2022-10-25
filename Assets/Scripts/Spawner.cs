using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject rootSpawn;

    public GameObject SpawnObject()  {
        GameObject newObject = Instantiate(objectToSpawn);
        newObject.transform.position= rootSpawn.transform.position;
        newObject.transform.parent = rootSpawn.transform;
        
        return newObject;
    }
}
