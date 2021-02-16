using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExSharpBase.Modules;
using System.Windows.Forms;
using System.Drawing;
using static ExSharpBase.Program;
using ExSharpBase.Game;
namespace ExSharpBase
{
    class Main
    {
        public static APlayerController playerController;
        public static ULocalPlayer LocalPlayer;
        public static APlayerCameraManager cameraManager;
        public static AActor localPawn;
        public static UWorld uWorld;
        public static List<List<AActor>> actors;
        public static void OnMain()
        {
            if (Utils.IsGameOnDisplay())
            {
                int ss = Engine.oUWorld;
                int uWorlds = Read<int>(Engine.oUWorld);
                uWorld = Driver.Read<UWorld>(uWorlds);
                LocalPlayer = uWorld.OwningGameInstance().LocalPlayer();
                playerController = LocalPlayer.PlayerController();
                cameraManager = playerController.PlayerCameraManager();
                localPawn = playerController.AknowledgedPawn();
                actors = uWorld.CurrentLevel().AActors();

            }
        }
        public static T Read<T>(long targetAddress) where T : struct
        {
            return Driver.Read<T>(targetAddress);
        }
    }
}
