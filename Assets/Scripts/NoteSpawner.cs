using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab; // 音符预制体// Music note prefab
    public Transform spawnPoint; // 生成点，Circle下的Center对象// Where notes appear
    public Transform[] targetPoints = new Transform[8]; // 8个目标点// Points where notes move to
    public AudioSource musicPlayer;// Component to play the music
    public float baseInterval = 0.5f; // 基础间隔时间// Base interval between notes
    private int perfectCount = 0, greatCount = 0, missCount = 0; // 计数击中结果// Score counters

    void Start()
    {
        // 获取选中的音乐和音符序列
        // Retrieve selected music and note sequence from PlayerPrefs
        string selectedMusic = PlayerPrefs.GetString("SelectedMusic", "DefaultMusic");
        string selectedNoteSequence = PlayerPrefs.GetString("SelectedNoteSequence", "DefaultNoteSequence");

        // 加载音乐剪辑// Start music playback
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

        // 加载音符序列文件// Load and parse the note sequence text
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
        ScoreManager.finalScore = perfectCount * 100 + greatCount * 50; // 计算总分
        ScoreManager.perfectCount = perfectCount;
        ScoreManager.greatCount = greatCount;
        ScoreManager.missCount = missCount;
        Debug.Log($"Game Ended. Total Score: {ScoreManager.finalScore}, Perfects: {perfectCount}, Greats: {greatCount}, Misses: {missCount}");
        SceneManager.LoadScene("Score Scene"); // 加载得分场景
    }


    IEnumerator ParseAndSpawnNotes(string sequence)
    {
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
        EndGameAndLoadScoreScene(); // // Transition to score scene after last note
    }

    public void SpawnNote(int pointIndex)
    {
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, this.transform);
        Note noteScript = note.GetComponent<Note>();
        if (noteScript != null)
        {
            noteScript.Initialize(targetPoints[pointIndex], this);
        }
    }

    public void RegisterHit(string result)
    {
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
