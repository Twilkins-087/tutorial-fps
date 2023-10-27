using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickupSpawnSpace : MonoBehaviour
{
    [SerializeField] GameObject[] pickups = new GameObject[0];
    [SerializeField] float spawnInterval = 14f;

    BoxCollider _box;
    float _lastSpawnTime;

    private void Awake()
    {
        _box = GetComponent<BoxCollider>();
        _lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastSpawnTime > spawnInterval)
            Spawn();
    }

    void Spawn()
    {
        int iPickup = Random.Range(0, pickups.Length);
        var pickup = pickups[iPickup];

        var position = new Vector3(
            Random.Range(_box.bounds.min.x, _box.bounds.max.x),
            Random.Range(_box.bounds.min.y, _box.bounds.max.y),
            Random.Range(_box.bounds.min.z, _box.bounds.max.z)
        );
        Instantiate(pickup, position, Quaternion.identity);
        _lastSpawnTime = Time.time;
    }
}
