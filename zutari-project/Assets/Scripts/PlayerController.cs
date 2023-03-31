using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [Header("====================")]
    [SerializeField, Range(1f, 1000)] private float playerMovementSpeed = 1;
    [SerializeField, Range(0.01f, 100)] private float playerDragForce = 1;
    [SerializeField, Range(0.01f, 100)] private float incrementSize = 0.01f;
    [SerializeField] private bool movingLeft, movingRight, movingUp, movingDown;

    [Header("UI Settings")]
    [Header("====================")]
    [SerializeField] private TextMeshProUGUI dragText;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI incrementSizeText;

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
        UpdateRigidValues();
        PlayerInput();
    }

    #region Player Settings and Methods
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
    #endregion

    #region UI Settings and Methods
    // UI Button events to increase/decrease Drag and Velocity of Player Cube
    public void AdjustDrag(bool dir)
    {
        switch (dir)    // True = Increase ; False = Decrease
        {
            case true:
                playerDragForce = rb.drag + incrementSize;
                UpdateRigidValues();
                break;
            case false:
                playerDragForce = rb.drag - incrementSize;
                UpdateRigidValues();
                break;
        }

    }

    public void AdjustVelocity(bool dir)
    {
        switch (dir)    // True = Increase ; False = Decrease
        {
            case true:
                playerMovementSpeed = playerMovementSpeed + incrementSize;
                UpdateRigidValues();
                break;
            case false:
                playerMovementSpeed = playerMovementSpeed - incrementSize;
                UpdateRigidValues();
                break;
        }
    }

    public void AdjustIncrements(bool dir)
    {
        switch (dir)    // True = Increase ; False = Decrease
        {
            case true:
                incrementSize = incrementSize + incrementSize;
                break;
            case false:
                incrementSize = incrementSize - (incrementSize / 2);
                break;
        }
    }
    public void UpdateRigidValues()
    {
        dragText.text = string.Format("{0} {1}", playerDragForce.ToString("F2"), "Drag");
        velocityText.text = string.Format("{0} {1}", playerMovementSpeed.ToString("F2"), "Velocity");
        incrementSizeText.text = string.Format("{0} {1}", incrementSize.ToString("F2"), "Increment Amount");
        PlayerAdjustDrag();
    }
    #endregion

}
