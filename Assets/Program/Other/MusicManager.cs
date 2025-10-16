using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] MusicData defaultMusicData;

    private static MusicManager instance;
    public static MusicManager Instance => instance;

    private MusicData musicData;
    public MusicData MusicData => musicData;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // TODO : ���Ƃ�MusicSelect����擾����悤�ɕύX
        // LoadMusic(0);
        musicData = defaultMusicData;
        musicSource.clip = musicData.MusicClip;
    }

    public void LoadMusic(int musicID)
    {
        if (MusicDataBase.Instance == null)
        {
            Debug.LogError("MusicDataBase.Instance ��������܂���");
            musicData = defaultMusicData;
            return;
        }
        else
        {
            // MusicDataBase ���� MusicData ���擾
            musicData = MusicDataBase.Instance.GetMusicDataByID(musicID);

            if (musicData == null)
            {
                Debug.LogError($"MusicID: {musicID} �ɑΉ����� MusicData ��������܂���ł���");
                musicData = defaultMusicData;
                return;
            }
        }

        if (musicData.MusicClip == null)
        {
            Debug.LogError($"MusicID: {musicID} �ɑΉ����� MusicClip ��������܂���ł���");
            musicData = defaultMusicData;
        }
        
        musicSource.clip = musicData.MusicClip;

        if (musicData.MusicScore == null)
        {
            Debug.LogError($"MusicData: {musicData.MusicName} �Ɋy���f�[�^ (TextAsset) ���ݒ肳��Ă��܂���");
            musicData = defaultMusicData;
        }
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }
}
