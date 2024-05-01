using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    void Start() {
        // 确保按钮在不需要的场景中不可见
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckActiveState();
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        CheckActiveState();
    }

    private void CheckActiveState() {
        // 根据当前场景名称决定按钮是否激活
        gameObject.SetActive(SceneManager.GetActiveScene().name == "Gameplay Scene");
    }

    public void LoadScene() {
        SceneManager.LoadScene("Song Selection Scene");
    }
}
