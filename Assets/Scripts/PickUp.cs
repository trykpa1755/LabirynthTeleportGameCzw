using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip pickClip;

    private bool _collected = false;

    public virtual void Picked()
    {
        if (_collected) return;
        _collected = true;

        // opcjonalnie: natychmiast blokujemy kolejne hity
        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log("Podnios³em");
        Destroy(gameObject);
    }

    public void Rotation()
    {
        transform.Rotate(new Vector3(0f, 0.5f, 0f));
    }
}