using System.Collections;
using UnityEngine;

public class NecklaceBullet : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        StartCoroutine(Fire());
        Destroy(gameObject, 5f); // Suicide
    }

    IEnumerator Fire()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 targetPosition = player.transform.position;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        while (true)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //kill enemy
            Destroy(gameObject); // Suicide
        }
    }
}
