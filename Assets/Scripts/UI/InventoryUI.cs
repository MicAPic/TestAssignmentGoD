using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject inventory;
        public Button deleteButton;
        
        [SerializeField]
        private GameObject inventoryCellPrefab;
        [SerializeField]
        private GameObject inventoryCellContainer;
        private List<InventoryCell> _inventoryCells = new();

        void OnEnable()
        {
            InventoryManager.Instance.OnItemChangedCallback += UpdateInventory;
            
            for (var i = 0; i < InventoryManager.Instance.MaxStackCount; i++)
            {
                var cell = Instantiate(inventoryCellPrefab, inventoryCellContainer.transform);
                _inventoryCells.Add(cell.GetComponent<InventoryCell>());
            }
            inventory.SetActive(false);
        }

        void OnDisable()
        {
            InventoryManager.Instance.OnItemChangedCallback -= UpdateInventory;
        }

        private void UpdateInventory()
        {
            var i = 0;
            foreach (var itemCounterPair in InventoryManager.Instance.Items)
            {
                _inventoryCells[i].UpdateCell(itemCounterPair.Key);
                i++;
            }

            while (i < _inventoryCells.Count)
            {
                _inventoryCells[i].ClearCell();
                i++;
            }
        }
    }
}
