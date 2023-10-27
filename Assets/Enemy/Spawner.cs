using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy toSpawn;
    [SerializeField] float spawnInterval = 2f;

    Transform _player;
    float _lastSpawnTime = 0;

    private void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            _player = players[0].transform;
        }
        else
        {
            Debug.LogError("Could not find a player in the scene");
        }

        _lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastSpawnTime > spawnInterval)
            Spawn();
    }


    void Spawn()
    {
        var enemy = Instantiate(toSpawn, transform.position, Quaternion.identity);
        enemy.target = _player;
        _lastSpawnTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.8f);
    }
}
