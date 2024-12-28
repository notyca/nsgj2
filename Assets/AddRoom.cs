using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Start() {
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
