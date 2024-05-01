using UnityEngine;
using UnityEngine.UI; // 这个是用于UI类，比如Button
using UnityEngine.SceneManagement;

public class MusicSelectionManager : MonoBehaviour
{
    // 封面按钮的引用数组
    public Button[] coverButtons;

    // 用于保存当前选中的音乐名称
    private string selectedMusicName;

    void Start()
    {
        // 为每个封面添加点击事件监听
        foreach (Button coverButton in coverButtons)
        {
            // 使用匿名函数来传递封面按钮的名称
            coverButton.onClick.AddListener(() => SelectMusic(coverButton.gameObject.name));
        }
    }

    // 选中音乐的方法
    public void SelectMusic(string musicName)
    {
        Debug.Log("Music name received in SelectMusic: " + musicName);
        selectedMusicName = musicName; // 更新当前选中的音乐名称
        PlayerPrefs.SetString("SelectedMusic", selectedMusicName); // 在这里设置PlayerPrefs
        PlayerPrefs.Save(); // 立即保存更改到PlayerPrefs
        Debug.Log("Music selected: " + selectedMusicName); // 这应该输出选中的音乐名称
    }

    //Method used for testing
    /*public void TestSelectMusic()
    {
        string testMusicName = "Contrapasso"; // 只是一个测试名称
        Debug.Log("Test music name received in SelectMusic: " + testMusicName);
        PlayerPrefs.SetString("SelectedMusic", testMusicName); // 使用测试名称设置PlayerPrefs
        PlayerPrefs.Save(); // 立即保存更改到PlayerPrefs
        Debug.Log("Test music selected: " + PlayerPrefs.GetString("SelectedMusic", "No music found")); // 这应该输出测试的音乐名称
    }*/

    // 确认选择并开始游戏的方法
    public void ConfirmSelectionAndStartGame()
    {
        // selectedMusicName = PlayerPrefs.GetString("SelectedMusic", "");
        Debug.Log("Confirming selection: " + selectedMusicName);
        // 检查是否选择了音乐
        if (!string.IsNullOrEmpty(selectedMusicName))
        {
            // 无需再次设置PlayerPrefs，因为选中时已经设置
            // PlayerPrefs.SetString("SelectedMusic", selectedMusicName); 

            // 保存所选音乐对应的音符文件名
            PlayerPrefs.SetString("SelectedNoteSequence", selectedMusicName);
            PlayerPrefs.Save(); // 确保保存
            // 载入游戏场景
            SceneManager.LoadScene("Gameplay Scene");
        }
        else
        {
            Debug.Log("No music selected!");
        }
    }
}
