using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour
{
    private Camera mainCamera;
    public AudioClip pitfallSound;
    private AudioSource audioSource;

    void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Spawn = GameObject.FindGameObjectWithTag("Entrance");
            other.transform.position = transform.position;
            StartCoroutine(ShrinkPlayer(other.transform, Spawn));
        }
    }

    private IEnumerator ShrinkPlayer(Transform playerTransform, GameObject spawn)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().EnableMovement();

        yield return new WaitForSeconds(0.75f);

        audioSource.PlayOneShot(pitfallSound);

        Vector3 originalScale = playerTransform.localScale;
        Vector3 targetScale = new Vector3(0.1f, 0.1f, 1f);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.localScale = targetScale;

        playerTransform.position = spawn.transform.position;

        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = spawn.transform.position.x;
        cameraPosition.y = spawn.transform.position.y;
        mainCamera.transform.position = cameraPosition;

        yield return null;

        playerTransform.localScale = originalScale;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DisableMovement();
    }
}
