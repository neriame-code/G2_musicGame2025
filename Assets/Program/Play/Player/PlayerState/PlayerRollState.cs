using UnityEngine;
using UnityEngine.UI;

public class PlayerRollState : IPlayerState
{
    [SerializeField] private Image rollImage;

    private PlayerObject playerObject;
    public PlayerRollState(PlayerObject playerObject) { this.playerObject = playerObject; }

    public void EnterState()
    {
        Debug.Log("回転入力");
        // アニメーション処理など
        playerObject.ChangeState(PlayerObject.PlayerState.Idle);
    }

    public void Update()
    {

    }

    public void ExitState()
    {
    }
}
