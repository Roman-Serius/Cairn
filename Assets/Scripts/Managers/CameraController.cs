using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Transition Settings")]
    public float transitionSpeed = 5f;
    public bool smoothTransition = true;

    private Vector3 targetPosition;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (smoothTransition && isTransitioning)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);

            // Stop once close enough
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isTransitioning = false;
            }
        }
        else if (!smoothTransition && isTransitioning)
        {
            transform.position = targetPosition;
            isTransitioning = false;
        }
    }
   

    public void MoveToRoom(Bounds roomBounds)
    {
        // center the camera on the room
        targetPosition = new Vector3(roomBounds.center.x, roomBounds.center.y, transform.position.z);
        isTransitioning = true;
    }
}