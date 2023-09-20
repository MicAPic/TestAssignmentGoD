using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public Dictionary<Item, int> Items;

    public readonly int MaxStackCount = 24;
    
    [SerializeField]
    private InventoryUI inventoryUI;
    private Item _selectedItem;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        inventoryUI.deleteButton.onClick.AddListener(() => Remove(_selectedItem));
    }

    public void ToggleInventory()
    {
        inventoryUI.inventory.SetActive(!inventoryUI.inventory.activeInHierarchy);
        if (!inventoryUI.inventory.activeInHierarchy)
        {
            inventoryUI.deleteButton.gameObject.SetActive(false);
            _selectedItem = null;
        }
    }

    public void SelectItem(Item item)
    {
        inventoryUI.deleteButton.gameObject.SetActive(true);
        _selectedItem = item;
    }

    public bool HasItem(Item item)
    {
        return Items.ContainsKey(item);
    }

    public void Add(Item item)
    {
        Items.TryGetValue(item, out var currentCount);
        Items[item] = currentCount + 1; // currentCount will be 0 if the key wasn't initialized before
        // Debug.Log($"{item.itemName}: {Items[item]}");

        OnItemChangedCallback?.Invoke();
    }

    public void Remove(Item item)
    {
        Items.TryGetValue(item, out var currentCount);
        if (currentCount == 0)
        {
            Debug.LogWarning("Trying to remove an item that you don't have");
            return;
        }
        
        if (currentCount - 1 == 0)
        {
            Items.Remove(item);
        }
        else
        {
            Items[item] = currentCount - 1;
        }

        OnItemChangedCallback?.Invoke();
    }
}
