namespace Assets.Scripts.PlayerRemade.Model
{
    public struct MovementInfo
    {
        public bool canDoubleJump;

        public bool climbKeyOn;
        public bool climbUp, climbDown;

        public bool isClimbing;
        //used when raycasting in order to tell in which direction should the raycast go
        public bool holdsLeftWall;

        //calculated in the same way as the gravity in Player.cs. Stores the length of the raycast if
        //the object is climbing a wall
        public float projectionLength;
        /// <summary>
        /// Sets the isClimbing value accordingly to argument value.
        /// </summary>
        /// <param name="climbKeyOn">Is the climbing key pressed?</param>
        public void SetIsClimbing(bool climbKeyOn)
        {
            isClimbing = climbKeyOn;
        }
        public void SetProjectionLength(float length)
        {
            projectionLength = length;
        }
        public void HoldLeftWall(float directionX)
        {
            holdsLeftWall = directionX >=0? false: true;
        }
        public void ResetDoubleJump(CollisionInfo collisions)
        {
            if(collisions.below || collisions.left || collisions.right)
                canDoubleJump = true;
        }
        
    }
}