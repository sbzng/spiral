using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // 单例模式，保证全局只有一个 UIManager 实例。Singleton pattern to ensure there is only one instance of UIManager.
    public TextMeshProUGUI feedbackText;  // 在检查器中分配这个变量。Assign value in the Inspector.

    private AudioSource audioSource; // AudioSource component for playing sounds.

    void Awake()
    {
        // Ensure that only one instance of UIManager exists.
        // 确保只有一个 UIManager 实例存在。
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure it persists across scenes.
        }
        else
        {
            Destroy(gameObject); // Destroy any extra instances.
        }
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.
    }

    public void ShowFeedback(string message, Vector3 position)
    {
        // Show feedback messages on the screen.
        // 在屏幕上显示反馈信息。
        if (feedbackText != null)
        {
            feedbackText.text = message; // Set the text of feedback.
            feedbackText.gameObject.SetActive(true); // Make the text visible.
            // Set text position next to the specified game position.
            // 设置文本位置到指定游戏位置旁边。
            feedbackText.transform.position = Camera.main.WorldToScreenPoint(position + new Vector3(0, 0.5f, 0));
            Invoke("HideFeedback", 2.0f);  // Automatically hide feedback after 2 seconds.
        }
    }

    public void PlaySound()
    {
        // Play sound effect if AudioSource is available.
        // 如果 AudioSource 可用，播放声音效果。
        if (audioSource != null)
        {
            audioSource.Play();  // Play the sound.
        }
    }

    private void HideFeedback()
    {
        // Hide the feedback text.
        // 隐藏反馈文本。
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false); // Make the text invisible.
        }
    }
}
