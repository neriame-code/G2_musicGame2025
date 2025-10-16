using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    private int noteNum;

    private List<int> LaneNum = new List<int>();
    private List<NoteType> TypeList = new List<NoteType>();
    private List<float> NotesTime = new List<float>();
    private List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed = 10f; // デフォルト値を設定
    [SerializeField] GameObject notePrefub;
    [SerializeField] MusicData defaultMusicData;
    [Header("Lane Settings")]
    [SerializeField] private Transform[] lines;

    void Start()
    {
        noteNum = 0;
        LaneNum.Clear();
        TypeList.Clear();
        NotesTime.Clear();

        ScoreData scoreData = LoadScoreData();

        if (scoreData != null)
        {
            NoteGenerate(scoreData);
            MusicManager.Instance.PlayMusic();
        }
        else
            Debug.LogError("ノーツの生成を開始できませんでした");
    }

    private ScoreData LoadScoreData()
    {
        MusicData musicData;
        if (MusicManager.Instance == null)
        {
            Debug.LogError("MusicManager のインスタンスが見つかりません");
            musicData = defaultMusicData;
        }
        else
        {
            musicData = MusicManager.Instance.MusicData; // ID を使って曲を読み込む
        }

        string inputString = musicData.MusicScore.text;
        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(inputString);
        if (scoreData.Notes == null || scoreData.Notes.Length == 0)
            Debug.LogWarning($"曲名: {musicData.MusicName} にノーツデータがありませんでした");
        else
            return scoreData;

        string defaultInputString = defaultMusicData.MusicScore.text;
        ScoreData defaultScoreData = JsonUtility.FromJson<ScoreData>(inputString);
        return defaultScoreData;
    }

    public void NoteGenerate(ScoreData scoreData)
    {
        noteNum = scoreData.Notes.Length;

        for (int i = 0; i < scoreData.Notes.Length; i++)
        {
            float interval = 60f / (scoreData.Bpm * (float)scoreData.Notes[i].Lpb);
            float beatSec = interval * (float)scoreData.Notes[i].Lpb;
            float time = (beatSec * scoreData.Notes[i].Num / (float)scoreData.Notes[i].Lpb) + scoreData.Offset * 0.01f;

            NoteType noteType = (NoteType)scoreData.Notes[i].Type;

            NotesTime.Add(time);
            LaneNum.Add(scoreData.Notes[i].Block);
            TypeList.Add(noteType);

            // ノートがどのレーンに属するかを取得
            int laneIndex = scoreData.Notes[i].Block;

            int arrayIndex = laneIndex;

            if (arrayIndex < 0 || arrayIndex >= lines.Length)
            {
                Debug.LogError($"レーン番号 {laneIndex} は lines 配列の範囲外です (0 から {lines.Length - 1})。");
                continue;
            }

            // ノートの出現座標を計算
            float y = 0;

            // ノートのインスタンス化と親の設定
            GameObject newNote = Instantiate(
                notePrefub,
                new Vector3(0f, y, 0f),
                Quaternion.identity
            );

            newNote.transform.SetParent(lines[arrayIndex], false);

            NoteObject noteObj = newNote.GetComponent<NoteObject>();
            if (noteObj != null)
            {
                noteObj.Initialize(time, NotesSpeed * scoreData.Bpm / 2, noteType);
            }
            else
            {
                Debug.LogError("notePrefub に NoteObject スクリプトがアタッチされていません！");
            }

            NotesObj.Add(newNote);
        }
    }
}