using UnityEngine;

public class Door : MonoBehaviour
{
    private Camera mainCamera;
    public int direction;
    public Transform player;
    void Start() {
        mainCamera = Camera.main;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            switch (direction)
            {
                case 1:
                    //Door Up
                    MoveStuff(Vector3.up);
                    break;

                case 2:
                    //Door Down
                    MoveStuff(Vector3.down);
                    break;

                case 3:
                    //Door Left
                    MoveStuff(Vector3.left);
                    break;

                case 4:
                    //Door Right
                    MoveStuff(Vector3.right);
                    break;
            }
        }
    }

    public void MoveStuff(Vector3 direction)
    {
        mainCamera.transform.position += direction * 100;
        player.position += direction * 95;
    }
}
