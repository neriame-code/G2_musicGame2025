using System;
using UnityEngine;

[Serializable]
public class Note
{
    [SerializeField] private int block; // ���[���ԍ�
    [SerializeField] private int type;  // �m�[�g�^�C�v
    [SerializeField] private int num;   // �����ڂ�
    [SerializeField] private int LPB;   // 1���߂����������邩

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