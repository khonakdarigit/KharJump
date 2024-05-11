using Assets.C__Script.PlayGameScene;

public class LevelOption
{
    public int LevelNumber { get; set; }
    public MainPlatformType? MainPlatformType { get; set; }
    public int GreenPlatformInBlock { get; set; }
    public int simplePlatformInBlock { get; set; }
    public int BrackPlatformInBlock { get; set; }
    public int ExplosionPlatformInBlock { get; set; }
    public int LeftRightExplosionPlatformInBlock { get; set; }
    public int LeftRightPlatformInBLock { get; set; }
    public int JumpHidePlatformInBLock { get; set; }
    public int LeftRightJumpHidePlatformInBLock { get; set; }

    public int CoilPersent { get; internal set; }
    public int RoketPersent { get; internal set; }
    public int PolingPersent { get; internal set; }
    public int OneEyeMonestrPersent { get; internal set; }
    public int FourEyeMonestrPersent { get; internal set; }
    public int BeeMonster { get; internal set; }
    public int BlackHole { get; internal set; }



}