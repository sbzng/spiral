using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    // 方法用于加载指定的场景
    public void LoadScene()
    {
        SceneManager.LoadScene("Song Selection Scene");
    }
}
