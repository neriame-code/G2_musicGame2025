using UnityEngine;

public class PlayerGoLeftState : IPlayerState
{
    private PlayerObject playerObject;
    public PlayerGoLeftState(PlayerObject playerObject) { this.playerObject = playerObject; }

    public void EnterState()
    {
        Debug.Log("左移動入力");
        playerObject.MoveToLine(PlayerObject.MoveDirection.Left); // 左に移動
        // アニメーションを起動
        playerObject.ChangeState(PlayerObject.PlayerState.Idle);
    }

    public void Update() { }

    public void ExitState() { }
}
