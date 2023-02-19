using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChecker : MonoBehaviour
{
    private string greenMesh = "RGBA(0.000, 1.000, 0.000, 1.000)";
    private string redMesh = "RGBA(1.000, 0.000, 0.000, 1.000)";
    private string blueMesh = "RGBA(0.000, 0.000, 1.000, 1.000)";
    private MeshRenderer obstacle2Platforms, mazeWalls, obstacle4Platforms;
    private Rigidbody platformRigidBody;

    [SerializeField] MeshRenderer meshRenderer, colorCheckMesh;
    [SerializeField] PlayerMovement playerMovement;
    private GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.FindGameObjectWithTag("False Floor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Color Check"))
        {
            if (meshRenderer.material.color.ToString() == greenMesh && colorCheckMesh.GetComponent<Renderer>().material.color.ToString() == greenMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else if (meshRenderer.material.color.ToString() == redMesh && colorCheckMesh.GetComponent<Renderer>().material.color.ToString() == redMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else if (meshRenderer.material.color.ToString() == blueMesh && colorCheckMesh.GetComponent<Renderer>().material.color.ToString() == blueMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else
            {
                colorCheckMesh.enabled = false;
                floor.SetActive(false);
            }
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle2"))
        {
            playerMovement.groundCheck = true;

             obstacle2Platforms = collision.gameObject.GetComponent<MeshRenderer>();

            if (obstacle2Platforms.material.color == meshRenderer.material.color)
            {
                playerMovement.movementSpeed = 20;
            }
            else
            {
                transform.position = playerMovement.respawnPoint;
            }
        }

        if (collision.gameObject.CompareTag("Obstacle3"))
        {
            playerMovement.groundCheck = true;

            mazeWalls = collision.gameObject.GetComponent<MeshRenderer>();
            BoxCollider mazeCollider = collision.gameObject.GetComponent<BoxCollider>();

            if (mazeWalls.material.color == meshRenderer.material.color)
            {
                mazeCollider.enabled = false;
            }
        }

        if (collision.gameObject.CompareTag("Obstacle4"))
        {
            playerMovement.groundCheck = true;

            platformRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            obstacle4Platforms = collision.gameObject.GetComponent<MeshRenderer>();

            if (meshRenderer.material.color != obstacle4Platforms.material.color)
            {
                platformRigidBody.constraints = RigidbodyConstraints.None;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        MeshRenderer mazeWalls = collision.gameObject.GetComponent<MeshRenderer>();
        BoxCollider mazeCollider = collision.gameObject.GetComponent<BoxCollider>();

        if (mazeWalls.material.color == meshRenderer.material.color && mazeCollider.enabled == false)
        {
            mazeCollider.enabled = true;
        }
    }
}
