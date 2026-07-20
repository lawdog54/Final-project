using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float startingTime = 100f;
    private float currentTime;

    public TextMeshProUGUI timerText;

    private bool gameOver = false;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (gameOver)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;

        UpdateUI();

        if (currentTime <= 0)
        {
            LoseGame();
        }
    }

    void UpdateUI()
    {
        timerText.text = "Charge: " + Mathf.CeilToInt(currentTime) + "%";
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
    }

    void LoseGame()
    {
        gameOver = true;

        Debug.Log("Game Over!");

        // Replace this later with your Game Over screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}