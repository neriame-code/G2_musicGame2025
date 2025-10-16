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

        // TODO : あとでMusicSelectから取得するように変更
        // LoadMusic(0);
        musicData = defaultMusicData;
        musicSource.clip = musicData.MusicClip;
    }

    public void LoadMusic(int musicID)
    {
        if (MusicDataBase.Instance == null)
        {
            Debug.LogError("MusicDataBase.Instance が見つかりません");
            musicData = defaultMusicData;
            return;
        }
        else
        {
            // MusicDataBase から MusicData を取得
            musicData = MusicDataBase.Instance.GetMusicDataByID(musicID);

            if (musicData == null)
            {
                Debug.LogError($"MusicID: {musicID} に対応する MusicData が見つかりませんでした");
                musicData = defaultMusicData;
                return;
            }
        }

        if (musicData.MusicClip == null)
        {
            Debug.LogError($"MusicID: {musicID} に対応する MusicClip が見つかりませんでした");
            musicData = defaultMusicData;
        }
        
        musicSource.clip = musicData.MusicClip;

        if (musicData.MusicScore == null)
        {
            Debug.LogError($"MusicData: {musicData.MusicName} に楽譜データ (TextAsset) が設定されていません");
            musicData = defaultMusicData;
        }
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }
}
