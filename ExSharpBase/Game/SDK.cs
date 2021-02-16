using ExSharpBase.Modules;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ExSharpBase.Program;
namespace ExSharpBase.Game
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UWorld
    {
        [FieldOffset(0x20)]
        private int CurrentLevelPtr; //0x0348

        [FieldOffset(0x24)]
        private int GameInstancePtr; //0x0A98

        public ULevel CurrentLevel()
        {

            return Driver.Read<ULevel>(CurrentLevelPtr);
        }

        public UGameInstance OwningGameInstance()
        {

            return Driver.Read<UGameInstance>(GameInstancePtr);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UGameInstance
    {
        [FieldOffset(0x60)]
        private int localPlayersPtr; //0x0150

        public ULocalPlayer LocalPlayer()
        {
            return Driver.Read<ULocalPlayer>(localPlayersPtr);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ULocalPlayer
    {
        [FieldOffset(0x20)]
        private int PlayerControllerPtr; //0x0038
        public void SetViewAngles(Rotator rotator)
        {
            Driver.Write<Rotator>(PlayerControllerPtr + 0x2e0, rotator);
        }
        public APlayerController PlayerController()
        {
            return Driver.Read<APlayerController>(PlayerControllerPtr);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct APlayerController
    {
        [FieldOffset(0xC)]
        private int BasePointer;
        [FieldOffset(0x2e0)]
        public Rotator ControlRotation;

        [FieldOffset(0x318)]
        private int AknowledgedPawnPtr; //0x0448

        [FieldOffset(0x328)]
        private int playerCameraManagerPtr; //0x0468





        public void SetViewAngles(Rotator rotator)
        {
            Driver.Write<Rotator>(playerCameraManagerPtr + 0x358, rotator);
        }

        public AActor AknowledgedPawn()
        {
            return Driver.Read<AActor>(AknowledgedPawnPtr);
        }

        public APlayerCameraManager PlayerCameraManager()
        {
            return Driver.Read<APlayerCameraManager>(playerCameraManagerPtr);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct APlayerCameraManager
    {
        [FieldOffset(0x330)]
        public FCameraCacheEntry CameraCache; //0x03C0

        public bool WorldToScreen(Vector3 WorldLocation, Vector2 screenSize, out Vector2 Screenlocation)
        {

            Screenlocation = new Vector2(0, 0);

            var POV = CameraCache.POV;
            Rotator Rotation = POV.Rotation;

            Vector3 vAxisX, vAxisY, vAxisZ;
            Rotation.GetAxes(out vAxisX, out vAxisY, out vAxisZ);

            Vector3 vDelta = WorldLocation - POV.Location;
            Vector3 vTransformed = new Vector3(Vector3.Dot(vDelta, vAxisY), Vector3.Dot(vDelta, vAxisZ), Vector3.Dot(vDelta, vAxisX));

            if (vTransformed.Z < 1f)
                vTransformed.Z = 1f;

            float FovAngle = POV.FOV;
            float ScreenCenterX = screenSize.X / 2;
            float ScreenCenterY = screenSize.Y / 2;

            Screenlocation.X = ScreenCenterX + vTransformed.X * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;
            Screenlocation.Y = ScreenCenterY - vTransformed.Y * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;

            if (Screenlocation.X <= (int)screenSize.X + 50 && Screenlocation.X >= -0 && Screenlocation.Y <= (screenSize.Y) && Screenlocation.Y >= 0)
            {
                return true;
            }

            return false;


        }

        public Vector2 WorldToScreen(Vector3 WorldLocation, Vector2 screenSize)
        {
            Vector2 Screenlocation;
            Screenlocation = new Vector2(0, 0);

            FMinimalViewInfo POV = CameraCache.POV;
            Rotator Rotation = POV.Rotation;

            Vector3 vAxisX, vAxisY, vAxisZ;
            Rotation.GetAxes(out vAxisX, out vAxisY, out vAxisZ);

            Vector3 vDelta = WorldLocation - POV.Location;
            Vector3 vTransformed = new Vector3(Vector3.Dot(vDelta, vAxisY), Vector3.Dot(vDelta, vAxisZ), Vector3.Dot(vDelta, vAxisX));

            if (vTransformed.Z < 1f)
                vTransformed.Z = 1f;

            float FovAngle = POV.FOV;
            float ScreenCenterX = screenSize.X / 2;
            float ScreenCenterY = screenSize.Y / 2;

            Screenlocation.X = ScreenCenterX + vTransformed.X * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;
            Screenlocation.Y = ScreenCenterY - vTransformed.Y * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;

            return Screenlocation;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FCameraCacheEntry
    {
        [FieldOffset(0x10)]
        public FMinimalViewInfo POV; //0x0010
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FMinimalViewInfo
    {
        [FieldOffset(0x0)]
        public Vector3 Location; //0x0028

        [FieldOffset(0x18)]
        public Rotator Rotation; //0x000C

        [FieldOffset(0x24)]
        public float FOV; //0x0024


    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ULevel
    {
        [FieldOffset(0x70)]
        private TArray<AActor> ActorArrayPtr; //0x0058

        public List<List<AActor>> AActors()
        {
            List<AActor> players = new List<AActor>();
            List<AActor> items = new List<AActor>();
            List<AActor> vehicles = new List<AActor>();
            List<List<AActor>> aActors = new List<List<AActor>>();
            for (int i = 0; i < ActorArrayPtr.Count; i++)
            {
                AActor actor = ActorArrayPtr.ReadValue(i, true);
                if (actor.IsPlayer)
                {
                    players.Add(actor);
                    continue;
                }
                if (actor.IsItem != Item.Useless.GetDescription())
                {
                    items.Add(actor);
                    continue;
                }
                if (actor.IsVehicle != Vehicle.Unknown.GetDescription())
                {
                    vehicles.Add(actor);
                    continue;
                }

            }
            aActors.Add(players);
            aActors.Add(items);
            aActors.Add(vehicles);
            return aActors;
        }
    }
    #region "Actor"
    [StructLayout(LayoutKind.Explicit)]
    public struct AActor
    {
        [FieldOffset(0x10)]
        public int ObjectID; //0x0000


        [FieldOffset(0x138)]
        private int RootComponentPtr;//[Offset: 0x138 , Size: 4]
        [FieldOffset(0xff4)]
        private int STCharacterMovementPtr;//[Offset: 0xff4 , Size: 4]
        [FieldOffset(0x308)]
        private int MeshPtr; //0x0488

        [FieldOffset(0x5f8)]
        public FString PlayerName;
        [FieldOffset(0x620)]
        public int TeamID; //0x07DC
        [FieldOffset(0x828)]
        public long CurrentStates;//[Offset: 0x828 , Size: 8]; 

        [FieldOffset(0x830)]
        public float Health; //0x0E38

        [FieldOffset(0x1604)]
        private int EquipWeaponPtr; // AnimStatusKeyList (0x151c + 0x1c)
        public bool IsPlayer
        {
            get
            {
                string actorType = Engine.GetEntityType(this.ObjectID);
                if (actorType.Contains("BP_PlayerPawn"))
                    return true;
                return false;
            }
        }
        public string IsItem
        {
            get
            {
                string str = Engine.GetEntityType(this.ObjectID);
                Item item = Item.Useless;
                //if (!str.Contains("Pickup") || !str.Contains("PickUp"))
                //    return Item.Useless;
                if (str.Contains("Grenade_Shoulei_Weapon_Wra"))
                    item = Item.Grenade;
                if (str.Contains("MZJ_4X"))
                    item = Item.Scope4x;
                if (str.Contains("MZJ_6X"))
                    item = Item.Scope6x;
                if (str.Contains("MZJ_8X"))
                    item = Item.Scope8x;
                if (str.Contains("DJ_Large_EQ"))
                    item = Item.RifleMagazine;
                if (str.Contains("QK_Sniper_Suppressor"))
                    item = Item.SniperSilenter;
                if (str.Contains("QK_Large_Suppressor"))
                    item = Item.RifleSilenter;
                if (str.Contains("Ammo_556mm"))
                    item = Item.Ammo556;
                if (str.Contains("Ammo_762mm"))
                    item = Item.Ammo762;
                if (str.Contains("Ammo_300Magnum"))
                    item = Item.AmmoMagnum;
                if (str.Contains("Helmet_Lv3"))
                    item = Item.HelmetLv3;
                if (str.Contains("Armor_Lv3"))
                    item = Item.ArmorLv3;
                if (str.Contains("Bag_Lv3"))
                    item = Item.BagLv3;
                if (str.Contains("Helmet_Lv2"))
                    item = Item.HelmetLv2;
                if (str.Contains("Armor_Lv2"))
                    item = Item.ArmorLv2;
                if (str.Contains("Bag_Lv2"))
                    item = Item.BagLv2;
                if (str.Contains("Firstaid"))
                    item = Item.AidKit;
                if (str.Contains("Injection"))
                    item = Item.Epinephrine;
                if (str.Contains("Pills"))
                    item = Item.PainKiller;
                if (str.Contains("Drink"))
                    item = Item.EnegyDrink;
                if (!str.Contains("Wrapper"))
                    item = Item.Useless;
                if (str.Contains("Pistol_Flaregun"))
                    item = Item.FlareGun;
                if (str.Contains("AWM"))
                    item = Item.AWM;
                if (str.Contains("Kar98k"))
                    item = Item.Kar98;
                if (str.Contains("Mk14"))
                    item = Item.MK14;
                if (str.Contains("DP28"))
                    item = Item.DP28;
                if (str.Contains("SKS"))
                    item = Item.SKS;
                if (str.Contains("Groza"))
                    item = Item.Groza;
                if (str.Contains("M762"))
                    item = Item.M762;
                if (str.Contains("AKM"))
                    item = Item.AKM;
                if (str.Contains("M249"))
                    item = Item.M249;
                if (str.Contains("M24"))
                    item = Item.M24;
                if (str.Contains("AUG"))
                    item = Item.AUG;
                if (str.Contains("QBZ"))
                    item = Item.QBZ;
                if (str.Contains("M416"))
                    item = Item.M4A1;
                if (str.Contains("SCAR"))
                    item = Item.SCARL;

                return item.GetDescription();
            }
        }
        public string IsVehicle
        {
            get
            {
                string str = Engine.GetEntityType(this.ObjectID);
                Vehicle vehicle = Vehicle.Unknown;
                if (str.Contains("BRDM"))
                    vehicle = Vehicle.BRDM;
                if (str.Contains("Scooter"))
                    vehicle = Vehicle.Scooter;
                if (str.Contains("Motorcycle"))
                    vehicle = Vehicle.Motorcycle;
                if (str.Contains("MotorcycleCart"))
                    vehicle = Vehicle.MotorcycleCart;
                if (str.Contains("Snowmobile"))
                    vehicle = Vehicle.Snowmobile;
                if (str.Contains("Tuk"))
                    vehicle = Vehicle.Tuk;
                if (str.Contains("Buggy"))
                    vehicle = Vehicle.Buggy;
                if (str.Contains("open"))
                    vehicle = Vehicle.Sports;
                if (str.Contains("close"))
                    vehicle = Vehicle.Sports;
                if (str.Contains("Dacia"))
                    vehicle = Vehicle.Dacia;
                if (str.Contains("Rony"))
                    vehicle = Vehicle.Rony;
                if (str.Contains("UAZ"))
                    vehicle = Vehicle.UAZ;
                if (str.Contains("MiniBus"))
                    vehicle = Vehicle.MiniBus;
                if (str.Contains("PG117"))
                    vehicle = Vehicle.PG117;
                if (str.Contains("AquaRail"))
                    vehicle = Vehicle.AquaRail;
                if (str.Contains("BP_AirDropPlane_C"))
                    vehicle = Vehicle.BP_AirDropPlane_C;

                return vehicle.GetDescription();
            }
        }

        public SceneComponent RootComponent()
        {
            return Driver.Read<SceneComponent>(RootComponentPtr);
        }
        public STCharacterMovementComponent STCharacterMovement()
        {
            return Driver.Read<STCharacterMovementComponent>(STCharacterMovementPtr);
        }
        public STExtraWeapon EquipWeapon()
        {
            return Driver.Read<STExtraWeapon>(EquipWeaponPtr);
        }
        public USkeletalMeshComponent Mesh()
        {
            return Driver.Read<USkeletalMeshComponent>(MeshPtr);
        }
        public override string ToString()
        {
            return Engine.GetEntityType(this.ObjectID);
        }
        public Vector3 GetBoneLocation(int bone)
        {
            USkeletalMeshComponent Mesh = this.Mesh();
            FTransform2 boneTransform = Driver.Read<FTransform2>(Mesh.BoneArray + (bone * 0x30));
            Matrix boneMatrix = boneTransform.ToMatrixWithScale();
            Matrix componentToWorldMatrix = Mesh.ComponentToWorld.ToMatrixWithScale();

            Matrix newMatrix = boneMatrix * componentToWorldMatrix;

            return new Vector3(newMatrix.M41, newMatrix.M42, newMatrix.M43);
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct STCharacterMovementComponent
    {
        [FieldOffset(0x250)]
        public Vector3 LastUpdateVelocity;//[Offset: 0x250 , Size: 12]
    }

        
    public enum Item
    {
        [Description("Useless")]
        Useless,
        [Description("[Med] Enegy Drink")]
        EnegyDrink,
        [Description("[Med] Epinephrine")]
        Epinephrine,
        [Description("[Med] Pain Killer")]
        PainKiller,
        [Description("[Med] First Aid Kit")]
        AidKit,
        [Description("[Armor] Lv.3 Bag")]
        BagLv3,
        [Description("[Armor] Lv.2 Bag")]
        BagLv2,
        [Description("[Armor] Lv.2 Armor")]
        ArmorLv2,
        [Description("[Armor] Lv.3 Armor")]
        ArmorLv3,
        [Description("[Armor] Lv.3 Helmet")]
        HelmetLv3,
        [Description("[Armor] Lv.2 Helmet")]
        HelmetLv2,
        [Description("[Sniper] AWM")]
        AWM,
        [Description("[Rifle] SCAR-L")]
        SCARL,
        [Description("[Sniper] Kar-98")]
        Kar98,
        [Description("[Rifle] M762")]
        M762,
        [Description("[MachineGun] DP-28")]
        DP28,
        [Description("[Rifle] Groza")]
        Groza,
        [Description("[Rifle] AKM")]
        AKM,
        [Description("[Rifle] AUG")]
        AUG,
        [Description("[Rifle] QBZ")]
        QBZ,
        [Description("[MachineGun] M249")]
        M249,
        [Description("[Rifle] M4A1")]
        M4A1,
        [Description("[Ammo] 300 Magnum Ammo")]
        AmmoMagnum,
        [Description("[Ammo] 7.62 Ammo")]
        Ammo762,
        [Description("[Ammo] 5.56 Ammo")]
        Ammo556,
        [Description("[Scope] 4x Scope")]
        Scope4x,
        [Description("[Scope] 6x Scope")]
        Scope6x,
        [Description("[Scope] 8x Scope")]
        Scope8x,
        [Description("[Apendix] Rifle Silenter")]
        RifleSilenter,
        [Description("[Apendix] Rifle Rapid Expansion Magazine")]
        RifleMagazine,
        [Description("[Armor] Ghillie Suit")]
        GhillieSuit,
        [Description("[Pistol] Flare Gun")]
        FlareGun,
        [Description("[Sniper] M24")]
        M24,
        [Description("[Apendix] Sniper Silenter")]
        SniperSilenter,
        [Description("[Sniper] MK14")]
        MK14,
        [Description("[Sniper] SKS")]
        SKS,
        [Description("[Ammo] Grenade")]
        Grenade
    }
    public enum Vehicle
    {
        [Description("Unknown")]
        Unknown,
        [Description("BRDM")]
        BRDM,
        [Description("Scooter")]
        Scooter,
        [Description("Motorcycle")]
        Motorcycle,
        [Description("MotorcycleCart")]
        MotorcycleCart,
        [Description("Snowmobile")]
        Snowmobile,
        [Description("Tuk")]
        Tuk,
        [Description("Buggy")]
        Buggy,
        [Description("Sports")]
        Sports,
        [Description("Dacia")]
        Dacia,
        [Description("Rony")]
        Rony,
        [Description("PickUp")]
        PickUp,
        [Description("UAZ")]
        UAZ,
        [Description("MiniBus")]
        MiniBus,
        [Description("PG117")]
        PG117,
        [Description("AquaRail")]
        AquaRail,
        [Description("AirPlane")]
        BP_AirDropPlane_C

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct SceneComponent
    {
        [FieldOffset(0x118)]
        public Vector3 RelativeLocation;//[Offset: 0x118 , Size: 12]
        [FieldOffset(0x124)]
        public Rotator RelativeRotation;//[Offset: 0x124 , Size: 12]
        [FieldOffset(0x130)]
        public Vector3 RelativeScale3D;//[Offset: 0x130 , Size: 12]
        [FieldOffset(0x1a0)]
        public Vector3 ComponentVelocity;//[Offset: 0x1a0 , Size: 12]
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct STExtraWeapon
    {
        [FieldOffset(0x10)]
        public int ObjectID;
        [FieldOffset(0x8c8)]
        public int CurBulletNumInClip;
        [FieldOffset(0x8dc)]
        public int CurMaxBulletNumInOneClip;
        [FieldOffset(0x9c8)]
        private int ShootWeaponEntityCompPtr;

        public override string ToString()
        {
            string[] str = Engine.GetEntityType(this.ObjectID).Split('_');
            if (str.Length >= 2)
                return str[2];
            else
                return "Fist";
        }
        public void no_recoil()
        {
            Driver.Write<float>(ShootWeaponEntityCompPtr + 0x7e8, 100f);

        }
        public ShootWeaponEntity ShootWeaponEntityComp()
        {
            return Driver.Read<ShootWeaponEntity>(ShootWeaponEntityCompPtr);
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct ShootWeaponEntity
    {
        [FieldOffset(0x3c4)]
        public float BulletFireSpeed;//[Offset: 0x33c , Size: 4]
        [FieldOffset(0x490)]
        public float BulletRange;//[Offset: 0x404 , Size: 4]
        [FieldOffset(0x67c)]
        public SRecoilInfo RecoilInfo;//[Offset: 0x5f4 , Size: 100]
        [FieldOffset(0x794)]
        public float WeaponAimFOV;//[Offset: 0x70c , Size: 4]
        [FieldOffset(0x798)]
        public float WeaponAimInTime;//[Offset: 0x710 , Size: 4]
        [FieldOffset(0x79c)]
        public float WeaponAimPitchRate;//[Offset: 0x714 , Size: 4]
        [FieldOffset(0x7a0)]
        public float WeaponAimYawRate;//[Offset: 0x718 , Size: 4]
        [FieldOffset(0x7a4)]
        public float GameMotionYawRate;//[Offset: 0x71c , Size: 4]
        [FieldOffset(0x7a8)]
        public float GameMotionPitchRate;//[Offset: 0x720 , Size: 4]
        [FieldOffset(0xf4)]
        public int MaxNoGravityRange;//[Offset: 0xf4 , Size: 4]
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct SRecoilInfo
    {
        [FieldOffset(0x0)]
        public float VerticalRecoilMin;//[Offset: 0x0 , Size: 4]
        [FieldOffset(0x4)]
        public float VerticalRecoilMax;//[Offset: 0x4 , Size: 4]
        [FieldOffset(0x8)]
        public float VerticalRecoilVariation;//[Offset: 0x8 , Size: 4]
        [FieldOffset(0xc)]
        public float VerticalRecoveryModifier;//[Offset: 0xc , Size: 4]
        [FieldOffset(0x10)]
        public float VerticalRecoveryClamp;//[Offset: 0x10 , Size: 4]
        [FieldOffset(0x14)]
        public float VerticalRecoveryMax;//[Offset: 0x14 , Size: 4]
        [FieldOffset(0x18)]
        public float LeftMax;//[Offset: 0x18 , Size: 4]
        [FieldOffset(0x1c)]
        public float RightMax;//[Offset: 0x1c , Size: 4]
        [FieldOffset(0x20)]
        public float HorizontalTendency;//[Offset: 0x20 , Size: 4]
                                        //   CurveVector* RecoilCurve;//[Offset: 0x24 , Size: 4]
        [FieldOffset(0x28)]
        public int BulletPerSwitch;//[Offset: 0x28 , Size: 4]
        [FieldOffset(0x2c)]
        public float TimePerSwitch;//[Offset: 0x2c , Size: 4]
        [FieldOffset(0x30)]
        public bool SwitchOnTime;//(ByteOffset: 0, ByteMask: 1, FieldMask: 255)[Offset: 0x30 , Size: 1]
        [FieldOffset(0x34)]
        public float RecoilSpeedVertical;//[Offset: 0x34 , Size: 4]
        [FieldOffset(0x38)]
        public float RecoilSpeedHorizontal;//[Offset: 0x38 , Size: 4]
        [FieldOffset(0x3c)]
        public float RecovertySpeedVertical;//[Offset: 0x3c , Size: 4]
        [FieldOffset(0x40)]
        public float RecoilValueClimb;//[Offset: 0x40 , Size: 4]
        [FieldOffset(0x44)]
        public float RecoilValueFail;//[Offset: 0x44 , Size: 4]
        [FieldOffset(0x48)]
        public float RecoilModifierStand;//[Offset: 0x48 , Size: 4]
        [FieldOffset(0x4c)]
        public float RecoilModifierCrouch;//[Offset: 0x4c , Size: 4]
        [FieldOffset(0x50)]
        public float RecoilModifierProne;//[Offset: 0x50 , Size: 4]
        [FieldOffset(0x54)]
        public float RecoilHorizontalMinScalar;//[Offset: 0x54 , Size: 4]
        [FieldOffset(0x58)]
        public float BurstEmptyDelay;//[Offset: 0x58 , Size: 4]
        [FieldOffset(0x5c)]
        public bool ShootSightReturn;//(ByteOffset: 0, ByteMask: 1, FieldMask: 255)[Offset: 0x5c , Size: 1]
        [FieldOffset(0x60)]
        public float ShootSightReturnSpeed;//[Offset: 0x60 , Size: 4]
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct USkeletalMeshComponent
    {
        [FieldOffset(0x140)]
        public FTransform ComponentToWorld; //0x0200
        [FieldOffset(0x2f4)]
        public float LastRenderTimeOnScreen;
        [FieldOffset(0x62c)]
        public byte bRecentlyRendered;
        [FieldOffset(0x580)]
        public int BoneArray; //0x06F8
        [FieldOffset(0x6b8)]
        public int CachedBoneSpaceTransforms; //0x06F8
    }
    public enum Bone
    {
        head = 6,
        neck_01 = 5,
        neck_02 = 4,
        body_03 = 3,
        body_02 = 2,
        body_01 = 1,
        body_00 = 0,
        l_hand_01 = 10,
        l_hand_02 = 11,
        l_hand_03 = 12,
        l_hand_04 = 13,
        r_hand_01 = 31,
        r_hand_02 = 32,
        r_hand_03 = 33,
        r_hand_04 = 34,
        l_leg_01 = 52,
        l_leg_02 = 53,
        l_leg_03 = 54,
        l_leg_04 = 55,
        r_leg_01 = 56,
        r_leg_02 = 57,
        r_leg_03 = 58,
        r_leg_04 = 59,
    };
    #endregion
    #region "UE4 Structs"
    [StructLayout(LayoutKind.Sequential)]
    public struct FString
    {
        private int pData;
        private int DataSize;
        public override string ToString()
        {
            return Encoding.Unicode.GetString(Driver.Read(pData, DataSize * 0x4)).Split('\0')[0];
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct TArray<T> where T : struct
    {
        public int pData;
        public int Count;
        public int Max;

        public int this[int nIndex]
        {
            get
            {
                return pData + nIndex * 0x4;
            }
        }
        public T ReadValue(int nIndex, bool bDeref)
        {
            int ptrData = pData + nIndex * 0x4;

            if (bDeref)
                ptrData = Driver.Read<int>(ptrData);

            return Driver.Read<T>(ptrData);
        }
        public void SetValue(T value, int nIndex, bool bDeref)
        {
            int ptrData = pData + nIndex * 0x4;

            if (bDeref)
                ptrData = Driver.Read<int>(ptrData);

            Driver.Write<T>(ptrData, value);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct FQuat
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
    };
    [StructLayout(LayoutKind.Explicit)]
    public struct FTransform2
    {
        [FieldOffset(0x0)]
        public FQuat Rotation;
        [FieldOffset(0x10)]
        public Vector3 Translation;
        [FieldOffset(0x1c)]
        public Vector3 Scale3D;

        public Matrix ToMatrixWithScale()
        {
            Matrix m = new Matrix();

            m.M41 = Translation.X;
            m.M42 = Translation.Y;
            m.M43 = Translation.Z;

            float x2 = Rotation.X + Rotation.X;
            float y2 = Rotation.Y + Rotation.Y;
            float z2 = Rotation.Z + Rotation.Z;

            float xx2 = Rotation.X * x2;
            float yy2 = Rotation.Y * y2;
            float zz2 = Rotation.Z * z2;
            m.M11 = (1.0f - (yy2 + zz2)) * Scale3D.X;
            m.M22 = (1.0f - (xx2 + zz2)) * Scale3D.Y;
            m.M33 = (1.0f - (xx2 + yy2)) * Scale3D.Z;


            float yz2 = Rotation.Y * z2;
            float wx2 = Rotation.W * x2;
            m.M32 = (yz2 - wx2) * Scale3D.Z;
            m.M23 = (yz2 + wx2) * Scale3D.Y;


            float xy2 = Rotation.X * y2;
            float wz2 = Rotation.W * z2;
            m.M21 = (xy2 - wz2) * Scale3D.Y;
            m.M12 = (xy2 + wz2) * Scale3D.X;


            float xz2 = Rotation.X * z2;
            float wy2 = Rotation.W * y2;
            m.M31 = (xz2 + wy2) * Scale3D.Z;
            m.M13 = (xz2 - wy2) * Scale3D.X;

            m.M14 = 0.0f;
            m.M24 = 0.0f;
            m.M34 = 0.0f;
            m.M44 = 1.0f;

            return m;
        }
    };
    [StructLayout(LayoutKind.Explicit)]
    public struct FTransform
    {
        [FieldOffset(0x0)]
        public FQuat Rotation;
        [FieldOffset(0x10)]
        public Vector3 Translation;
        [FieldOffset(0x20)]
        public Vector3 Scale3D;

        public Matrix ToMatrixWithScale()
        {
            Matrix m = new Matrix();

            m.M41 = Translation.X;
            m.M42 = Translation.Y;
            m.M43 = Translation.Z;

            float x2 = Rotation.X + Rotation.X;
            float y2 = Rotation.Y + Rotation.Y;
            float z2 = Rotation.Z + Rotation.Z;

            float xx2 = Rotation.X * x2;
            float yy2 = Rotation.Y * y2;
            float zz2 = Rotation.Z * z2;
            m.M11 = (1.0f - (yy2 + zz2)) * Scale3D.X;
            m.M22 = (1.0f - (xx2 + zz2)) * Scale3D.Y;
            m.M33 = (1.0f - (xx2 + yy2)) * Scale3D.Z;


            float yz2 = Rotation.Y * z2;
            float wx2 = Rotation.W * x2;
            m.M32 = (yz2 - wx2) * Scale3D.Z;
            m.M23 = (yz2 + wx2) * Scale3D.Y;


            float xy2 = Rotation.X * y2;
            float wz2 = Rotation.W * z2;
            m.M21 = (xy2 - wz2) * Scale3D.Y;
            m.M12 = (xy2 + wz2) * Scale3D.X;


            float xz2 = Rotation.X * z2;
            float wy2 = Rotation.W * y2;
            m.M31 = (xz2 + wy2) * Scale3D.Z;
            m.M13 = (xz2 - wy2) * Scale3D.X;

            m.M14 = 0.0f;
            m.M24 = 0.0f;
            m.M34 = 0.0f;
            m.M44 = 1.0f;

            return m;
        }
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct Rotator
    {
        public float Pitch;
        public float Yaw;
        public float Roll;

        public Rotator(float flPitch, float flYaw, float flRoll)
        {
            Pitch = flPitch;
            Yaw = flYaw;
            Roll = flRoll;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.Pitch * this.Pitch + this.Yaw * this.Yaw + this.Roll * this.Roll);
            }
        }

        public Rotator Clamp()
        {
            var result = this;

            if (result.Pitch > 180)
                result.Pitch -= 360;

            else if (result.Pitch < -180)
                result.Pitch += 360;

            if (result.Yaw > 180)
                result.Yaw -= 360;

            else if (result.Yaw < -180)
                result.Yaw += 360;

            if (result.Pitch < -89)
                result.Pitch = -89;

            if (result.Pitch > 89)
                result.Pitch = 89;

            while (result.Yaw < -180.0f)
                result.Yaw += 360.0f;

            while (result.Yaw > 180.0f)
                result.Yaw -= 360.0f;

            result.Roll = 0;

            return result;




        }

        public Vector3 ToVector()
        {
            float radPitch = (float)(this.Pitch * Math.PI / 180f);
            float radYaw = (float)(this.Yaw * Math.PI / 180f);

            float SP = (float)Math.Sin(radPitch);
            float CP = (float)Math.Cos(radPitch);
            float SY = (float)Math.Sin(radYaw);
            float CY = (float)Math.Cos(radYaw);

            return new Vector3(CP * CY, CP * SY, SP);
        }

        public void GetAxes(out Vector3 x, out Vector3 y, out Vector3 z)
        {
            Matrix m = new Matrix();
            Rotator rot = this;
            float radPitch = (rot.Pitch * (float)Math.PI / 180.0f);
            float radYaw = (rot.Yaw * (float)Math.PI / 180.0f);
            float radRoll = (rot.Roll * (float)Math.PI / 180.0f);
            float SP = (float)Math.Sin((double)radPitch);
            float CP = (float)Math.Cos((double)radPitch);
            float SY = (float)Math.Sin((double)radYaw);
            float CY = (float)Math.Cos((double)radYaw);
            float SR = (float)Math.Sin((double)radRoll);
            float CR = (float)Math.Cos((double)radRoll);
            m.M11 = CP * CY;
            m.M12 = CP * SY;
            m.M13 = SP;
            m.M21 = SR * SP * CY - CR * SY;
            m.M22 = SR * SP * SY + CR * CY;
            m.M23 = -SR * CP;
            m.M31 = -(CR * SP * CY + SR * SY);
            m.M32 = CY * SR - CR * SP * SY;
            m.M33 = CR * CP;
            x = new Vector3(m.M11, m.M12, m.M13);
            y = new Vector3(m.M21, m.M22, m.M23);
            z = new Vector3(m.M31, m.M32, m.M33);
        }
        public static Rotator operator +(Rotator angA, Rotator angB) => new Rotator(angA.Pitch + angB.Pitch, angA.Yaw + angB.Yaw, angA.Roll + angB.Roll);
        public static Rotator operator -(Rotator angA, Rotator angB) => new Rotator(angA.Pitch - angB.Pitch, angA.Yaw - angB.Yaw, angA.Roll - angB.Roll);
        public static Rotator operator /(Rotator angA, float flNum) => new Rotator(angA.Pitch / flNum, angA.Yaw / flNum, angA.Roll / flNum);
        public static Rotator operator *(Rotator angA, float flNum) => new Rotator(angA.Pitch * flNum, angA.Yaw * flNum, angA.Roll * flNum);
        public static bool operator ==(Rotator angA, Rotator angB) => angA.Pitch == angB.Pitch && angA.Yaw == angB.Yaw && angA.Yaw == angB.Yaw;
        public static bool operator !=(Rotator angA, Rotator angB) => angA.Pitch != angB.Pitch || angA.Yaw != angB.Yaw || angA.Yaw != angB.Yaw;
    }
    #endregion

}
