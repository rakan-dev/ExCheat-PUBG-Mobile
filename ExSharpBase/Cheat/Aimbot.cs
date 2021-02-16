using ExSharpBase.Events;
using ExSharpBase.Game;
using ExSharpBase.Modules;
using ExSharpBase.Overlay.Drawing;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExSharpBase.Cheat
{
    public static class Aimbot
    {

        public static AimPos aimPos = AimPos.Head;
        public static float raduisFov = Drawing.FOV;
        public static float distance = 999999999;
        public static Rotator idealAngDelta;
        public static AActor bestActor = default;
        public static bool SilentAim = Drawing.DrawingProperties["MagicBullet"];
        public static bool aimbot = Drawing.DrawingProperties["Aimbot"];
        public static bool MouseAim = false;
        public static Vector2 screensize = new Vector2(Program.DrawBase.Width, Program.DrawBase.Height);
        public static void BeginFrame()
        {
            distance = 999999999;
            idealAngDelta = new Rotator(0, 0, 0);
            bestActor = default;
            SilentAim = Drawing.DrawingProperties["MagicBullet"];
            aimbot = Drawing.DrawingProperties["Aimbot"];
            raduisFov = Drawing.FOV;
        }
        public static void EvaluateTarget(AActor actor)
        {
           // if (!IsVisiable(actor))
          //      return;
            if (actor.Health < 1f)
                return;

            Vector2 centerScreen = new Vector2(screensize.X / 2, screensize.Y / 2);
            Vector3 vecLocalPlayer = Main.cameraManager.CameraCache.POV.Location;
            Vector3 vecTargetPlayer = actor.GetBoneLocation((int)aimPos);
            Vector2 vecTargetPlayer2d = Main.cameraManager.WorldToScreen(vecTargetPlayer, screensize);


            float BulletSpeed = Main.localPawn.EquipWeapon().ShootWeaponEntityComp().BulletFireSpeed /100;
            Vector3 TargetVelocity = GetPlayerVelocity(actor);
            float curDist = (vecTargetPlayer - vecLocalPlayer).Length();
            float timeToTravel = (curDist / 100) / BulletSpeed;
            if(timeToTravel > 1f && !float.IsInfinity(timeToTravel))
            {
                vecTargetPlayer += TargetVelocity * timeToTravel;
                vecTargetPlayer.Z += TargetVelocity.Z * timeToTravel + 0.5f * 588.6f * timeToTravel * timeToTravel;
            }

            Vector2 head = Main.cameraManager.WorldToScreen(actor.GetBoneLocation((int)aimPos),screensize);
            Vector2 prediction = Main.cameraManager.WorldToScreen(vecTargetPlayer, screensize);
            DrawFactory.DrawLine(head.X, head.Y, prediction.X, prediction.Y, 3, Color.Yellow);



            if (isInside(centerScreen.X, centerScreen.Y, 200, vecTargetPlayer2d))
            {
                if (curDist < distance)
                {
                    Rotator angDelta = ToRotator(vecLocalPlayer, vecTargetPlayer);
                    idealAngDelta = angDelta;
                    bestActor = actor;
                    distance = curDist;
                }
            }
        }
        public static void AimToTarget()
        {

            SetViewAngles(idealAngDelta);//Engine.cameraManager.CameraCache.POV.Rotation + (idealAngDelta));
        }
        public static Vector3 GetPlayerVelocity(AActor p)
        {
            return p.STCharacterMovement().LastUpdateVelocity;
        }
        public static void SetViewAngles(Rotator ang)
        {
            if (ang == new Rotator(0, 0, 0))
                return;
            if (MouseAim)
            {
                Vector2 Aim = Main.cameraManager.WorldToScreen(bestActor.GetBoneLocation((int)aimPos), screensize);
                var point = new System.Drawing.Point((int)Aim.X, (int)Aim.Y);

                var delay = TimeSpan.FromMilliseconds(1);

                CursorMover.MoveCursor(point, delay);
            }
            else
            {
                if (aimbot)
                    Main.LocalPlayer.SetViewAngles(ang);
            }
            if (SilentAim)
                Main.playerController.SetViewAngles(ang);
        }
        public static bool IsVisiable(AActor actor)
        {
            var localMesh = Main.localPawn.Mesh();
            var targetMesh = actor.Mesh();
            var localRenderTime = localMesh.LastRenderTimeOnScreen;
            var targetRenderTime = targetMesh.LastRenderTimeOnScreen;
            bool visiable1 = false, visiable = false;

            if (localRenderTime == targetRenderTime)
                visiable1 = true;
            if ((targetMesh.bRecentlyRendered & 0x4) > 0)
                visiable = true;
            return visiable == true && visiable1 == true;
        }
        public static bool isInside(float circle_x, float circle_y,
                   float rad, Vector2 point)
        {
            // Compare radius of circle with distance  
            // of its center from given point 
            if ((point.X - circle_x) * (point.X - circle_x) +
                (point.Y - circle_y) * (point.Y - circle_y) <= rad * rad)
                return true;
            else
                return false;
        }
        public static Rotator ToRotator(Vector3 local, Vector3 target)
        {
            Vector3 rotation = local - target;

            Rotator newViewAngle = new Rotator();

            float hyp = (float)Math.Sqrt(rotation.X * rotation.X + rotation.Y * rotation.Y);

            newViewAngle.Pitch = (float)(-Math.Atan(rotation.Z / hyp) * (180f / Math.PI));
            newViewAngle.Yaw = (float)(Math.Atan(rotation.Y / rotation.X) * (180f / Math.PI));
            newViewAngle.Roll = (float)0f;

            if (rotation.X >= 0f)
                newViewAngle.Yaw += 180.0f;

            return newViewAngle.Clamp();
        }
        public enum AimPos
        {
            Head = 6, Chest = 1
        }

    }
}
