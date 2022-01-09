using UnityEngine;
using UnityEngine.InputSystem;

namespace Axvemi.ClassicInventory
{
    public class CursorFollowMouse : MonoBehaviour
    {

        [SerializeField] private RectTransform canvasRectTransform = null;
        private RectTransform rectTransform = null;

        #region MONOBEHAVIOUR

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
        }
        private void OnEnable() {
            Vector2 startingPosition = rectTransform.anchoredPosition;
        }

        private void Update() {
            FollowMousePosition();
        }
        #endregion


        /// <summary>
        /// Follows the mouse position
        /// </summary>
        private void FollowMousePosition(){
            rectTransform.anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransform.localScale.x;
        }
    }
}
