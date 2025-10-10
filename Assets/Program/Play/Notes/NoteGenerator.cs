using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    private int noteNum;
    [SerializeField] private int targetMusicID = 0;

    private List<int> LaneNum = new List<int>();
    private List<int> NoteType = new List<int>();
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
        NoteType.Clear();
        NotesTime.Clear();

        ScoreData scoreData = LoadMusic(targetMusicID); // ID を使って曲を読み込む
        if (scoreData != null)
        {
            NoteGenerate(scoreData);
        }
        else
        {
            Debug.LogError("ノーツの生成を開始できませんでした。");
        }
    }

    private ScoreData LoadMusic(int musicID)
    {
        if (MusicDataBase.Instance == null)
        {
            Debug.LogError("MusicDataBase.Instance が見つかりません。シーンに配置されているか確認してください。");
        }

        // MusicDataBase から MusicData を取得
        MusicData musicData = MusicDataBase.Instance.GetMusicDataByID(musicID);

        if (musicData == null)
        {
            Debug.LogError($"MusicID: {musicID} に対応する MusicData が見つかりませんでした。");
        }

        if (musicData.MusicScore == null)
        {
            Debug.LogError($"MusicData: {musicData.MusicName} に楽譜データ (TextAsset) が設定されていません。");
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("NoteGenerator の lines 配列にレーンオブジェクトが設定されていません。");
        }

        string inputString = musicData.MusicScore.text;
        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(inputString);

        if (scoreData.Notes == null || scoreData.Notes.Length == 0)
        {
            Debug.LogWarning($"曲名: {musicData.MusicName} にノーツデータがありませんでした。");
        }

        return scoreData;
    }

    public void NoteGenerate(ScoreData scoreData)
    {
        noteNum = scoreData.Notes.Length;

        for (int i = 0; i < scoreData.Notes.Length; i++)
        {
            float interval = 60f / (scoreData.Bpm * (float)scoreData.Notes[i].Lpb);
            float beatSec = interval * (float)scoreData.Notes[i].Lpb;
            float time = (beatSec * scoreData.Notes[i].Num / (float)scoreData.Notes[i].Lpb) + (scoreData.Offset / 1000f) + 0.01f;

            NotesTime.Add(time);
            LaneNum.Add(scoreData.Notes[i].Block);
            NoteType.Add(scoreData.Notes[i].Type);

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
                noteObj.Initialize(time, NotesSpeed * scoreData.Bpm / 2);
            }
            else
            {
                Debug.LogError("notePrefub に NoteObject スクリプトがアタッチされていません！");
            }

            NotesObj.Add(newNote);
        }
    }
}