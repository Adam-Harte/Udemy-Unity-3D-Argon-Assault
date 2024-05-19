using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathVfx;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
        scoreBoard.IncreaseScore(1);
    }
}
