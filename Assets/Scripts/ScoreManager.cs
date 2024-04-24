using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // 确保包含此命名空间

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 分数显示的文本组件
    public TextMeshProUGUI perfectText; // Perfect 计数显示的文本组件
    public TextMeshProUGUI greatText; // Great 计数显示的文本组件
    public TextMeshProUGUI missText; // Miss 计数显示的文本组件

    public static int finalScore; // 用于存储最终分数的静态变量
    public static int perfectCount; // 用于存储 Perfect 计数的静态变量
    public static int greatCount; // 用于存储 Great 计数的静态变量
    public static int missCount; // 用于存储 Miss 计数的静态变量

    void Start()
    {
        Debug.Log("Final Score: " + finalScore); // 在控制台中打印最终分数
        scoreText.text = "Score: " + finalScore.ToString(); // Display the final score
        perfectText.text = "Perfect: " + perfectCount.ToString(); // // Display the count of Perfect hits
        greatText.text = "Great: " + greatCount.ToString(); // Display the count of Great hits
        missText.text = "Miss: " + missCount.ToString(); // Display the count of Misses
    }
    public void LoadSongSelectionScene()
    {
        // 
        SceneManager.LoadScene("Song Selection Scene");
    }
}
