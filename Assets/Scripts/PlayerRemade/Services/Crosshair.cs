using Assets.Scripts.PlayerRemade.Contracts;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Manages the crosshair. Requires the Begin method to be run before working.
    /// </summary>
    public class Crosshair : MonoBehaviour, IObserver<Sprite> {
        #region Members
        public CameraWorks cameraWorks;
        SpriteRenderer _crosshairSprite;

        //public Vector3 mousePosition { get; private set; }
        public Vector3 CrosshairPosition { get; private set; }
        #endregion
        /// <summary>
        /// Gets the position of the mouse, detected by this script.
        /// </summary>
        //public Vector3 MousePosition {get; private set;}
        
        public void Begin(CameraWorks cWorks)
        {
            _crosshairSprite = GetComponent<SpriteRenderer>();
            cameraWorks = cWorks;
        }
        // Update is called once per frame
        public void Update ()
        {
            transform.position = GetWorldPosition(Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
            CrosshairPosition = transform.position;
        }
        public Vector3 GetWorldPosition(Vector3 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }
        
        public void SetCrosshair(Sprite newCrosshair)
        {
            _crosshairSprite.sprite = newCrosshair;
        }

        public void Notify(Sprite observedObject)
        {
            SetCrosshair(observedObject);
        }
    }
}
