using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private GameObject gameOverUI;
    [Space]

    [SerializeField] private TextMeshProUGUI timertext;
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount = 0;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }
    private void Update()
    {
        timertext.text = Time.time.ToString("F2") +"s";
    }
    public void EnableGameOver()
    {
        Time.timeScale = 0.5f;
        gameOverUI.SetActive(true);
    }
    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }

}
