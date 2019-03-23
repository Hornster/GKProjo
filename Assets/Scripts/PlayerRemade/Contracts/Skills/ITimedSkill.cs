namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    /// <summary>
    /// Used for skills that have set lifetime after which they stop working, for example
    /// AoE effect.
    /// </summary>
    public interface ITimedSkill
    {
        float SkillLifeTime { get; }
    }
}
