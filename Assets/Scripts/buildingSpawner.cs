using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingSpawner : MonoBehaviour
{
    public Building prefab;
    public float spawnrate = 1f;
    public float minheight = -1f;
    public float maxheight = 2f;
    public List<GameObject> spawnedBuildings = new List<GameObject>();

    private bool isSpawning = true;  // Flag for spawning control

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnrate, spawnrate);
    }

    private void Spawn()
    {
        if (isSpawning)
        {
            Building newbuilding = Instantiate(prefab, transform.position, Quaternion.identity);
            newbuilding.transform.position += Vector3.up * Random.Range(minheight, maxheight);
            spawnedBuildings.Add(newbuilding.gameObject);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    // Add a method to resume spawning
    public void ResumeSpawning()
    {
        isSpawning = true;
    }
}
