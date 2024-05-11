using Assets.C__Script.PlayGameScene;
using System.Collections.Generic;
using System.Linq;

public class GameLeveData
{
    public GameLeveData()
    {
        levelOptions = new List<LevelOption>() {
            new LevelOption() { LevelNumber = 1 , simplePlatformInBlock = 5},
            new LevelOption() { LevelNumber = 2 , simplePlatformInBlock = 5 , CoilPersent = 30},
            new LevelOption() { LevelNumber = 3 , simplePlatformInBlock = 4 , CoilPersent = 30},
            new LevelOption() { LevelNumber = 4 , simplePlatformInBlock = 4 , CoilPersent = 20 , PolingPersent = 30},
            new LevelOption() { LevelNumber = 5 , simplePlatformInBlock = 4 , CoilPersent = 20 , PolingPersent = 30},
            new LevelOption() { LevelNumber = 6 , simplePlatformInBlock = 1 , BrackPlatformInBlock = 4 , CoilPersent = 20 , PolingPersent = 30},
            new LevelOption() { LevelNumber = 7 , simplePlatformInBlock = 1 , BrackPlatformInBlock = 4 , CoilPersent = 20 , PolingPersent = 20},
            new LevelOption() { LevelNumber = 8 , simplePlatformInBlock = 0 , BrackPlatformInBlock = 5 , CoilPersent = 10 , PolingPersent = 20},
            new LevelOption() { LevelNumber = 9 , LeftRightPlatformInBLock = 3 , PolingPersent = 20},
            new LevelOption() { LevelNumber = 10 , LeftRightPlatformInBLock = 3 , BrackPlatformInBlock = 2 , CoilPersent = 60 , RoketPersent = 40 },
            new LevelOption() { LevelNumber = 11 , LeftRightPlatformInBLock = 3 , BrackPlatformInBlock = 2 , CoilPersent = 60 , RoketPersent = 40  },
            new LevelOption() { LevelNumber = 12 , simplePlatformInBlock = 1 , JumpHidePlatformInBLock = 4 , CoilPersent = 100 },
            new LevelOption() { LevelNumber = 13 , LeftRightJumpHidePlatformInBLock = 2},
            new LevelOption() { LevelNumber = 14 , LeftRightJumpHidePlatformInBLock = 1},
            new LevelOption() { LevelNumber = 15 , MainPlatformType = MainPlatformType.LeftRightPlatform , LeftRightPlatformInBLock = 4},
            new LevelOption() { LevelNumber = 16 , MainPlatformType = MainPlatformType.LeftRightPlatform , LeftRightPlatformInBLock = 2},
            new LevelOption() { LevelNumber = 17 , simplePlatformInBlock = 3 , CoilPersent = 40},
            new LevelOption() { LevelNumber = 18 , simplePlatformInBlock = 3 , CoilPersent = 40},
            new LevelOption() { LevelNumber = 19 , simplePlatformInBlock = 3 , CoilPersent = 10 , BlackHole = 10 },
            new LevelOption() { LevelNumber = 20 , MainPlatformType = MainPlatformType.SimplePlatform , BrackPlatformInBlock = 1 , ExplosionPlatformInBlock = 2  },
            new LevelOption() { LevelNumber = 21 , MainPlatformType = MainPlatformType.SimplePlatform , LeftRightJumpHidePlatformInBLock = 2 , ExplosionPlatformInBlock = 2  },
            new LevelOption() { LevelNumber = 22 , MainPlatformType = MainPlatformType.SimplePlatform , ExplosionPlatformInBlock = 2 , LeftRightExplosionPlatformInBlock = 2  },
            new LevelOption() { LevelNumber = 23 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 3 , PolingPersent = 10  },
            new LevelOption() { LevelNumber = 24 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 3 , BeeMonster = 10 },
            new LevelOption() { LevelNumber = 25 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 3 , FourEyeMonestrPersent = 10 },
            new LevelOption() { LevelNumber = 26 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 3 , RoketPersent = 10 },
            new LevelOption() { LevelNumber = 27 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 2 , LeftRightPlatformInBLock = 2 , RoketPersent = 10 },
            new LevelOption() { LevelNumber = 28 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 1 , FourEyeMonestrPersent = 10 },
            new LevelOption() { LevelNumber = 29 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 0 , OneEyeMonestrPersent = 15 },
            new LevelOption() { LevelNumber = 30 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 0 , BeeMonster = 5 , FourEyeMonestrPersent = 0 , OneEyeMonestrPersent = 5 },
            new LevelOption() { LevelNumber = 31 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 0 , BeeMonster = 5 , FourEyeMonestrPersent = 0 , OneEyeMonestrPersent = 5 },
            new LevelOption() { LevelNumber = 32 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 0 , BeeMonster = 5 , FourEyeMonestrPersent = 0 , OneEyeMonestrPersent = 5 },
            new LevelOption() { LevelNumber = 33 , MainPlatformType = MainPlatformType.SimplePlatform , simplePlatformInBlock = 1  , LeftRightPlatformInBLock = 0 , BeeMonster = 5 , FourEyeMonestrPersent = 0 , OneEyeMonestrPersent = 5 },


        };
        CurrentLevel = levelOptions.First();
    }

    public LevelOption CurrentLevel { get; set; }
    public List<LevelOption> levelOptions { get; set; }
}