public enum EnemyType
{
    Sunflower,
    TargetDummy,
    AlphaDummy
}
public enum CollectibleType
{
    Healing,
    Experience
}
public enum UpgradeCase
{
    UpgradeOrNew,
    JustUpgrade,
    JustNew,
    None
}
public enum ConditionType
{
    UnlockedByDefault,
    UnlockedWithWeaponKills,
    UnlockedWithEnemyKilled,
    UnlockedWithMaxRankOfWeapon,
    UnlockedWithMaxRankOfAccessory,
    UnlockedWithCollectedItems,
    UnlockedWithLevelCompletion
}
public enum WeaponType
{
    DefaultValue,
    Spear,
    Rock,
    Hedgehog,
    TempSpear1,
    TempSpear2,
    TempSpear3
}
public enum AccessoryType
{
    Amulet,
    Clock,
    Boot,
    Hook
}
public enum StatModifier
{
    MoveSpeedModifier,
    WeaponSpeedModifier,
    DamageModifier,
    CooldownModifier,
    LootingRadius,
    RegenerationModifier
}
public enum SectorOfChunk
{
    LeftBottomCorner,
    LeftMiddle,
    LeftTopCorner,
    RightBottomCorner,
    RightMiddle,
    RightTopCorner,
    MiddleTop,
    MiddleBottom
}
public enum OccupiedSide
{
    Top,
    Bottom,
    Left,
    Right,
}
public enum AudioClipType
{
    Sound,
    Music
}
public enum AudioClipID
{
    Music,
    EnemyHit,
    PlayerHit,
    ExpPickUp,
    HealthPickup,
    LevelUp,
    PlayerDeath,
    GameOver,
    GameWon

}
public enum MixerGroup
{
    Master,
    Sounds,
    Music
}
public enum SceneName
{
    Cemetery,
    Forest,
    MainMenu
}
public enum GameLevel
{
    Cemetery,
    Forest,
    Test,
    Test2,
    Test3
}
