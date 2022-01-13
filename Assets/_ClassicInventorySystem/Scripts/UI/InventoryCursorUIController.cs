using UnityEngine;
using UnityEngine.UI;

namespace Axvemi.ClassicInventory
{
    /// <summary>
    /// Show the cursor slot
    /// </summary>
    public class InventoryCursorUIController : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCursorUIParent = null;
        [Header("UI")]
        [SerializeField] private Image itemImage = null;

        private InventoryCursorController cursorController = null;

        private void Start() {
            cursorController = GetComponent<InventoryCursorController>();
            cursorController.Slot.onSlotItemUpdated += OnCursorSlotItemUpdated;
            inventoryCursorUIParent.SetActive(false);
        }
        
        /// <summary>
        /// When the cursor slot item is updated change the visuals
        /// If the item is null show nothing
        /// </summary>
        /// <param name="item"></param>
        private void OnCursorSlotItemUpdated(InventoryItemSO item){
            if(item == null){
                inventoryCursorUIParent.SetActive(false);
            }
            else{
                inventoryCursorUIParent.SetActive(true);
                itemImage.sprite = item.Sprite;
            }
            
        }
    }
}
