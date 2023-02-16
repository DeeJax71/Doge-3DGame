using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody pRigidBody;
    private float x, y, z, xMovement, yMovement, zMovement;
    private bool groundCheck, checkpointReached = false;
    private GameObject floor;
    private string redMesh = "RGBA(1.000, 0.000, 0.000, 1.000)", greenMesh = "RGBA(0.000, 1.000, 0.000, 1.000)", blueMesh = "RGBA(0.000, 0.000, 1.000, 1.000)";
    private Vector3 respawnPoint;


    [SerializeField] private float movementSpeed, jumpForce;
    [SerializeField] MeshRenderer colorCheckMesh, safePlatformMesh, meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        pRigidBody= GetComponent<Rigidbody>();      
        floor = GameObject.FindGameObjectWithTag("False Floor");
        PlatformColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        GrabXYZ();  
    }

    private void FixedUpdate()
    {
        pRigidBody.velocity = new Vector3(xMovement * movementSpeed, y, zMovement * movementSpeed);

        if (yMovement > 0 && groundCheck == true)
        {
            groundCheck = false;
            PlayerJump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Color Change"))
        {
            meshRenderer.material.color = Color.red;
        }
        else if (other.gameObject.CompareTag("Middle Color Change"))
        {
            meshRenderer.material.color = Color.green;
        }
        else if (other.gameObject.CompareTag("Right Color Change"))
        {
            meshRenderer.material.color = Color.blue;
        }

        if (other.gameObject.CompareTag("Color Check"))
        {
            if (meshRenderer.material.color.ToString() == greenMesh && colorCheckMesh.material.color.ToString() == greenMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else if (meshRenderer.material.color.ToString() == redMesh && colorCheckMesh.material.color.ToString() == redMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else if (meshRenderer.material.color.ToString() == blueMesh && colorCheckMesh.material.color.ToString() == blueMesh)
            {
                colorCheckMesh.enabled = false;
            }
            else
            {
                colorCheckMesh.enabled = false;
                floor.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        { 
            respawnPoint = transform.position;
            checkpointReached= true;
        }

        if (other.gameObject.CompareTag("FallingDeath") && checkpointReached == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("FallingDeath") && checkpointReached == true)
        {
            //Vector3 name = new Vector3(10.03f, 3.72f, 29.5f);

            transform.position = respawnPoint;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }
    }

    private void GrabXYZ()
    {
        x = pRigidBody.velocity.x;
        y = pRigidBody.velocity.y;
        z = pRigidBody.velocity.z;
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Jump");
        zMovement = Input.GetAxis("Vertical");
    }

    private void PlayerJump()
    {
        pRigidBody.velocity = new Vector3(x, 5, z);
    }

    private void PlatformColorChange()
    {
        int randomNumber = Random.Range(0 ,3);

        switch(randomNumber) 
        {
            case 0:
                colorCheckMesh.material.color = new Color(1f, 0, 0, 1f);
                safePlatformMesh.material.color = new Color(1f, 0, 0, 1f);
                break;
            case 1:
                colorCheckMesh.material.color = new Color(0, 1f, 0, 1f);
                safePlatformMesh.material.color = new Color(0, 1f, 0, 1f);
                break;
            case 2:
                colorCheckMesh.material.color = new Color(0, 0, 1f, 1f);
                safePlatformMesh.material.color = new Color(0, 0, 1f, 1f);
                break;
        }
    }
}
