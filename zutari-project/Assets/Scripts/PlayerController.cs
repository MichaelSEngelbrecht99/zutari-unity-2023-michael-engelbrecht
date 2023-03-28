using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField, Range(1f, 1000)] private float playerMovementSpeed = 1;
    [SerializeField, Range(0.01f, 100)] private float playerDragForce = 1;
    [SerializeField] private bool movingLeft, movingRight, movingUp, movingDown;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI dragText;
    [SerializeField] private TextMeshProUGUI velocityText;

    private Rigidbody rb;
    private Material playerCubeRenderer;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        playerCubeRenderer = GetComponent<Renderer>().sharedMaterial;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        PlayerAdjustDrag();
    }

    // Called every frame to register player input. Used to move up, down, left, and right using WASD (Supports diagonal movement as well)
    public void PlayerInput()
    {
        // Moves Player UP
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("Player moving up");
            rb.AddForce(Vector3.forward * playerMovementSpeed, ForceMode.Impulse);
            movingUp = true;
        }
        else
        {
            movingUp = false;
        }

        // Moves Player LEFT
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Player moving left");
            rb.AddForce(-Vector3.right * playerMovementSpeed, ForceMode.Impulse);
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }

        // Moves Player DOWN
        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("Player moving down");
            rb.AddForce(-Vector3.forward * playerMovementSpeed, ForceMode.Impulse);
            movingDown = true;
        }
        else
        {
            movingDown = false;
        }

        // Moves Player RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Player moving right");
            rb.AddForce(Vector3.right * playerMovementSpeed, ForceMode.Impulse);
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        // Calls method to change color based on player input
        ChangeMaterialColor();
    }

    // Adjusts Color based on player input of direction
    public void ChangeMaterialColor()
    {
        if (movingUp == true)
        {
            playerCubeRenderer.color = Color.red;
        }

        if (movingLeft == true)
        {
            playerCubeRenderer.color = Color.blue;
        }

        if (movingDown == true)
        {
            playerCubeRenderer.color = Color.yellow;
        }

        if (movingRight == true)
        {
            playerCubeRenderer.color = Color.cyan;
        }

        // No input detected will make cube black
        if (!Input.anyKey)
        {
            playerCubeRenderer.color = Color.black;
        }
    }

    // Adjust Drag of Rigidbody in Update based on settings of player
    public void PlayerAdjustDrag()
    {
        if (rb != null)
        {
            rb.drag = playerDragForce;
            rb.angularDrag = playerDragForce;
        }
    }

    public void IncreaseDrag()
    {
        rb.drag = rb.drag + 0.05f;
        dragText.text = string.Format("{0} {1}", rb.drag, "Drag");
    }

    public void DecreseDrag()
    {
        rb.drag = rb.drag - 0.05f;
        dragText.text = string.Format("{0} {1}", rb.drag, "Drag");
    }

    public void IncreaseVelocity()
    {
        playerMovementSpeed = playerMovementSpeed + 0.05f;
        velocityText.text = string.Format("{0} {1}", playerMovementSpeed, "Drag");
    }

    public void DecreaseVelocity()
    {
        playerMovementSpeed = playerMovementSpeed - 0.05f;
        velocityText.text = string.Format("{0} {1}", playerMovementSpeed, "Drag");
    }
}
