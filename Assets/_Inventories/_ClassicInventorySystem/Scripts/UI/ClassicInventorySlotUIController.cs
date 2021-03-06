using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Axvemi.Inventories.ClassicInventory
{
    /// <summary>
    /// Controls a inventory slot
    /// </summary>
    public class ClassicInventorySlotUIController : MonoBehaviour
    {
        private ClassicInventorySlot slot = null;
        public ClassicInventorySlot Slot { get => slot; set => slot = value; }

        private void Update() {
            ShowSlot();
        }

        #region UI
        
        [SerializeField] private Image image = null;
        [SerializeField] private TextMeshProUGUI ammountText = null;

        #endregion

        //TODO: Listen when the item gets updated
        private void ShowSlot() {
            if(slot == null) return;

            if(slot.Item == null) {
                image.gameObject.SetActive(false);
                ammountText.gameObject.SetActive(false);
            }
            else {
                image.gameObject.SetActive(true);
                image.sprite = slot.Item.Sprite;
                ammountText.gameObject.SetActive(true);
                ammountText.SetText(slot.Ammount.ToString());
            }
        }

    }
}
