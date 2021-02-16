using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExSharpBase.Cheat;
using ExSharpBase.Game;
using ExSharpBase.Modules;
using ExSharpBase.Overlay.Drawing;
using SharpDX;

namespace ExSharpBase.Events
{
    class Drawing
    {
        public static Dictionary<string, bool> DrawingProperties = new Dictionary<string, bool>()
        {
            { "DrawPlayers", true },
            { "Draw2dBox", true },
            { "Draw3dBox", false },
            { "DrawSkelton", true },
            { "DrawCrosshair", true },
            { "Aimbot", true },
            { "DrawFOV", true },
            { "MagicBullet", true },
            { "DrawLoot", false },
            { "DrawVehicle", false }
        };
        public static Color SkeltonColor = Color.White;
        public static Color BoxColor = Color.Red;
        public static float FOV = 100;
        public static bool IsMenuBeingDrawn = false;
        public static Vector2 ScreenSize = new Vector2(Program.DrawBase.Width, Program.DrawBase.Height);
        public static void OnDeviceDraw()
        {

            if (Utils.IsGameOnDisplay())
            {
                //When ~ key is pressed...
                DrawMenu();
                Aimbot.BeginFrame();
                List<List<AActor>> actors = Main.actors;
                if (actors == null)
                    return;
                if (DrawingProperties["DrawPlayers"])
                {
                    for (int i = 0; i < actors[0].Count; i++)
                    {
                        AActor actor = actors[0][i];
                        if (actor.TeamID != Main.localPawn.TeamID && actor.PlayerName.ToString() != Main.localPawn.PlayerName.ToString() && Main.localPawn.IsPlayer)
                        {
                            Aimbot.EvaluateTarget(actor);
                            if (DrawingProperties["DrawSkelton"])
                            {
                                ESP.DrawSkelton(actor, ScreenSize, SkeltonColor);
                            }
                            if (DrawingProperties["Draw2dBox"])
                            {
                                ESP.DrawBox(actors[0][i], ScreenSize, BoxColor);
                            }
                            if (DrawingProperties["Draw3dBox"])
                            {
                                ESP.DrawBox3d(actors[0][i], ScreenSize, BoxColor);
                            }
                            ESP.DrawPlayerDetils(actors[0][i], ScreenSize, BoxColor);

                        }
                    }
                }
                if(DrawingProperties["DrawCrosshair"])
                {
                    int crosshairtype = 0;
                    float CenterX = ScreenSize.X / 2;
                    float CenterY = ScreenSize.Y / 2;
                    switch (crosshairtype)
                    {
                        case 0:
                            
                            DrawFactory.DrawFilledBox(CenterX - 20, CenterY, 40, 1, Color.Purple);//Purple
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 20, 1, 40, Color.Purple);
                            DrawFactory.DrawFilledBox(CenterX - 17, CenterY, 34, 1, Color.Blue);//Blue
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 17, 1, 34, Color.Blue);
                            DrawFactory.DrawFilledBox(CenterX - 14, CenterY, 28, 1, Color.Cyan);//Cyan
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 14, 1, 28, Color.Cyan);
                            DrawFactory.DrawFilledBox(CenterX - 11, CenterY, 22, 1, Color.Green);//Green
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 11, 1, 22, Color.Green);
                            DrawFactory.DrawFilledBox(CenterX - 9, CenterY, 18, 1, Color.Yellow);//Yellow
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 9, 1, 18, Color.Yellow);
                            DrawFactory.DrawFilledBox(CenterX - 6, CenterY, 12, 1, Color.Orange);//Orange
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 6, 1, 12, Color.Orange);
                            DrawFactory.DrawFilledBox(CenterX - 3, CenterY, 6, 1, Color.Red);//Red
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 3, 1, 6, Color.Red);
                            break;
                        case 1:
                            DrawFactory.DrawCircle((int)CenterX, (int)CenterY, 8, 50, Color.Red);//Circle
                            DrawFactory.DrawFilledBox(CenterX - 17, CenterY, 10, 1, Color.Red);//Left line
                            DrawFactory.DrawFilledBox(CenterX + 9, CenterY, 10, 1, Color.Red); // Right line
                            DrawFactory.DrawFilledBox(CenterX, CenterY - 17, 1, 10, Color.Red);//Top line
                            DrawFactory.DrawFilledBox(CenterX, CenterY + 9, 1, 10, Color.Red);//Bottom line
                            DrawFactory.DrawPoint(CenterX, CenterY, Color. Green);//Dot point
                            break;
                    }
                    
                }
                if (DrawingProperties["DrawFOV"])
                {
                    float CenterX = ScreenSize.X / 2;
                    float CenterY = ScreenSize.Y / 2;
                    DrawFactory.DrawCircle((int)CenterX, (int)CenterY, FOV, 50, Color.Black);
                }
                if (DrawingProperties["DrawLoot"])
                {
                    for (int i = 0; i < actors[1].Count; i++)
                    {
                        AActor actor = actors[1][i];

                        ESP.DrawItem(actor, ScreenSize);
                    }
                }
                if (DrawingProperties["DrawVehicle"])
                {
                    for (int i = 0; i < actors[2].Count; i++)
                    {
                        AActor actor = actors[2][i];
                        ESP.DrawVehicle(actor, ScreenSize);
                    }
                }
                if (DrawingProperties["Aimbot"] || DrawingProperties["MagicBullet"])
                {
                    Vector2 feetScreenLocation = Main.cameraManager.WorldToScreen(Aimbot.bestActor.GetBoneLocation(0), ScreenSize);
                    Vector3 Headd = Aimbot.bestActor.GetBoneLocation(6);
                    Headd.Z += 30;
                    Vector2 headScreenLocation = Main.cameraManager.WorldToScreen(Headd, ScreenSize);
                    float xHeight = feetScreenLocation.Y - headScreenLocation.Y;
                    float xWidth = xHeight / 2.5f;
                    Vector2 headw2s = Main.cameraManager.WorldToScreen(Aimbot.bestActor.GetBoneLocation(6), ScreenSize);
                    DrawFactory.DrawCircle((int)headw2s.X, (int)headw2s.Y, xWidth / 6, 50, SkeltonColor);
                    if (Utils.IsKeyPressed(Keys.RButton) || Utils.IsKeyPressed(Keys.LButton))
                        Aimbot.AimToTarget();
                }




            }
        }

        private static void DrawMenu()
        {
            if (Utils.IsKeyPressed(System.Windows.Forms.Keys.Insert) )
            {
                IsMenuBeingDrawn = true;
                Program.MenuBasePlate.Show();
               
              
            }
            else
            {
                IsMenuBeingDrawn = false;
                Program.MenuBasePlate.Hide();
            }
        }
    }
}
