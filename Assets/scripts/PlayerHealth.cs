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
        // check if collision was with bullet. can do this in other ways if this is too limited
        if (collision.TryGetComponent(out bullet b))
        {
            HurtPlayer();
            hitPointsUI.ShowHitPoints(hp);
        }
    }

    public void Heal() {
        hp = 3;
    }
}
