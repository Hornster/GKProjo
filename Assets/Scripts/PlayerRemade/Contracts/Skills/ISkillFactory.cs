using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    public interface ISkillFactory
    {
        ISkill CreateSkill(SkillType skillID, ref IGetTransform shotSpawner, ref IGetTransform skillOwnerPlayer);
    }
}

