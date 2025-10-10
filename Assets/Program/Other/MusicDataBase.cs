using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicDataBase : MonoBehaviour
{
    [SerializeField] List<MusicData> musicList;

    public static MusicDataBase Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("MusicDataBase ‚ª³‚µ‚­ì¬‚³‚ê‚Ü‚µ‚½");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public MusicData GetMusicDataByID(int musicID)
    {
        return musicList.FirstOrDefault(data => data.MusicID == musicID);
    }
}