using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomCameraTrigger : MonoBehaviour
{
    public Room parentRoom;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentRoom != null)
            parentRoom.OnPlayerEnter();
    }
}
