using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // 调用这个方法来加载选歌页面的场景
    public void LoadSongSelection()
    {
        // 这里假设您已经创建了一个名为 "SongSelectionScene" 的场景
        SceneManager.LoadScene("Song Selection Scene");
    }
}
