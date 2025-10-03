using UnityEngine;

public class PlayerGoRightState : IPlayerState
{
    private PlayerObject playerObject;
    public PlayerGoRightState(PlayerObject playerObject) { this.playerObject = playerObject; }

    public void EnterState()
    {
        Debug.Log("右移動入力");
        playerObject.MoveToLine(PlayerObject.MoveDirection.Right); // 右に移動
        // アニメーションを起動
        playerObject.ChangeState(PlayerObject.PlayerState.Idle);
    }

    public void Update(){}

    public void ExitState() { }
}
