using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventoryCell : MonoBehaviour, ISelectHandler
    {
        [Header("Visuals")]
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private GameObject stackCount;
        [SerializeField]
        private TMP_Text stackCountText;

        private Button _button;
        private Item _currentItem;
        private Sprite _defaultItemImage;

        public void OnSelect(BaseEventData eventData)
        {
            InventoryManager.Instance.SelectItem(_currentItem);
        }
        
        void Awake()
        {
            _button = GetComponent<Button>();
            _defaultItemImage = itemImage.sprite;
        }

        public void UpdateCell(Item itemToAssign)
        {
            _currentItem = itemToAssign;

            itemImage.sprite = _currentItem.sprite;
            // _button.interactable = true;
            var currentCount = InventoryManager.Instance.Items[_currentItem];
            if (currentCount > 1)
            {
                stackCount.SetActive(true);
                stackCountText.text = currentCount.ToString();
            }
            else if (stackCount.activeSelf)
            {
                stackCount.SetActive(false);
            }
        }

        public void ClearCell()
        {
            _currentItem = null;

            itemImage.sprite = _defaultItemImage;
            // _button.interactable = false;
            stackCount.SetActive(false);
        }
    }
}
