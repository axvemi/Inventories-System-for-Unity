using UnityEngine;
using UnityEngine.UI;

namespace Axvemi.Inventories.ClassicInventory
{
    public class ClassicInventoryTrashUIController : MonoBehaviour
    {
        [SerializeField] private ClassicInventoryCursorController cursorController = null;
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
