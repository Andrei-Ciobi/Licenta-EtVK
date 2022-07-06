namespace EtVK.Scrips.Core_Module
{

    // For attacks
    public enum EnemyBaseAnimation
    {
        BaseAttack,
        BaseAttackCombo,

    }

    // for attacks type
    public enum AttackType
    {
        StartAttack,
        ComboAttack,
        FinisherAttack,
    }

    // For Enemy AI
    public enum EnemyAIState
    {
        AgentState,
        IdleState = 1,
        CombatState = 2,
        DummyState,
        LookForTarget = 5,
        VoidState = 100,

    }

    public enum EnemyAIAction
    {
        IsPatrolling,
        TakeDamage,
        IsRotating,
        Angle,
        Rotation,
        IsAttacking,
        ComboAttack,
        CombatVertical,
        CombatHorizontal,
        IsPhaseShifting,
        IsAttackRotate,
        IsActive,
        StartEncounter,

    }

    public enum BossPhase
    {
        PhaseOne = 1,
        PhaseTwo = 2,
        PhaseThree = 3,
        PhaseFour = 4,
        PhaseFive = 5,
        PhaseSix = 6,
    }

    // For ability types
    public enum AbilityType
    {
        GroundSmash,
    }

    public enum RotateAround
    {
        CurentTarget,
        CurentWaypoint
    }

    public enum Factions
    {
        NoFaction,
        Nathrezeem,
        Balkan,
    }


    // For player controller
    public enum PlayerState
    {
        Movement = 0,
        CombatMovementX = 2,
        CombatMovementY = 3,
        Jump = 4,
        CombatState = 5,
        Running = 6,
        IsAttacking = 7,
        WeaponState = 8,
        Transition = 9,
        TransitionLayer = 1,
        IsAiming = 10,
        MouseYAxis = 11,
    }

    public enum ItemType
    {
        Weapon = 0,
        Armor = 1,
        Colectable = 2,
    }

    public enum WeaponType
    {
        Unequiped = 0,
        Sword = 1,
        Shield = 3,
        Bow = 2,
        Arrows = 9,
        FistRightHand = 20,
        FistLeftHand = 21,
        BootRightFeet = 22,
        BootLeftFeet = 23,

    }

    public enum ArmorType
    {
        Unequiped = 0,
        Helmet = 1,
        Chestplate = 2,
        Boots = 3,
    }

    public class AllEnums
    {

    }
}