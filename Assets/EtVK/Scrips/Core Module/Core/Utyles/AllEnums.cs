// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using System;

namespace EtVK.Core.Utyles
{


    // for attacks type
    public enum AttackType
    {
        NormalAttack,
        DashAttack,
        AirAttack,
    }



    // For Enemy AI
    public enum EnemyAIState
    {
        AgentState,
        IdleState = 1,
        CombatState = 2,
        DummyMode = 3,
        LookForTarget = 5,
        VoidState = 100,

    }

    public enum ModularOptions
    {
        Head = 0,
        Hair = 1,
        Eyebrows = 2,
        FacialHair = 3,
        Helmet = 4,
        Shoulder = 5,
        Back = 6,
        UpperArm = 7,
        LowerArm = 8,
        Hand = 9,
        Chest = 10,
        Hips = 11,
        Legs = 12,
        HelmetAttachment = 13,
        HeadAttachment = 14,
        All = 15,

    }

    public enum ModularColorOptions
    {
        Primary = 0,
        Secondary = 1,
        Leather_Primary = 2,
        Leather_Secondary = 3,
        Metal_Primary = 4,
        Metal_Secondary = 5,
        Metal_Dark = 6,
        Hair = 7,
        Skin = 8,
        Stubble = 9,
        Scar = 10,
        Eyes = 11,
        BodyArt = 12,
    }

    public enum EnemyAIAction
    {
        IsPatrolling,
        IsRotating,
        Angle,
        Rotation,
        IsAttacking,
        CombatVertical,
        CombatHorizontal,
        DodgeX,
        DodgeY,
        IsPhaseShifting,
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
        Dash,
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

    public enum StaminaCostType
    {
        Sprint,
        Dodge,
        Jump,
    }


    // For player controller
    public enum PlayerState
    {
        Movement = 0,
        LockOnMovementX = 2,
        LockOnMovementY = 3,
        Jump = 4,
        CombatState = 5,
        Running = 6,
        IsAttacking = 7,
        InDodge = 8,
        Transition = 9,
        TransitionLayer = 1,
        IsAiming = 10,
        MouseYAxis = 11,
        ComboAttack = 12,
        IsLockedOn = 13,
    }

    public enum ItemType
    {
        Weapon = 0,
        Armor = 1,
        Colectable = 2,
        OffHand = 3,
    }

    public enum WeaponType
    {
        Unequipped = 0,
        Sword = 1,
        GreatSword = 2,
        Bow = 3,
        Shield = 4,
        Arrows = 9,
        FistRightHand = 20,
        FistLeftHand = 21,
        BootRightFeet = 22,
        BootLeftFeet = 23,

    }
    
    public enum WeaponActionType
    {
        Draw = 0,
        Withdraw = 1,
        Block,
        Aim,
    }

    public enum OffHandType
    {
        Shield = 0,
        Potion = 1,
        Torch = 2,
    }

    public enum ArmorType
    {
        Unequiped = 0,
        Helmet = 1,
        Shoulders = 2,
        UpperBracers = 3,
        LowerBracers = 4,
        Gloves = 5,
        Chestplate = 6,
        Back = 7,
        Pants = 8,
        Boots = 9,

    }



    public enum StatusResponse
    {
        Success = 0,
        Fail = 1,
    }

    public enum ActiveCameraType
    {
        Main = 0,
        Aim = 1,
        LockOn = 2,
    }

    public enum SceneNames
    {
        GameUi = 4, 
        Player = 1,
        CharacterCreation = 2,
        LevelOne = 3,
    }

    public enum GameLevel
    {
        None = 0,
        One = 1,
    }

    public enum GameUi
    {
        Empty = 0,
        MainMenu = 1,
        GameMenu = 2,
        Inventory = 2,
        Shop,
        Hud,
    }
    

    public enum AnimatorLayer
    {
        Base = 0,
        RightArm = 1,
        LeftArm = 2,
        BothArms = 3,
        UpperBody = 4,
    }
    
    public enum AnimatorCommonFileds
    {
        AbilityFinished,
        IsAimAction = 14,
    }

    public class AllEnums
    {

    }
}