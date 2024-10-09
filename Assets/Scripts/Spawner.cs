using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scene1  
{
    public class Spawner : MonoBehaviour
    {
        [System.Serializable]
        public struct SpawnableObject
        {
            public GameObject prefab;
            [Range(0f, 1f)]
            public float spawnChance;
        }

        public SpawnableObject[] objects;
        public float minSpawnRate = 1f;
        public float maxSpawnRate = 2f;

        private void OnEnable()
        {
            Invoke(nameof(Spawn1), Random.Range(minSpawnRate, maxSpawnRate));
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Spawn1()
        {
            float spawnChance = Random.value;

            foreach (SpawnableObject obj in objects)
            {
                if (spawnChance < obj.spawnChance)
                {
                    GameObject obstacle = Instantiate(obj.prefab);
                    obstacle.transform.position += transform.position;
                    break;
                }

                spawnChance -= obj.spawnChance;
            }

            Invoke(nameof(Spawn1), Random.Range(minSpawnRate, maxSpawnRate));
        }
    }
}
