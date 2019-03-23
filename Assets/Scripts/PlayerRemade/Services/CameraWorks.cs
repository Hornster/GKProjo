using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.PlayerRemade.Services
{
    public class CameraWorks : MonoBehaviour {
        public Camera camera;
        readonly Vector2 defaulOffset = Vector2.zero;
        Vector2 cameraToWorldRatio;
        public void Start()
        {
            camera = Camera.main.GetComponent<Camera>();
        }
        /// <summary>
        /// Moves camera around, providing no offset.
        /// </summary>
        /// <param name="newPosition">New position for the camera.</param>
        public void MoveCamera(Vector3 newPosition)
        {
            MoveCamera(newPosition, defaulOffset);
        }
        /// <summary>
        /// Moves the camera around.
        /// </summary>
        /// <param name="newPosition">New position for the camera.</param>
        /// <param name="offset">Offset for the camera position.</param>
        public void MoveCamera(Vector3 newPosition, Vector2 offset)
        {
            camera.transform.position = new Vector3(offset.x + newPosition.x, newPosition.y + offset.y, camera.transform.position.z);
        }
        /// <summary>
        /// Translates coordinates of point on the screen to point in the world
        /// </summary>
        /// <param name="position">Position on screen.</param>
        /// <returns></returns>
        public Vector3 GetWorldPosition(Vector3 position)
        {
            return camera.ScreenToWorldPoint(position);
        }
        public Vector2 GetViewsize()
        {
            return new Vector2(camera.pixelWidth, camera.pixelHeight);
        }
    }
}
