using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Characters
{
    public class RuyoInputReceiver
    {
        #region Members

        private static RuyoInputReceiver Instance { get; set; }
        /// <summary>
        /// Retrieves the instance of this singleton.
        /// </summary>
        public static RuyoInputReceiver GetInstance()
        {
            if (Instance != null)
                return Instance;
            else
            {
                Instance = new RuyoInputReceiver();
                return Instance;
            }
        }

        public static KeyCode JumpKey { get; private set; }
        public static KeyCode ClimbKey { get; private set; }

        #endregion
        #region Ctors

        private RuyoInputReceiver()
        {
            JumpKey = KeyCode.Space;
            ClimbKey = KeyCode.LeftShift;
        }
#endregion
        #region Functionalities
        /// <summary>
        /// Loads the X and Y input from the user and stores it. Call it
        /// before retrieving any more info.
        /// </summary>
        public Vector2 RetrieveInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        /// <summary>
        /// Checks if the button has been pressed once.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
        /// <summary>
        /// Checks if given button is pressed, all the time. Use defined in this class values.
        /// </summary>
        /// <param name="keyCode">Code of the button to check.</param>
        /// <returns>TRUE if button is pressed.</returns>
        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }
        #endregion

    }
}
