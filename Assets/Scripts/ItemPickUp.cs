using UnityEngine;

public class ItemPickUp : Interactable
{
    [SerializeField]
    private Item item;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
}
