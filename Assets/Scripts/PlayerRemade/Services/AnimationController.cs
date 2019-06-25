using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// A script that takes care of animation control, determining which and when shall be changed.
    /// Communicate with it through public methods.
    /// </summary>
    public class AnimationController : MonoBehaviour {
        Animator animatorComponent;               // The reference to the animation component. Found through the code.
        //      Communication with the scary, outside world         //
        private void Awake()
        {
            animatorComponent = GetComponent<Animator>();
        }
        void Start()
        {
            IsRunning = false;
            IsGrounded = true;
            VelocityY = 0.0f;
            IsJumping = false;
            IsDoubleJumping = false;
            IsHoldingWall = false;
        }

        /// <summary>
        /// Sets or gets the isRunning boolean of the animation.
        /// </summary>
        public bool IsRunning
        {
            set
            {
                animatorComponent.SetBool("isRunning", value);
            }
            get
            {
                return animatorComponent.GetBool("isRunning");
            }
        }
        /// <summary>
        /// Sets or gets the isGrounded boolean of the animation.
        /// </summary>
        public bool IsGrounded
        {
            set
            {
                animatorComponent.SetBool("isGrounded", value);
            }
            get
            {
                return animatorComponent.GetBool("isGrounded");
            }
        }
        /// <summary>
        /// Sets or gets the velocityY float value of the animation.
        /// </summary>
        public float VelocityY
        {
            set
            {
                animatorComponent.SetFloat("velocityY", value);
            }
            get
            {
                return animatorComponent.GetFloat("velocityY");
            }
        }
        /// <summary>
        /// Sets or gets the isJumping boolean of the animation.
        /// </summary>
        public bool IsJumping
        {
            set
            {
                animatorComponent.SetBool("isJumping", value);
            }
            get
            {
                return animatorComponent.GetBool("isJumping");
            }
        }
        /// <summary>
        /// Sets or gets the isDoubleJumping boolean of the animation.
        /// </summary>
        public bool IsDoubleJumping
        {
            set
            {
                animatorComponent.SetBool("isDoubleJumping", value);
            }
            get
            {
                return animatorComponent.GetBool("isDoubleJumping");
            }
        }
        /// <summary>
        /// Sets or gets the canDoubleJump boolean of the animation.
        /// </summary>
        public bool CanDoubleJump
        {
            set
            {
                animatorComponent.SetBool("canDoubleJump", value);
            }
            get
            {
                return animatorComponent.GetBool("canDoubleJump");
            }
        }
        /// <summary>
        /// Sets or gets the isHoldingWall boolean of the animation.
        /// </summary>
        public bool IsHoldingWall
        {
            get
            {
                return animatorComponent.GetBool("isHoldingWall");
            }
            set
            {
                animatorComponent.SetBool("isHoldingWall", value);
            }
        }
        /// <summary>
        /// Sets or gets the isClimbingUp boolean of the animation.
        /// </summary>
        public bool IsClimbingUp
        {
            get
            {
                return animatorComponent.GetBool("isClimbingUp");
            }
            set
            {
                animatorComponent.SetBool("isClimbingUp", value);
            }
        }
        /// <summary>
        /// Sets or gets the isClimbingDown boolean of the animation.
        /// </summary>
        public bool IsClimbingDown
        {
            get
            {
                return animatorComponent.GetBool("isClimbingDown");
            }
            set
            {
                animatorComponent.SetBool("isClimbingDown", value);
            }
        }
        /// <summary>
        /// Sets or gets the velocityX float parameter of the animation.
        /// </summary>
        public float NormalizeRunSpeed
        {
            get
            {
                return animatorComponent.GetFloat("NormalizeRunSpeed");
            }
            set
            {
                animatorComponent.SetFloat("NormalizeRunSpeed", value);
            }
        }
        /// <summary>
        /// Sets or gets the isRunningBackwards boolean.
        /// </summary>
        public bool IsRunningBackwards
        {
            get
            {
                return animatorComponent.GetBool("isRunningBackwards");
            }
            set
            {
                animatorComponent.SetBool("isRunningBackwards", value);
            }
        }

        // Update is called once per frame
        void Update () {
		
        }
    }
}
