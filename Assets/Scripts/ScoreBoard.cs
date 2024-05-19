using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
    }
}
