using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner1 : MonoBehaviour
{
    private float timer = 0;

    [SerializeField] private GameObject spheres;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {            
            Instantiate(spheres, spawnPoint.position, spawnPoint.rotation);
            timer = 0;
        }
    }
}