using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    class ClimbDeterminator
    {
        int[] signs;

        bool isClimbingDown;
        bool isHoldingRightWall;
        bool isWallSlashed; //slashed or backslashed - is it leaning forward or backward, think of / and \ characters

        //the weights for the booleans above.
        const int climbWage = 1;
        const int wallHoldWage = 2;
        const int wallSlashedWage = 4;

        public ClimbDeterminator()
        {
            signs = new int[8];
            signs[0] = signs[2] = signs[7] = signs[5] = 1;
            signs[6] = signs[4] = signs[1] = signs[3] = -1;
        }
        /// <summary>
        /// Used to get sign for the x coordinate of velocity used to move the character when climbing slopes
        /// </summary>
        /// <param name="isClimbingDown">Is the character climbing down?</param>
        /// <param name="isHoldingRightWall">Is the character holding a wall to its right?</param>
        /// <param name="topRaycastHit">The hit coordinates of the top raycast (the most top that actually hit something)</param>
        /// <param name="bottomRaycastHit">The hit coordinates of the bottom raycast (the most bottom that actually hit something)</param>
        /// <returns>Sign for the x coordinate of climbing velocity.</returns>
        public int GetSignForWallClimb(bool isClimbingDown, bool isHoldingRightWall, Vector2 topRaycastHit, Vector2 bottomRaycastHit)
        {
            this.isClimbingDown = isClimbingDown;
            this.isHoldingRightWall = isHoldingRightWall;

            if (topRaycastHit.x - bottomRaycastHit.x < 0)
                isWallSlashed = false;
            else
                isWallSlashed = true;

            return signs[CalculateIndex()];
        }
        /// <summary>
        /// Calculates the index which will be used to determine what sign should be returned from the
        /// signs array, using wages and decisions saved in booleans of this class.
        /// </summary>
        /// <returns>Index of required sign.</returns>
        int CalculateIndex()
        {
            int index = 0;
            if (isClimbingDown)
                index += climbWage;
            if (isHoldingRightWall)
                index += wallHoldWage;
            if (isWallSlashed)
                index += wallSlashedWage;

            return index;
        }
    }
}
