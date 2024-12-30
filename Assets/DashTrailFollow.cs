using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DashTrailFollow : MonoBehaviour
{
    [SerializeField] private Transform follow;
    private float speed = 20f;
    public bool done = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, follow.position.x, speed * Time.deltaTime), Mathf.Lerp(transform.position.y, follow.position.y, speed * Time.deltaTime), transform.position.z);
    }

    public IEnumerator DelayDisappear()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
