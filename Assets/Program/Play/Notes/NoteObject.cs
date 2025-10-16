using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    [SerializeField] Color smashObjectColor;
    private float targetTime; // ノートが判定時刻に到達すべきゲーム内の時間（秒）
    private float noteSpeed; // ノートの流れる速さ（Z軸方向の移動速度）

    public void Initialize(float time, float speed, NoteType noteType)
    {
        targetTime = time;
        noteSpeed = speed;
        if (noteType == NoteType.SMASH)
        {
            GetComponent<Image>().color = smashObjectColor;
        }

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // 現在のゲーム時間
        float currentTime = Time.time;

        // ノートが判定ラインに到達するまでの残り時間
        float timeRemaining = targetTime - currentTime;

        float targetY = timeRemaining * noteSpeed;

        Vector3 newPosition = transform.localPosition;
        newPosition.y = targetY;
        transform.localPosition = newPosition;
    }
}