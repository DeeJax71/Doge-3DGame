using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody pRigidBody;
    private float x, y, z, xMovement, yMovement, zMovement;
    private bool checkpointReached = false;
    public bool groundCheck;
    public Vector3 respawnPoint;


    [SerializeField] public float movementSpeed, jumpForce;
    [SerializeField] MeshRenderer colorCheckMesh, safePlatformMesh, meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        pRigidBody= GetComponent<Rigidbody>();      
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

        if (other.gameObject.CompareTag("Checkpoint"))
        { 
            respawnPoint = transform.position;
            checkpointReached= true;
            movementSpeed = 15;
        }
        if (other.gameObject.CompareTag("FallingDeath") && checkpointReached == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("FallingDeath") && checkpointReached == true)
        {
            transform.position = respawnPoint;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }
        if (collision.gameObject.CompareTag("Sphere"))
        {
            transform.position = respawnPoint;
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
}
