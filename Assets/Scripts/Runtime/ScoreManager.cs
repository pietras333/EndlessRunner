using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float score = 0f;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Canvas canvas;

    void Update()
    {
        score = playerTransform.position.z;
    }

    public void OnGameOver()
    {
        scoreText.text = "Score: " + score.ToString("0");
        canvas.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
