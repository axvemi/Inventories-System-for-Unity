using UnityEngine;
using UnityEngine.UI;

namespace Axvemi.ClassicInventory
{
    public class InventoryTrashUIController : MonoBehaviour
    {
        [SerializeField] private InventoryCursorController cursorController = null;
        private void Start() {
            GetComponent<Button>().onClick.AddListener(OnClickTrash);
        }

        /// <summary>
        /// Sets the cursor item to null
        /// </summary>
        private void OnClickTrash(){
            cursorController.Slot.StoreItem(null);
        }
    }
}
