using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicScript : MonoBehaviour
{
    public int PlayerScore;
    public GameObject ScoreText;
    public GameObject GameOverState;

    public void AddScore()
    {
        PlayerScore += 1;
        ScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        GameOverState.SetActive(true);
    }
}
