using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Axvemi.Inventories.ClassicInventory
{
    public class ClassicInventoryCursorController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private ClassicInventoryMoveAmmountUIController moveAmmountUIController = null;
        private ClassicInventorySlot slot = new ClassicInventorySlot();
        public ClassicInventorySlot Slot { get => slot; set => slot = value; }
        private InputAction clickInputAction;


        #region MONOBEHAVIOUR
        private void Awake() {
            clickInputAction = playerInput.actions["Click"];
        }
        private void OnEnable() {
            clickInputAction.performed += OnClickPerformed;
        }
        private void OnDisable() {
            clickInputAction.performed -= OnClickPerformed;
        }
        #endregion

        #region INPUT
        private void OnClickPerformed(InputAction.CallbackContext context){
            ClassicInventorySlot hoverSlot = GetSlotAtMousePosition(Mouse.current.position.ReadValue());
            if(hoverSlot == null) return;

            moveAmmountUIController.gameObject.SetActive(false);
            //Move x ammount dialog
            if(Keyboard.current.leftShiftKey.isPressed == true){
                //From hover slot to cursor
                if(slot.Item == null){
                    moveAmmountUIController.SetupMoveAmmount(hoverSlot, slot);
                }
                //From cursor to hover
                else{
                    //Dont show if the hover slot item is not the same type or null
                    if(hoverSlot.Item != slot.Item && hoverSlot.Item != null) return;
                    
                    moveAmmountUIController.SetupMoveAmmount(slot, hoverSlot);
                }
            }
            //Move all the ammount
            else{
                //Move from the hover slot to the cursor
                if(slot.Item == null) {
                    hoverSlot.MoveItemToSlot(this.slot, hoverSlot.Ammount);
                }
                //Move from the cursor to the hover slot
                else {
                    slot.MoveItemToSlot(hoverSlot, slot.Ammount);
                }
            }


        }
        #endregion

        #region INVENTORY SLOTS

        /// <summary>
        /// Gets the Inventory Slot that its at the mouse position
        /// </summary>
        /// <param name="position">Position in the canvas</param>
        /// <returns>Inventory Slot</returns>
        private ClassicInventorySlot GetSlotAtMousePosition(Vector2 position) {
            PointerEventData pointerEventData = new PointerEventData(null);
            pointerEventData.position = position;
            //Debug.Log("Position: " + pointerEventData.position);

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach(RaycastResult result in raycastResults) {
                ClassicInventorySlotUIController slotUIController = result.gameObject.GetComponent<ClassicInventorySlotUIController>();
                if(slotUIController != null) {
                    return slotUIController.Slot;
                }
            }

            return null;
        }
        #endregion
    }
}
