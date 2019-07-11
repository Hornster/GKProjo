using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

public class EnemyWallBasicAttackData : MonoBehaviour, IProjectile
{
    [field: SerializeField]
    public float Damage { get; set; }
    [field: SerializeField]
    public Teams Alignment { get; set; }
    [field: SerializeField]
    public bool HasItsOwnLifeTimer { get; set; }
    [field: SerializeField]
    public bool IsTimed { get; set; }
    [field: SerializeField]
    public float SkillDuration { get; set; }
    [field: SerializeField]
    public bool CanPenetrate { get; set; }
    public IDebuff AssignedDebuff { get; set; }
}
