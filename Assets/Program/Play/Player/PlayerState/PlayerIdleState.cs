using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerObject;

public class PlayerIdleState : IPlayerState
{
    private PlayerObject playerObject;
    public PlayerIdleState(PlayerObject playerObject){ this.playerObject = playerObject; }

    public void EnterState()
    {
        if (playerObject != null && playerObject.PlayerActionMap != null)
        {
            // アクションの取得とイベントの購読
            playerObject.PlayerActionMap.FindAction("MoveLeft").performed += OnMoveLeftPerformed;
            playerObject.PlayerActionMap.FindAction("MoveRight").performed += OnMoveRightPerformed;
            playerObject.PlayerActionMap.FindAction("SpaceAction").performed += OnSpaceActionPerformed;
            playerObject.PlayerActionMap.FindAction("JumpAction").performed += OnJumpActionPerformed;
        }
    }

    public void Update()
    {

    }

    public void ExitState()
    {
        // オブジェクト破棄時にイベントの購読を解除
        if (playerObject != null)
        {
            playerObject.PlayerActionMap.FindAction("MoveLeft").performed -= OnMoveLeftPerformed;
            playerObject.PlayerActionMap.FindAction("MoveRight").performed -= OnMoveRightPerformed;
            playerObject.PlayerActionMap.FindAction("SpaceAction").performed -= OnSpaceActionPerformed;
        }
    }

    private void OnMoveLeftPerformed(InputAction.CallbackContext context){ playerObject.ChangeState(PlayerState.GoLeft); }
    private void OnMoveRightPerformed(InputAction.CallbackContext context){ playerObject.ChangeState(PlayerState.GoRight); }
    private void OnSpaceActionPerformed(InputAction.CallbackContext context){ playerObject.ChangeState(PlayerState.Roll); }

    private void OnJumpActionPerformed(InputAction.CallbackContext context) { playerObject.ChangeState(PlayerState.Jump); }
}
