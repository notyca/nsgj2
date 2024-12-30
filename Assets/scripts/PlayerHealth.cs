using Unity.VisualScripting;
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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Necklace>().hasNecklace) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Necklace>().SpawnNecklace();
            return;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Sounds>().playHurtSound();
        //hp -= 1;
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

    public void Heal() {
        hp = 3;
        hitPointsUI.ShowHitPoints(hp);
    }
}
