using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField, Range(1f, 1000)] private float playerMovementSpeed = 1;
    [SerializeField, Range(0.01f, 100)] private float playerDragForce = 1;
    // Awake is called upon pressing play before the first frame is rendered
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.drag = playerDragForce;
            rb.angularDrag = playerDragForce;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    // Called every frame to register player input. Used to move up, down, left, and right using WASD
    public void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Player moving up");
            rb.AddForce(Vector3.forward * playerMovementSpeed, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Player moving left");
            rb.AddForce(-Vector3.right * playerMovementSpeed, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Player moving down");
            rb.AddForce(-Vector3.forward * playerMovementSpeed, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Player moving right");
            rb.AddForce(Vector3.right * playerMovementSpeed, ForceMode.Impulse);
        }

    }

    public void ChangeMaterialColor()
    {

    }
}
