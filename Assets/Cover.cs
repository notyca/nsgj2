using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowCover()
    {
        float elapsed = 0;
        while(elapsed < 1)
        {
            elapsed += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, elapsed / 1);
            yield return null;
        }
    }

    public IEnumerator HideCover()
    {
        float elapsed = 0;
        while (elapsed < 1)
        {
            elapsed += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1 - (elapsed / 1));
            yield return null;
        }
    }

    public void CoverImmediate()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
    }
}
