using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerObject: MonoBehaviour
{
    [SerializeField] private Image playerImage;
    // ���[���̐e�I�u�W�F�N�g��z��ŊǗ�
    [SerializeField] private Transform[] lines;

    // ���݂̃��[���C���f�b�N�X
    private int currentLineIndex = 2; // �����̃��[������X�^�[�g
    public enum MoveDirection { Left = -1, Right = 1 }

    private IPlayerState currentState;
    public enum PlayerState{ Idle, GoLeft, GoRight, Roll, Jump }
    private Dictionary<PlayerState, IPlayerState> playerStates = new Dictionary<PlayerState, IPlayerState>();

    private PlayerInput playerInput;
    private InputActionMap playerActionMap;
    public InputActionMap PlayerActionMap { get => this.playerActionMap; }

    private int life = 5;
    public int Life { get => this.life; set => this.life = value; }

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
            transform.SetParent(lines[currentLineIndex], false); // �Q�[���J�n���ɒ����̃��[����e�ɐݒ�
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
            transform.SetParent(lines[newLineIndex], false); // �e��V�������[���ɐݒ�
            currentLineIndex = newLineIndex; // �C���f�b�N�X���X�V
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NoteObject note = collision.GetComponent<NoteObject>();
        if (note != null)
        {
            if(0 < life)
                life--;
            Debug.Log("Life: " + life);
        }
    }
}
