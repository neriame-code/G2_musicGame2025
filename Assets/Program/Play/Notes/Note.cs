using UnityEngine;

public class Note : MonoBehaviour
{
    private int floatSpeed = 70;

    void Update()
    {
        transform.Translate(0, -floatSpeed * Time.deltaTime, 0);
    }
}
