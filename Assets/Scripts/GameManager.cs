using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("UI Panels")]
    public GameObject startScreen;
    public GameObject gameUI;
    public GameObject gameOverScreen;

    [Header("Score Elements")]
    public TextMeshProUGUI scoreText;

    [Header("Player")]
    public PlayerMovement player;
    public StackManager stackManager;

    public bool IsPlaying { get; private set; } = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        IsPlaying = false;
        Time.timeScale = 1f;
        startScreen.SetActive(true);
        gameUI.SetActive(false);
        gameOverScreen.SetActive(false);

        player.enabled = false;
    }

    public void StartGame()
    {
        IsPlaying = true;
        startScreen.SetActive(false);
        gameUI.SetActive(true);

        player.enabled = true;
    }

    public void GameOver()
    {
        if (!IsPlaying) return;
        IsPlaying = false;

        Debug.Log("Show Game Over UI");
        Invoke("ShowLoseUI", 1.5f);
    }

    public void LevelComplete()
    {
        if (!IsPlaying) return;
        IsPlaying = false;

        Debug.Log("Show Win UI");
        AudioManager.Instance.PlayWin();
        Invoke("ShowWinUI", 1f);
    }

    void ShowLoseUI()
    {
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);

        if (scoreText != null)
            scoreText.text = "SCORE: 0";
    }

    void ShowWinUI()
    {
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);

        CalculateScore();
    }

    void CalculateScore()
    {
        if (scoreText == null) return;

        StackManager stack = stackManager;

        int brickCount = 0;
        if (stack != null)
        {
            brickCount = stack.GetBrickCount();
        }

        int finalScore = brickCount * 10;


        int currentDisplayScore = 0;

        DOTween.To(() => currentDisplayScore, x => currentDisplayScore = x, finalScore, 1f)
            .OnUpdate(() =>
            {
                scoreText.text = "SCORE: " + currentDisplayScore.ToString();
            })
            .OnComplete(() =>
            {
                if (finalScore > PlayerPrefs.GetInt("HighScore", 0))
                {
                    scoreText.text += "\nNEW BEST!";
                    PlayerPrefs.SetInt("HighScore", finalScore);
                }
            });
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}