using ExSharpBase.Game;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExSharpBase.Overlay.Drawing;
namespace ExSharpBase.Cheat
{
    class ESP
    {
        private static Vector2[][] GetBonesLocations(AActor player, Vector2 screenSize)
        {
            //spin 1 2 3 4 5 6
            //left arm 11 12 13 14 
            //right arm 32 33 34 35
            //left leg 53 54 55
            //right leg 57 58 59
            List<Vector2[]> bones = new List<Vector2[]>();
            Vector2[] bone1 = new Vector2[] { WorldToScreen(player.GetBoneLocation(1), screenSize), WorldToScreen(player.GetBoneLocation(2), screenSize), WorldToScreen(player.GetBoneLocation(3), screenSize), WorldToScreen(player.GetBoneLocation(4), screenSize), WorldToScreen(player.GetBoneLocation(5), screenSize), WorldToScreen(player.GetBoneLocation(6), screenSize) };
            Vector2[] bone2 = new Vector2[] { WorldToScreen(player.GetBoneLocation(11), screenSize), WorldToScreen(player.GetBoneLocation(12), screenSize), WorldToScreen(player.GetBoneLocation(13), screenSize), WorldToScreen(player.GetBoneLocation(14), screenSize) };
            Vector2[] bone3 = new Vector2[] { WorldToScreen(player.GetBoneLocation(32), screenSize), WorldToScreen(player.GetBoneLocation(33), screenSize), WorldToScreen(player.GetBoneLocation(34), screenSize), WorldToScreen(player.GetBoneLocation(35), screenSize) };
            Vector2[] bone4 = new Vector2[] { WorldToScreen(player.GetBoneLocation(53), screenSize), WorldToScreen(player.GetBoneLocation(54), screenSize), WorldToScreen(player.GetBoneLocation(55), screenSize) };
            Vector2[] bone5 = new Vector2[] { WorldToScreen(player.GetBoneLocation(57), screenSize), WorldToScreen(player.GetBoneLocation(58), screenSize), WorldToScreen(player.GetBoneLocation(59), screenSize) };
            Vector2[] bone6 = new Vector2[] { WorldToScreen(player.GetBoneLocation(57), screenSize), WorldToScreen(player.GetBoneLocation(1), screenSize), WorldToScreen(player.GetBoneLocation(53), screenSize) };


            bones.Add(bone1);
            bones.Add(bone2);
            bones.Add(bone3);
            bones.Add(bone4);
            bones.Add(bone5);
            bones.Add(bone6);
            return bones.ToArray();
        }
        public static void DrawSkelton(AActor player, Vector2 screenSize, Color color)
        {
           
            Vector2[][] bones = GetBonesLocations(player, screenSize);

            Vector2 prev, current;
            for (int ii = 0; ii < bones.Length; ii++)
            {
                prev = new Vector2(0, 0);
                current = new Vector2(0, 0);
                for (int j = 0; j < bones[ii].Length; j++)
                {
                    if (prev.X == 0 && prev.Y == 0)
                    {
                        prev = bones[ii][j];
                        continue;
                    }
                    current = bones[ii][j];
                    DrawFactory.DrawLine(prev.X, prev.Y, current.X, current.Y, 2, color);
                    prev = current;
                }
            }
        }
        public static void DrawBox(AActor player, Vector2 screenSize, Color color)
        {
          
            Vector2 feetScreenLocation = WorldToScreen(player.GetBoneLocation(0), screenSize);
            Vector3 Headd = player.GetBoneLocation(6);
            Headd.Z += 30;
            Vector2 headScreenLocation = WorldToScreen(Headd, screenSize);
            float xHeight = feetScreenLocation.Y - headScreenLocation.Y;
            float xWidth = xHeight / 2.5f;
            Vector2 xx = new Vector2(headScreenLocation.X - xWidth / 2, headScreenLocation.Y);
            DrawFactory.DrawBox(xx.X, xx.Y, xWidth, xHeight, color);
        }
        public static void DrawPlayerDetils(AActor player, Vector2 screenSize, Color color)
        {
            Vector3 vecLocalPlayer = Main.cameraManager.CameraCache.POV.Location;
            Vector3 Headd = player.GetBoneLocation(6);
            Headd.Z += 30;
            Vector2 Headw2s = WorldToScreen(Headd, screenSize);
            Vector2 Center = WorldToScreen(player.GetBoneLocation(1), screenSize);
            Vector2 Root = WorldToScreen(player.GetBoneLocation(0), screenSize);
            float curDist = (Headd - vecLocalPlayer).Length();
            float flWidth = Math.Abs((Root.Y - Headw2s.Y) / 4);
            Vector2 LeftUpperCorner = new Vector2((Center.X - flWidth), Headw2s.Y);
            Vector2 LeftDownCorner = new Vector2((Center.X - flWidth), Root.Y);
            Vector2 RightUpperCorner = new Vector2((Center.X + flWidth), Headw2s.Y);
            Vector2 RightDownCorner = new Vector2((Center.X + flWidth), Root.Y);
            float Height = Vector2.Distance( RightDownCorner,RightUpperCorner) ;
            int fontSize = ((int)Height) / 20;


            //Modules.LogService.Log(fontSize.ToString());





            Vector2 feetScreenLocation = WorldToScreen(player.GetBoneLocation(0), screenSize);
            Vector2 headScreenLocation = WorldToScreen(Headd, screenSize);
            float xHeight = feetScreenLocation.Y - headScreenLocation.Y;
            float xWidth = xHeight / 2.5f;

            float x = Headw2s.X; float y = Headw2s.Y;
            float flX = x - 7 - Height / 4f; float flY = y - 1;

            DrawFactory.DrawText(player.PlayerName.ToString(), fontSize, new Vector2(Headw2s.X, Headw2s.Y - 5), color);
            DrawFactory.DrawText("[" + (int)curDist / 100 + "M]", 7, new Vector2(Root.X , Root.Y + 10), color);
            DrawHealthBar(Headw2s, xHeight, player.Health);


        }
      private static void DrawHealthBar(Vector2 Headw2s,float Height,float Health)
        {
            float x = Headw2s.X; float y = Headw2s.Y;
            float flBoxes = (float)Math.Ceiling(Health / 10f);
            float flX = x - 7 - Height / 4f; float flY = y - 1;
            float flHeight = Height / 10f;
            Color ColHealth = new Color();
            Health = Math.Max(0, Math.Min(Health, 100));

            ColHealth.R = (byte)Math.Min((510 * (100 - Health)) / 100, 255);
            ColHealth.G = (byte)Math.Min((510 * Health) / 100, 255);
            ColHealth.B = 0; 
            ColHealth.A = 255;

            DrawFactory.DrawBox(flX, flY, 4, Height + 2, new Color(80, 80, 80,125));
            DrawFactory.DrawBox(flX, flY, 4, Height + 2, Color.Black);
            DrawFactory.DrawFilledBox(flX + 1, flY, 2, flHeight * flBoxes + 1, ColHealth);

            for (int i = 0; i < 10; i++)
                DrawFactory.DrawLine(flX, flY + i * flHeight, flX + 4, flY + i * flHeight, 1, Color.Black);
        }
      public static void DrawBox3d(AActor player, Vector2 screenSize, Color color)
        {
            SceneComponent RootComponent = player.RootComponent();
            Vector3 loclLocation = Main.localPawn.RootComponent().RelativeLocation;
            Vector3 enimeLocation = RootComponent.RelativeLocation;
            float distince = Vector3.Distance(loclLocation, enimeLocation);


            Vector3 Head = player.GetBoneLocation(6);
            Head.Z += 50;
            Vector2 Heads2s = WorldToScreen(Head, screenSize);




            float l, w, h, o;
            l = 80f; o = 50f; //box size for standing player
            h = Vector3.Distance(player.GetBoneLocation(0), Head);
            w = h / 2;//Vector3.Distance(player.GetBoneLocation(63), player.GetBoneLocation(64));


            float zOffset = -100f;

            //if (actor.Type == ActorTypes.Vehicle) //vehicles have a bigger box
            //{
            //	zOffset = 50;
            //	l = 200f; w = 160f; h = 80f; o = 0f;
            //}

            //build box
            Vector3 p00 = new Vector3(o, -w / 2, 0f);
            Vector3 p01 = new Vector3(o, w / 2, 0f);
            Vector3 p02 = new Vector3(o - l, w / 2, 0f);
            Vector3 p03 = new Vector3(o - l, -w / 2, 0f);

            //rotate rectangle to match actor rotation
            float theta1 = 2.0f * (float)Math.PI * (Main.cameraManager.CameraCache.POV.Rotation.Pitch) / 180.0f;
            Matrix rotM = Matrix.RotationZ((float)(theta1 / 2)); // rotate around Z-axis

            Vector3 curPos = new Vector3(RootComponent.RelativeLocation.X, RootComponent.RelativeLocation.Y, RootComponent.RelativeLocation.Z + zOffset);
            p00 = Vector3.TransformCoordinate(p00, rotM) + curPos;
            p01 = Vector3.TransformCoordinate(p01, rotM) + curPos;
            p02 = Vector3.TransformCoordinate(p02, rotM) + curPos;
            p03 = Vector3.TransformCoordinate(p03, rotM) + curPos;

            Vector2 s00 = WorldToScreen(p00, screenSize);
            Vector2 s01 = WorldToScreen(p01, screenSize);
            Vector2 s02 = WorldToScreen(p02, screenSize);
            Vector2 s03 = WorldToScreen(p03, screenSize);
            Vector2[] square0 = { s00, s01, s02, s03, s00 };

            for (int x = 0; x < square0.Length - 1; x++)
                DrawFactory.DrawLine(square0[x].X, square0[x].Y, square0[x + 1].X, square0[x + 1].Y,2, color);


            // top rectangle
            p00.Z += h; s00 = WorldToScreen(p00, screenSize);
            p01.Z += h; s01 = WorldToScreen(p01, screenSize);
            p02.Z += h; s02 = WorldToScreen(p02, screenSize);
            p03.Z += h; s03 = WorldToScreen(p03, screenSize);

            Vector2[] square1 = { s00, s01, s02, s03, s00 };
            for (int x = 0; x < square1.Length - 1; x++)
                DrawFactory.DrawLine(square1[x].X, square1[x].Y,square1[x + 1].X, square1[x + 1].Y,2, color);


            // upper/lower rectangles get connected
            DrawFactory.DrawLine(square0[0].X, square0[0].Y, square1[0].X, square1[0].Y,2, color);
            DrawFactory.DrawLine(square0[1].X, square0[1].Y, square1[1].X, square1[1].Y, 2, color);
            DrawFactory.DrawLine(square0[2].X, square0[2].Y, square1[2].X, square1[2].Y, 2, color);
            DrawFactory.DrawLine(square0[3].X, square0[3].Y, square1[3].X, square1[3].Y, 2, color);

        }
        public static void DrawItem(AActor item, Vector2 screenSize)
        {
           
            Vector2 location = WorldToScreen(item.RootComponent().RelativeLocation, screenSize);
            if (location.X > screenSize.X || location.Y > screenSize.Y)
                return;
            DrawFactory.DrawText(item.IsItem, 7,new Vector2(location.X, location.Y), Color.Red);

        }
        public static void DrawVehicle(AActor Vehicle, Vector2 screenSize)
        {
           

            Vector2 location = WorldToScreen(Vehicle.RootComponent().RelativeLocation, screenSize);
            if (location.X > screenSize.X || location.Y > screenSize.Y)
                return;

            DrawFactory.DrawText(Vehicle.IsVehicle, 18, new Vector2(location.X, location.Y), Color.Red);
        }
        public static Vector2 WorldToScreen(Vector3 position, Vector2 screenSize)
        {
            return Main.cameraManager.WorldToScreen(position, screenSize);
        }
        public static bool WorldToScreen(Vector3 position, out Vector2 ScreenPosition, Vector2 screenSize)
        {
            ScreenPosition = new Vector2();
            return Main.cameraManager.WorldToScreen(position, screenSize, out ScreenPosition);
        }
    }
}
