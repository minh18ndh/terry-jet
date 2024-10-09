using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacle;
    [SerializeField] private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawn), 1.0f, spawnTime);
    }

    private void spawn()
    {
        if (!MegalodonGameManager.Instance.IsSceneEnd())
        {
            int i = Random.Range(0, obstacle.Length * 10);
            GameObject o = Instantiate(obstacle[i%obstacle.Length], transform.position, Quaternion.identity);
            o.transform.position = transform.position;
        }
    }
}
