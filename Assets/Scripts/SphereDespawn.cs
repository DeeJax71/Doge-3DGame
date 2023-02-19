using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDespawn : MonoBehaviour
{
    private SphereDespawn despawnScript;

    [SerializeField] GameObject sphere;
    
    private void Start()
    {
        despawnScript = GetComponent<SphereDespawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(sphere);
            Destroy(despawnScript);
        }
    } 
}
