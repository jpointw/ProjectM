using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 10.0f;
    public float minZoomZ = -10.0f;
    public float maxZoomZ = 10.0f; 
    public Vector3 minBounds = new Vector3(-50, -50, -50);
    public Vector3 maxBounds = new Vector3(50, 50, 50);

    private Vector3 dragOrigin;
    private bool isDragging;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleDrag();
        HandleRotation();
        HandleZoom();
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 dragDelta = (dragOrigin - currentMousePosition) * dragSpeed * Time.deltaTime;

            Vector3 move = new Vector3(dragDelta.x, 0, dragDelta.y);
            mainCamera.transform.Translate(move, Space.World);

            mainCamera.transform.position = new Vector3(
                Mathf.Clamp(mainCamera.transform.position.x, minBounds.x, maxBounds.x),
                mainCamera.transform.position.y,
                Mathf.Clamp(mainCamera.transform.position.z, minBounds.z, maxBounds.z)
            );

            dragOrigin = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            mainCamera.transform.Rotate(Vector3.up, -rotationX, Space.World);
            mainCamera.transform.Rotate(Vector3.right, rotationY, Space.Self);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Vector3 localPosition = mainCamera.transform.localPosition;

        localPosition.z = Mathf.Clamp(localPosition.z + scroll, minZoomZ, maxZoomZ);
        mainCamera.transform.localPosition = localPosition;
    }
}