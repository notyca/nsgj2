using UnityEngine;

public class Door : MonoBehaviour
{
    private Camera mainCamera;
    public int direction;
    private Transform player;

    public float cameraSpeed = 20f;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (direction)
            {
                case 1:
                    // Door Up
                    MoveStuff(Vector3.up, 11, 11 - 5);
                    break;

                case 2:
                    // Door Down
                    MoveStuff(Vector3.down, 11, 11 - 5);
                    break;

                case 3:
                    // Door Left
                    MoveStuff(Vector3.left, 16.5f, 16.5f - 10);
                    break;

                case 4:
                    // Door Right
                    MoveStuff(Vector3.right, 16.5f, 16.5f - 10);
                    break;
            }
        }
    }

    public void MoveStuff(Vector3 direction, float cameraDistance, float playerDistance)
    {
        Vector3 targetCameraPosition = mainCamera.transform.position + direction * cameraDistance;
        Vector3 targetPlayerPosition = player.position + direction * playerDistance;
        StartCoroutine(SmoothMoveCamera(targetCameraPosition));
        player.position = targetPlayerPosition;
    }

    private System.Collections.IEnumerator SmoothMoveCamera(Vector3 targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
            yield return null;
        }
        mainCamera.transform.position = targetPosition;
    }
}
