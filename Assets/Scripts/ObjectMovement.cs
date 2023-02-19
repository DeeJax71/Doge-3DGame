using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
     private int currentWaypoint = 0;

     [SerializeField] private GameObject[] waypoints;
     [SerializeField] private float movementSpeed = 1f;

     void Start()
     {

     }

     void Update()
     {
         if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .1f)
         {
             currentWaypoint++;

             if (currentWaypoint >= waypoints.Length)
             { 
                 currentWaypoint = 0;
             }
         }

         transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, movementSpeed * Time.deltaTime);

     }
}
