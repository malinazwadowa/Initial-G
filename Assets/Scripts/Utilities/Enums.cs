public enum EnemyType
{
    Sunflower,
    TargetDummy,
    AlphaDummy
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
    UnlockedWithMaxRankOfAccessory
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
    LootingRadius
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
public enum SceneName
{
    Forest,
    Cementary,
    Loading,
    MainMenu
}
public enum OccupiedSide
{
    Top,
    Bottom,
    Left,
    Right,
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
    GameOver

}
public enum MixerGroup
{
    Master,
    Sounds,
    Music
}
public enum GameLevel
{
    Forest,
    Cementary,
    dupa
}