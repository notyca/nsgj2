using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int hp = 3;
    [SerializeField] private HitPoints hitPointsUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Respawn()
    {
        //were making a roguelike, so dying just restarts the whole game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void HurtPlayer()
    {
        hp -= 1;
        if(hp <= 0 )
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<PlayerMovement>().dashing)
        {
            return;
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            HurtPlayer();
            hitPointsUI.ShowHitPoints(hp);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<PlayerMovement>().dashing)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            HurtPlayer();
            hitPointsUI.ShowHitPoints(hp);
        }
    }
}
