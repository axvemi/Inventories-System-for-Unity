using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Axvemi.ClassicInventory
{
    public class InventoryMoveAmmountUIController : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField] private Button acceptButton = null;
        [SerializeField] private TMP_InputField inputField = null;
        [SerializeField] private Slider slider = null;

        private int ammountToTranfer = 0;

        private InventorySlot originSlot = null;
        private InventorySlot targetSlot = null;

        #region MONOBEHAVIOUR
        private void Awake() {
            acceptButton.onClick.AddListener(OnAcceptButton);
            slider.onValueChanged.AddListener(OnSliderUpdated); 
            inputField.onValueChanged.AddListener(OnInputFieldUpdated);   
        }
        
        #endregion

        public void SetupMoveAmmount(InventorySlot originSlot, InventorySlot targetSlot){
            this.gameObject.SetActive(true);
            this.originSlot = originSlot;
            this.targetSlot = targetSlot;

            ammountToTranfer = 1;
            slider.minValue = 1;
            slider.maxValue = originSlot.Ammount;
            inputField.text = "1";
        }

        

        #region UI
        /// <summary>
        /// Called by the UI Button
        /// Transfers the selected ammount from the origin slot to the target slot
        /// </summary>
        public void OnAcceptButton(){
            originSlot.MoveItemToSlot(targetSlot, ammountToTranfer);
            this.gameObject.SetActive(false);
        }

        public void OnSliderUpdated(float value){
            ammountToTranfer = (int)value;
            inputField.text = ammountToTranfer.ToString();
        }

        public void OnInputFieldUpdated(string text){
            int value = int.Parse(text);
            value = Mathf.Clamp(value, 1, originSlot.Ammount);
            ammountToTranfer = value;
            slider.value = ammountToTranfer;
        }
        #endregion
    }
}
