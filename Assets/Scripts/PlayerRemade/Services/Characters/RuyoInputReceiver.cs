using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerRemade.Services.Characters
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// All KeyCodes are initialized upon calling GetInstance() method.
    /// Thank C#4 xD
    /// </summary>
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
        public static KeyCode FirstSkillKey { get; private set; }
        public static KeyCode SecondSkillKey { get; private set; }
        public static KeyCode ThirdSkillKey { get; private set; }
        public static KeyCode LeftMouseButton { get; private set; }

        #endregion
        #region Ctors

        private RuyoInputReceiver()
        {
            JumpKey = KeyCode.Space;
            ClimbKey = KeyCode.LeftShift;
            FirstSkillKey = KeyCode.Alpha1;
            SecondSkillKey = KeyCode.Alpha2;
            ThirdSkillKey = KeyCode.Alpha3;

            LeftMouseButton = KeyCode.Mouse0;
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
