using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpState : IPlayerState
{
    // [SerializeField] private Image jumpImage;

    private PlayerObject playerObject;
    public PlayerJumpState(PlayerObject playerObject) { this.playerObject = playerObject; }

    public void EnterState()
    {
        Debug.Log("ジャンプ入力");
        // playerObject.SetImage(jumpImage);
        // アニメーション処理など
        playerObject.ChangeState(PlayerObject.PlayerState.Idle);
    }

    public void Update() { }

    public void ExitState() { }
}
