using UnityEngine;

public class DestroyNotesLine : MonoBehaviour
{
    // ƒgƒŠƒK[‚É‰½‚©‚ªN“ü‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚é
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"ƒgƒŠƒK[‚ÉG‚ê‚Ü‚µ‚½: {other.gameObject.name}");

        NoteObject note = other.GetComponent<NoteObject>();
        if (note != null)
        {
            Destroy(other.gameObject);
        }
    }
}