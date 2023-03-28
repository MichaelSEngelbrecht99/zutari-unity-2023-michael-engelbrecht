using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapPlayer : MonoBehaviour
{
    [Header("Screen Bounds View & Buffer/Distance Setting")]
    [Header("====================")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] float screenLeftConstraint = Screen.width;
    [SerializeField] float screenRightConstraint = Screen.width;
    [SerializeField] float screenBottomConstraint = Screen.height;
    [SerializeField] float screenTopConstraint = Screen.height;
    float buffer = 0.5f;
    float distanceZ;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetScreenConstraints();
    }

    // FixedUpdate is called every fixed frame-rate frame
    private void FixedUpdate()
    {
        RepositionPlayer();
    }

    #region Determine and reposition Player methods

    // Gets constraints based on screen's resolution/aspect ration (camera view)
    public void GetScreenConstraints()
    {
        distanceZ = Mathf.Abs(mainCamera.transform.position.z + transform.position.z);
        screenLeftConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        screenRightConstraint = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        screenBottomConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).z;
        screenTopConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).z;
    }
    // Reposition player if out of bounds of camera
    public void RepositionPlayer()
    {
        if (transform.position.x < screenLeftConstraint - buffer)
        {
            transform.position = new Vector3(screenRightConstraint - 0.10f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > screenRightConstraint)
        {
            transform.position = new Vector3(screenLeftConstraint, transform.position.y, transform.position.z);
        }

        if (transform.position.z < screenBottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, screenTopConstraint + buffer);
        }

        if (transform.position.z > screenTopConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, screenBottomConstraint - buffer);
        }
    }
    #endregion
}
