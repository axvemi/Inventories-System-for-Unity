using UnityEngine;
using UnityEngine.InputSystem;

namespace Axvemi.ClassicInventory
{
    public class CursorFollowMouse : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = null;
        private RectTransform canvasRectTransform = null;
        private RectTransform rectTransform = null;

        #region MONOBEHAVIOUR

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
        private void OnEnable() {
            Vector2 startingPosition = rectTransform.anchoredPosition;
        }

        private void Update() {
            if(canvas.renderMode == RenderMode.ScreenSpaceCamera){
                FollowMouseOnCameraSpace();
            }
            else if(canvas.renderMode == RenderMode.ScreenSpaceOverlay){
                FollowMouseOnOverlaySpace();
            }
        }
        #endregion

        private void FollowMouseOnOverlaySpace(){
            rectTransform.anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransform.localScale.x;
        }

        private void FollowMouseOnCameraSpace(){
            Vector2 canvasPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Mouse.current.position.ReadValue(), canvas.worldCamera , out canvasPoint);
            rectTransform.anchoredPosition = canvasPoint;
        }
    }
}
