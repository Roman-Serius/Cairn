using UnityEngine;

/// <summary>
/// Lets you click, drag, and rotate 2D physics stones.
/// Attach this to your Main Camera or a manager object.
/// </summary>
public class StoneDrag2D : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragForce = 15f;
    public float rotationSpeed = 180f; // degrees per second
    public bool rotateWithKeys = true; // toggle key-based rotation

    private Camera cam;
    private Rigidbody2D selectedBody;
    private Vector3 mouseWorldPos;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Convert mouse position to world
        mouseWorldPos = GetMouseWorldPos();
        mouseWorldPos.z = 0f;

        // Click to select
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.rigidbody != null)
            {
                selectedBody = hit.rigidbody;
                selectedBody.gravityScale = 0;
                selectedBody.linearVelocity = Vector2.zero;
                selectedBody.angularVelocity = 0;
            }
        }

        // While dragging
        if (Input.GetMouseButton(0) && selectedBody != null)
        {
            Vector2 targetPos = mouseWorldPos;
            Vector2 direction = (targetPos - selectedBody.position);
            selectedBody.linearVelocity = direction * dragForce;

            // Rotate with keys or scroll wheel
            if (rotateWithKeys)
            {
                if (Input.GetKey(KeyCode.R))
                    selectedBody.MoveRotation(selectedBody.rotation + rotationSpeed * Time.deltaTime);
                if (Input.GetKey(KeyCode.E))
                    selectedBody.MoveRotation(selectedBody.rotation - rotationSpeed * Time.deltaTime);
            }
            else
            {
                float scroll = Input.mouseScrollDelta.y;
                if (Mathf.Abs(scroll) > 0.1f)
                    selectedBody.MoveRotation(selectedBody.rotation + scroll * rotationSpeed * Time.deltaTime * 10f);
            }
        }

        // Release
        if (Input.GetMouseButtonUp(0) && selectedBody != null)
        {
            selectedBody.gravityScale = 1;
            selectedBody.linearVelocity = Vector2.zero;
            selectedBody = null;
        }
    }
    Vector3 GetMouseWorldPos()
    {
        // Make a ray from camera through mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Define the gameplay plane (Z = 0)
        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        // Find where the ray intersects that plane
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            worldPos.z = 0;
            return worldPos;
        }
        return Vector3.zero;
    }
}