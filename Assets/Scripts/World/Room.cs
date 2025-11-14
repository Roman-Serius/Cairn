using UnityEngine;

[DisallowMultipleComponent]
public class Room : MonoBehaviour
{
    [Header("Room Settings")]
    public string roomName = "Room_1";
    public BoxCollider2D cameraBounds;
    public bool isStartRoom = false;
    public Color gizmoColor = new Color(1, 0, 0, 0.2f);

    private void OnDrawGizmos()
    {
        if (cameraBounds != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(cameraBounds.bounds.center, cameraBounds.bounds.size);
        }
    }

    public void OnPlayerEnter()
    {
        if (CameraController.instance != null && cameraBounds != null)
        {
            CameraController.instance.MoveToRoom(cameraBounds.bounds);
        }
    }

    private void Start()
    {
        if (isStartRoom && CameraController.instance != null)
        {
            CameraController.instance.MoveToRoom(cameraBounds.bounds);
        }
    }
}