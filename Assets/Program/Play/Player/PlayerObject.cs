using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerObject: MonoBehaviour
{
    [SerializeField] private Image playerImage;
    // レーンの親オブジェクトを配列で管理
    [SerializeField] private Transform[] lines;

    // 現在のレーンインデックス
    private int currentLineIndex = 2; // 中央のレーンからスタート
    public enum MoveDirection { Left = -1, Right = 1 }

    private IPlayerState currentState;
    public enum PlayerState{ Idle, GoLeft, GoRight, Roll, Jump }
    private Dictionary<PlayerState, IPlayerState> playerStates = new Dictionary<PlayerState, IPlayerState>();

    private PlayerInput playerInput;
    private InputActionMap playerActionMap;
    public InputActionMap PlayerActionMap { get => this.playerActionMap; }

    void Awake()
    {
        playerStates.Add(PlayerState.Idle, new PlayerIdleState(this));
        playerStates.Add(PlayerState.GoLeft, new PlayerGoLeftState(this));
        playerStates.Add(PlayerState.GoRight, new PlayerGoRightState(this));
        playerStates.Add(PlayerState.Roll, new PlayerRollState(this));
        playerStates.Add(PlayerState.Jump, new PlayerJumpState(this));

        playerInput = GetComponent<PlayerInput>();
        playerActionMap = playerInput.actions.FindActionMap("PlayerAction");

        ChangeState(PlayerState.Idle);
    }

    void Start()
    {
        if (0 < lines.Length)
        {
            transform.SetParent(lines[currentLineIndex], false); // ゲーム開始時に中央のレーンを親に設定
        }
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(PlayerState nextState)
    {
        if (playerStates.ContainsKey(nextState))
        {
            if (currentState != null)
                currentState.ExitState();
            currentState = playerStates[nextState];
            currentState.EnterState();
        }
    }

    public void MoveToLine(MoveDirection direction)
    {
        int newLineIndex = currentLineIndex + (int)direction;
        if (0 <= newLineIndex && newLineIndex < lines.Length)
        {
            transform.SetParent(lines[newLineIndex], false); // 親を新しいレーンに設定
            currentLineIndex = newLineIndex; // インデックスを更新
        }
    }

    public void SetImage(Image image)
    {
        playerImage.color = Color.yellow;
        //if(image != null)
        //    playerImage = image;
    }

    public void ResetImage()
    {
        playerImage.color = Color.white;
    }
}
