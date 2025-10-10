using System;
using UnityEngine;

[Serializable]
public class ScoreData
{
    [SerializeField] private int maxBlock;
    [SerializeField] private int BPM;
    [SerializeField] private int offset;
    [SerializeField] private Note[] notes;

    public int MaxBlock => maxBlock;
    public int Bpm => BPM;
    public int Offset => offset;
    public Note[] Notes => notes;
}
