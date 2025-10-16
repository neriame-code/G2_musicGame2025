using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    [SerializeField] Color smashObjectColor;
    private float targetTime; // �m�[�g�����莞���ɓ��B���ׂ��Q�[�����̎��ԁi�b�j
    private float noteSpeed; // �m�[�g�̗���鑬���iZ�������̈ړ����x�j

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
        // ���݂̃Q�[������
        float currentTime = Time.time;

        // �m�[�g�����胉�C���ɓ��B����܂ł̎c�莞��
        float timeRemaining = targetTime - currentTime;

        float targetY = timeRemaining * noteSpeed;

        Vector3 newPosition = transform.localPosition;
        newPosition.y = targetY;
        transform.localPosition = newPosition;
    }
}