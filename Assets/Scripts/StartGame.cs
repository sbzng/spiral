using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // 调用这个方法来加载选歌页面的场景
    public void LoadSongSelection()
    {
        SceneManager.LoadScene("Song Selection Scene");
    }
}
