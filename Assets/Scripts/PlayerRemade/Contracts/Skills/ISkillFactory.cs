using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public interface ISkillFactory
    {
        ISkill CreateSkill(SkillType skillID, ref IGetTransform shotSpawner, ref IGetTransform skillOwnerPlayer);
    }
}

