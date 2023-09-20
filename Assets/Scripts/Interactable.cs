using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    protected abstract void OnTriggerEnter2D(Collider2D col);
}
