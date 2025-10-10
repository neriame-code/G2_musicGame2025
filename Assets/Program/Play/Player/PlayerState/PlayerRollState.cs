using UnityEngine;
using UnityEngine.UI;

public class PlayerRollState : IPlayerState
{
    [SerializeField] private Image rollImage;

    private PlayerObject playerObject;
    public PlayerRollState(PlayerObject playerObject) { this.playerObject = playerObject; }

    public void EnterState()
    {
        Debug.Log("��]����");
        playerObject.SetImage(rollImage);
        // �A�j���[�V���������Ȃ�
        playerObject.ChangeState(PlayerObject.PlayerState.Idle);
    }

    public void Update()
    {

    }

    public void ExitState()
    {
        playerObject.ResetImage();
    }
}
