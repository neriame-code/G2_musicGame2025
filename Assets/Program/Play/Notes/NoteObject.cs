using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private float targetTime; // ノートが判定時刻に到達すべきゲーム内の時間（秒）
    private float noteSpeed; // ノートの流れる速さ（Z軸方向の移動速度）

    public void Initialize(float time, float speed)
    {
        targetTime = time;
        noteSpeed = speed;
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