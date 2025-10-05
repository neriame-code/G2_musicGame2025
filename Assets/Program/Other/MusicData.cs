using UnityEngine;

[CreateAssetMenu]
public class MusicData : ScriptableObject
{
    [SerializeField] private string musicName;
    [SerializeField] private int musicID;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private TextAsset musicScore;

    public string MusicName => musicName;
    public int MusicID => musicID;
    public AudioClip MusicClip => musicClip;
    public TextAsset MusicScore => musicScore;
}
