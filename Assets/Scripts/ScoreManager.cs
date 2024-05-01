using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Ensure inclusion of the TMPro namespace for text manipulation.

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 分数显示的文本组件。Display component for the score.
    public TextMeshProUGUI perfectText; // Perfect 计数显示的文本组件。Display component for the count of "Perfect" hits.
    public TextMeshProUGUI greatText; // Great 计数显示的文本组件。Display component for the count of "Great" hits.
    public TextMeshProUGUI missText; // Miss 计数显示的文本组件。Display component for the count of "Misses".

    public static int finalScore; // 用于存储最终分数的静态变量。Static variable to store the final score.
    public static int perfectCount; // 用于存储 Perfect 计数的静态变量。Static variable to store the count of "Perfect" hits.
    public static int greatCount; // 用于存储 Great 计数的静态变量。Static variable to store the count of "Great" hits.
    public static int missCount; // 用于存储 Miss 计数的静态变量。Static variable to store the count of "Misses".

    void Start()
    {
        // Log and display score and hit counts at the start of the scene.
        // 在场景开始时记录并显示分数和击中计数。
        Debug.Log("Final Score: " + finalScore); // Log the final score to the console.
        scoreText.text = "Score: " + finalScore.ToString(); // Display the final score.
        perfectText.text = "Perfect: " + perfectCount.ToString(); // Display the count of Perfect hits.
        greatText.text = "Great: " + greatCount.ToString(); // Display the count of Great hits.
        missText.text = "Miss: " + missCount.ToString(); // Display the count of Misses.
    }
    public void LoadSongSelectionScene()
    {
        // Load the song selection scene when called.
        // 当被调用时，加载歌曲选择场景。
        SceneManager.LoadScene("Song Selection Scene");
    }
}
