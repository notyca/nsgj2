using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    public Vector2 roomPosition = Vector2.zero;

    public int roomType = 15;

    void Start() {
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
