public enum EnemyType
{
    Sunflower,
    TargetDummy,
    AlphaDummy
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
    Hedgehog
}
public enum AccessoryType
{
    Amulet,
    Clock
}
public enum StatModifier
{
    MoveSpeedModifier,
    WeaponSpeedModifier,
    DamageModifier,
    CooldownModifier,
    PickUpRadius
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