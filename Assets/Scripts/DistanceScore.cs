using UnityEngine;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 1f;

    private float startX;
    private int currentScore;
    private int highScore;

    void Start()
    {
        startX = player.position.x;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Best : " + highScore;
    }

    void Update()
    {
        float distance = Mathf.Max(0, player.position.x - startX);
        currentScore = Mathf.FloorToInt(distance * scoreMultiplier);
        scoreText.text = "Score : " + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            highScoreText.text = "Best : " + highScore;
        }
    }
}
