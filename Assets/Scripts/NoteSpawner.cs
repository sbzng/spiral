using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab; // 音符预制体// Music note prefab
    public Transform spawnPoint; // 生成点，Circle下的Center对象// Spawn point, typically the center of a circle in-game
    public Transform[] targetPoints = new Transform[8]; // 8个目标点// Target points for notes to move towards
    public AudioSource musicPlayer; // 音乐播放器组件// Component to play background music
    public float baseInterval = 0.5f; // 基础间隔时间// Base time interval between notes
    private int perfectCount = 0, greatCount = 0, missCount = 0; // 计分计数器// Counters for scoring

    void Start()
    {
        // Load and play selected music, and start the note spawning process
        // 加载并播放选定的音乐，并开始音符生成过程
        string selectedMusic = PlayerPrefs.GetString("SelectedMusic", "DefaultMusic");
        string selectedNoteSequence = PlayerPrefs.GetString("SelectedNoteSequence", "DefaultNoteSequence");

        // Load and play the music clip
        // 加载并播放音乐剪辑
        AudioClip musicClip = Resources.Load<AudioClip>("Audio/" + selectedMusic);
        if (musicClip)
        {
            musicPlayer.clip = musicClip;
            musicPlayer.Play();
        }
        else
        {
            Debug.LogError("Music clip not found in resources: " + selectedMusic);
        }

        // Load the note sequence file and start parsing it
        // 加载音符序列文件并开始解析
        TextAsset noteData = Resources.Load<TextAsset>(selectedNoteSequence);
        if (noteData)
        {
            StartCoroutine(ParseAndSpawnNotes(noteData.text));
        }
        else
        {
            Debug.LogError("Note sequence file not found in resources: " + selectedNoteSequence);
        }
    }

    public void StartNoteSequenceFromText(string fileName)
    {
        // Load note data from a file and start the spawning process
        // 从文件加载音符数据并开始生成过程
        TextAsset noteData = Resources.Load<TextAsset>(fileName);
        if (noteData != null)
        {
            StartCoroutine(ParseAndSpawnNotes(noteData.text));
        }
        else
        {
            Debug.LogError("Failed to load note sequence file: " + fileName);
        }
    }

    public void EndGameAndLoadScoreScene()
    {
        // Calculate final score and transition to the score scene
        // 计算最终得分并过渡到得分场景
        ScoreManager.finalScore = perfectCount * 100 + greatCount * 50;
        ScoreManager.perfectCount = perfectCount;
        ScoreManager.greatCount = greatCount;
        ScoreManager.missCount = missCount;
        Debug.Log($"Game Ended. Total Score: {ScoreManager.finalScore}, Perfects: {perfectCount}, Greats: {greatCount}, Misses: {missCount}");
        SceneManager.LoadScene("Score Scene");
    }

    IEnumerator ParseAndSpawnNotes(string sequence)
    {
        // Parse the note sequence and spawn notes at the designated times
        // 解析音符序列并在指定时间生成音符
        foreach (char ch in sequence)
        {
            if (ch == ',')
            {
                yield return new WaitForSeconds(baseInterval);
                continue;
            }
            if (int.TryParse(ch.ToString(), out int pointIndex))
            {
                pointIndex -= 1;
                if (pointIndex >= 0 && pointIndex < targetPoints.Length)
                {
                    SpawnNote(pointIndex);
                }
                else
                {
                    Debug.LogError("Point index out of range: " + pointIndex);
                }
            }
        }
        yield return new WaitForSeconds(3f); // Wait for the last notes to be processed
        EndGameAndLoadScoreScene(); // End the game and load the score scene
    }

    public void SpawnNote(int pointIndex)
    {
        // Instantiate a note at the spawn point and initialize it
        // 在生成点实例化一个音符并初始化
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, this.transform);
        Note noteScript = note.GetComponent<Note>();
        if (noteScript != null)
        {
            noteScript.Initialize(targetPoints[pointIndex], this);
        }
    }

    public void RegisterHit(string result)
    {
        // Update score counters based on the hit result
        // 根据击中结果更新得分计数器
        switch (result)
        {
            case "Perfect":
                perfectCount++;
                break;
            case "Great":
                greatCount++;
                break;
            case "Miss":
                missCount++;
                break;
        }
    }
}
