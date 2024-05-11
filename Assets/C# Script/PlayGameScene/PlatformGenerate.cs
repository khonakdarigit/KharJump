using Assets.C__Script.PlayGameScene;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerate : MonoBehaviour
{
    public static PlatformGenerate instance;
    public GameObject platformPrefab;
    public GameObject BrackplatformPrefab;
    public GameObject LeftRightplatformPrefab;
    public GameObject ExplosionPlatform;
    public GameObject LeftRightExplosionPlatform;
    public GameObject JumpHidePlatform;
    public GameObject LeftRightJumpHidePlatform;

    public GameObject coil_Pefefb;
    public GameObject Rocker_Pefefb;
    public GameObject Poling_Pefefb;
    public GameObject OneEyeMonester;
    public GameObject FourEyeMonester;
    public GameObject BeeMonester;
    public GameObject BlackHole;

    public float CameraSize { get; set; }
    public float HalfCameraSize { get; set; }

    public enum PlatformType { Reqular_Platfrom, Simple_Platform, Green_Platform }

    public float Y_FaseleMantegiBeinePlatformHa;

    public int DedPlatformCounter;
    public GameLeveData gamelevel { get; set; }


    void Start()
    {
        gamelevel = new GameLeveData();
        DedPlatformCounter = 0;
        if (instance == null)
        {
            instance = this;
        }

        CameraSize = Camera.main.orthographicSize;

        HalfCameraSize = (Camera.main.orthographicSize * Camera.main.aspect) - 0.7f;

        Vector3 SpawnerPosition = new Vector3();

        for (int i = 1; i <= 4; i++)
        {
            SpawnerPosition.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
            SpawnerPosition.y = i * Y_FaseleMantegiBeinePlatformHa;
            // Reqular
            platformPrefab.name = PlatformType.Reqular_Platfrom.ToString();
            Instantiate(platformPrefab, SpawnerPosition, Quaternion.identity);
            // Random
            RandomPlatformForBlock(SpawnerPosition.y, gamelevel.CurrentLevel);
        }
    }

    public void DedPlatform(GameObject platform)
    {
        var Reqular_Platfrom = platform.name.Contains(PlatformType.Reqular_Platfrom.ToString());
        if (Reqular_Platfrom)
        {
            DedPlatformCounter++;

            // Level Option
            var Calculatelevel = (DedPlatformCounter / 10 < 1) ? 1 : DedPlatformCounter / 10;

            Calculatelevel = Calculatelevel > gamelevel.levelOptions.Last().LevelNumber ?
                Random.Range(1, gamelevel.levelOptions.Count) :
                Calculatelevel;

            if (gamelevel.CurrentLevel.LevelNumber != Calculatelevel)
            {
                gamelevel.CurrentLevel = gamelevel.levelOptions.FirstOrDefault(c => c.LevelNumber == Calculatelevel);

                Debug.Log(string.Format(
               "Calculatelevel : {0} LevelNumber : {1} simplePlatformInBlock : {2} GreenPlatformInBlock : {3}",
                Calculatelevel, gamelevel.CurrentLevel.LevelNumber, gamelevel.CurrentLevel.simplePlatformInBlock, gamelevel.CurrentLevel.GreenPlatformInBlock));
            }

            // Main Patform position

            float RandX = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
            float RandY =
                DedPlatformCounter * Y_FaseleMantegiBeinePlatformHa +
                4 * Y_FaseleMantegiBeinePlatformHa;

            if (gamelevel.CurrentLevel.MainPlatformType != null)
            {
                switch (gamelevel.CurrentLevel.MainPlatformType.Value)
                {
                    case MainPlatformType.SimplePlatform:
                        platform.transform.position = new Vector3(RandX, RandY, 0);
                        break;
                    case MainPlatformType.LeftRightPlatform:
                        LeftRightplatformPrefab.name = PlatformType.Reqular_Platfrom.ToString();
                        Instantiate(LeftRightplatformPrefab, new Vector3(RandX, RandY, 0), Quaternion.identity);
                        UnityEngine.Object.Destroy(platform.gameObject);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Reqular platform
                platform.transform.position = new Vector3(RandX, RandY, 0);
            }



            // Simple platform
            RandomPlatformForBlock(RandY, gamelevel.CurrentLevel);

        }
        else
        {
            UnityEngine.Object.Destroy(platform.gameObject);
        }





    }

    private void RandomPlatformForBlock(float LastInstantiatePlatformY, LevelOption levelOption)
    {
        float SizeBetweenPlatform = 0.5f;

        // implePlatformInBlock with level option
        for (int i = 0; i < levelOption.simplePlatformInBlock; i++)
        {
            Vector3 SpawnerPositionR = new Vector3();
            SpawnerPositionR.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
            LastInstantiatePlatformY = LastInstantiatePlatformY + SizeBetweenPlatform;
            SpawnerPositionR.y = LastInstantiatePlatformY;
            platformPrefab.name = PlatformType.Simple_Platform.ToString();
            Instantiate(platformPrefab, SpawnerPositionR, Quaternion.identity);
            // Persent % of platforms is With Coil
            if (Random.Range(1, 100) <= levelOption.CoilPersent & i <= 2)
            {
                AddCoilOnPlatform(SpawnerPositionR);
                // For Coil Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;
            }
            // Persent % of platforms is With Roket
            else if (Random.Range(1, 100) <= levelOption.RoketPersent & i <= 1)
            {
                AddRoketOnPlatform(SpawnerPositionR);
                // For Rocket Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
            // Persent % of platforms is With Poling
            else if (Random.Range(1, 100) <= levelOption.PolingPersent & i <= 1)
            {
                AddPolingOnPlatform(SpawnerPositionR);
                // For Poling Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
            // Persent % of platforms is With Poling
            else if (Random.Range(1, 100) <= levelOption.OneEyeMonestrPersent & i <= 1)
            {
                AddOneEyeMonesterOnPlatform(SpawnerPositionR);
                // For OneEyeMonester Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
            else if (Random.Range(1, 100) <= levelOption.FourEyeMonestrPersent & i <= 1)
            {
                AddOneFourMonesterOnPlatform(SpawnerPositionR);
                // For FourMonester Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
            else if (Random.Range(1, 100) <= levelOption.BeeMonster & i <= 1)
            {
                AddBeeMonesterOnPlatform(SpawnerPositionR);
                // For BeeMonester Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
            else if (Random.Range(1, 100) <= levelOption.BlackHole & i <= 1)
            {
                AddBlackHoleOnPlatform(SpawnerPositionR);
                // For BlackHole Size
                i += 3;
                LastInstantiatePlatformY *= SizeBetweenPlatform * 3;

            }
        }

        for (int i = 0; i < levelOption.BrackPlatformInBlock; i++)
        {
            LastInstantiatePlatformY = AddPlatForm(BrackplatformPrefab, LastInstantiatePlatformY, SizeBetweenPlatform);
        }
        for (int i = 0; i < levelOption.LeftRightPlatformInBLock; i++)
        {
            LeftRightplatformPrefab.name = PlatformType.Simple_Platform.ToString();

            LastInstantiatePlatformY = AddPlatForm(LeftRightplatformPrefab, LastInstantiatePlatformY, SizeBetweenPlatform);

        }
        for (int i = 0; i < levelOption.ExplosionPlatformInBlock; i++)
        {

            LastInstantiatePlatformY = AddPlatForm(ExplosionPlatform, LastInstantiatePlatformY, SizeBetweenPlatform);

        }
        for (int i = 0; i < levelOption.LeftRightExplosionPlatformInBlock; i++)
        {

            LastInstantiatePlatformY = AddPlatForm(LeftRightExplosionPlatform, LastInstantiatePlatformY, SizeBetweenPlatform);

        }
        for (int i = 0; i < levelOption.JumpHidePlatformInBLock; i++)
        {

            LastInstantiatePlatformY = AddPlatForm(JumpHidePlatform, LastInstantiatePlatformY, SizeBetweenPlatform);
        }
        for (int i = 0; i < levelOption.LeftRightJumpHidePlatformInBLock; i++)
        {

            LastInstantiatePlatformY = AddPlatForm(LeftRightJumpHidePlatform, LastInstantiatePlatformY, SizeBetweenPlatform);
        }
    }

    private float AddPlatForm(GameObject platform, float LastInstantiatePlatformY, float SizeBetweenPlatform)
    {
        Vector3 SpawnerPositionR = new Vector3();
        SpawnerPositionR.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
        LastInstantiatePlatformY = LastInstantiatePlatformY + SizeBetweenPlatform;
        SpawnerPositionR.y = LastInstantiatePlatformY;
        Instantiate(platform, SpawnerPositionR, Quaternion.identity);
        return LastInstantiatePlatformY;
    }

    private void AddRoketOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = SpawnerPositionR.x;
        SpawnerPosition.y = SpawnerPositionR.y + 0.7f;
        Instantiate(Rocker_Pefefb, SpawnerPosition, Quaternion.identity);
    }
    private void AddOneEyeMonesterOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
        SpawnerPosition.y = SpawnerPositionR.y + 0.7f;
        Instantiate(OneEyeMonester, SpawnerPosition, Quaternion.identity);
    }
    private void AddOneFourMonesterOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
        SpawnerPosition.y = SpawnerPositionR.y + 0.7f;
        Instantiate(FourEyeMonester, SpawnerPosition, Quaternion.identity);
    }
    private void AddBeeMonesterOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
        SpawnerPosition.y = SpawnerPositionR.y + 0.7f;
        Instantiate(BeeMonester, SpawnerPosition, Quaternion.identity);
    }

    private void AddBlackHoleOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = Random.Range(-1 * HalfCameraSize, HalfCameraSize);
        SpawnerPosition.y = SpawnerPositionR.y + 1.2f;
        Instantiate(BlackHole, SpawnerPosition, Quaternion.identity);
    }
    private void AddCoilOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = Random.Range(SpawnerPositionR.x - 0.3f, SpawnerPositionR.x + 0.3f);
        SpawnerPosition.y = SpawnerPositionR.y + 0.3f;
        Instantiate(coil_Pefefb, SpawnerPosition, Quaternion.identity);
    }
    private void AddPolingOnPlatform(Vector3 SpawnerPositionR)
    {
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.x = SpawnerPositionR.x;
        SpawnerPosition.y = SpawnerPositionR.y + 0.28f;
        Instantiate(Poling_Pefefb, SpawnerPosition, Quaternion.identity);
    }

}
