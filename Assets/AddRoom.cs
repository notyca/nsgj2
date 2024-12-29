using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    public Vector2 roomPosition = Vector2.zero;

    public int roomType = 15;

    void Start() {
        templates = GetComponentInParent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
