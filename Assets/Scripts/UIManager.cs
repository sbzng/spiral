using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI feedbackText;  // Assign value in Inspector
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowFeedback(string message, Vector3 position)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            // 设置文本位置到点击点旁边
            feedbackText.transform.position = Camera.main.WorldToScreenPoint(position + new Vector3(0, 0.5f, 0));
            Invoke("HideFeedback", 2.0f);  // Hide text after 2 seconds
        }
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Play sound effects
        }
    }

    private void HideFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
}
