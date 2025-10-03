using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField] private Note notePrefab;
    [SerializeField] private Transform canvasTransform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
            GenerateNote();
    }

    public void GenerateNote()
    { 
        Instantiate(notePrefab, canvasTransform);
    }
}
