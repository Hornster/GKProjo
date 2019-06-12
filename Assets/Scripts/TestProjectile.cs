using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

public class TestProjectile : MonoBehaviour, IProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        Damage = 34.0f;
        Alignment = Teams.Enemy;
        HasItsOwnLifeTimer = false;
        IsTimed = false;
        SkillDuration = 0.0f;
        CanPenetrate = false;
        AssignedDebuff = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Damage { get; private set; }
    public Teams Alignment { get; private set; }
    public bool HasItsOwnLifeTimer { get; private set; }
    public bool IsTimed { get; private set; }
    public float SkillDuration { get; private set; }
    public bool CanPenetrate { get; private set; }
    public IDebuff AssignedDebuff { get; private set; }
}
