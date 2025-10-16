using System;
using UnityEngine;

[Serializable]
public class Note
{
    [SerializeField] private int block; // レーン番号
    [SerializeField] private int type;  // ノートタイプ
    [SerializeField] private int num;   // 何拍目か
    [SerializeField] private int LPB;   // 1小節を何分割するか

    public int Block { get => block; set => block = value; }
    public int Type { get => type; set => type = value; }
    public int Num { get => num; set => num = value; }
    public int Lpb { get => LPB; set => LPB = value; }
}

public enum NoteType
{
    NORMAL = 1,
    SMASH = 2,
}