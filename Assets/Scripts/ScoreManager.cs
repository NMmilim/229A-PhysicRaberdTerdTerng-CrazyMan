using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int winScore = 1000;

    public TMP_Text scoreText;

    private bool hasWon = false;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        CheckWin();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckWin()
    {
        if (!hasWon && score >= winScore)
        {
            hasWon = true;

            // เปลี่ยนไปหน้า Credit
            SceneManager.LoadScene("EndCredits");
        }
    }


}