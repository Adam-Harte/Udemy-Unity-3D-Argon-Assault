using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathVfx;
    [SerializeField]
    int hitPoints = 1;
    [SerializeField]
    int hitScoreBonus = 1;
    [SerializeField]
    int killScoreBonus = 1;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        hitPoints--;
        scoreBoard.IncreaseScore(hitScoreBonus);

        if (hitPoints < 1) {
            DestroyEnemy(); 
        }
    }

    private void DestroyEnemy() {
        Instantiate(deathVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
        scoreBoard.IncreaseScore(killScoreBonus);
    }
}
