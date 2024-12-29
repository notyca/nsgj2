using UnityEngine;

public class Door : MonoBehaviour
{
    private Camera mainCamera;
    private Transform player;

    public int direction;
    private float cameraMoveSpeed = 20f;

    private static float lastDoorUseTime = 0f;
    private float doorCooldown = 1f;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= lastDoorUseTime + doorCooldown)
        {
            lastDoorUseTime = Time.time;

            switch (direction)
            {
                case 1:
                    MoveStuff(Vector3.up, 11, 11 - 5);
                    break;
                case 2:
                    MoveStuff(Vector3.down, 11, 11 - 5);
                    break;
                case 3:
                    MoveStuff(Vector3.left, 16.5f, 16.5f - 10);
                    break;
                case 4:
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
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }
}
