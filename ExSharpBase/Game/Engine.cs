using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ExSharpBase.Program;
using ExSharpBase.Modules;
using static ExSharpBase.Modules.NativeImport;

namespace ExSharpBase.Game
{
    class Engine
    {
        public static int ViewWorld;
        public static int oUWorld;
        public static int GName;
        private static Dictionary<int, string> GNames;
        public static void Inti()
        {
            ViewWorld = (int)GetViewWorld();
            oUWorld = ViewWorld - 4582196;
            GName = ViewWorld - 4619344;
            GNames = new Dictionary<int, string>();
            LogService.Log("ViewWorld found : " + ViewWorld);
            LogService.Log("UWorld found : " + oUWorld);
            LogService.Log("GNames found : " + GName);
        }
        public static string GetEntityType(int id)
        {
            string result;
            if (GNames.ContainsKey(id))
            {
                result = GNames[id];
            }
            else
            {
                result = GetEntityTypeFromMemory(id);
                GNames.Add(id, result);

            }
            return result;
        }
        private static string GetEntityTypeFromMemory(int id)
        {

            string result = "";
            int gname = Driver.Read<int>(GName);
            if (id > 0 && id < 2000000)
            {
                int page = id / 16384;
                int index = id % 16384;
                int secPartAddv = Driver.Read<int>(gname + page * 0x4);
                if (secPartAddv > 0)
                {
                    int nameAddv = Driver.Read<int>(secPartAddv + index * 0x4);
                    if (nameAddv > 0)
                    {
                        result = Encoding.UTF8.GetString(Driver.Read(nameAddv + 0x8, 32)).Split('\0')[0];
                    }
                }
            }
            return result;
        }
        #region "Game Search"
        private static bool PatternCheck(int nOffset, byte[] arrPattern, byte[] source)
        {
            if (nOffset + arrPattern.Length > source.Length) return false;
            for (int i = 0; i < arrPattern.Length; i++)
            {
                if (arrPattern[i] == (byte)'?')
                    continue;

                if (arrPattern[i] != source[nOffset + i])
                    return false;
            }

            return true;
        }

        public static long GetViewWorld()
        {
            string szPattern = "02 00 00 00 80 00 00 00 FF FF FF FF 00 00 00 00 01";
            long iStartAddress = 0;
            long iEndAddress = 0x7FFFFFFF;
            IntPtr h_process = OpenProcess(0x1F0FFF, false, Driver.ProcessID.Id);
            int offset = 0;
            var patternArr = szPattern.Split(' ');
            List<byte> result = new List<byte>();
            for (int i = offset; i < patternArr.Length; i++)
            {
                if (patternArr[i] == "?") continue;
                result.Add(Convert.ToByte(patternArr[i], 16));
            }
            byte[] arrPattern = result.ToArray();
            MEMORY_BASIC_INFORMATION MBI;
            long iAddress = iStartAddress; byte[] bBuffer;
            List<long> matchAddvs = new List<long>();
            do
            {
                int iRead = VirtualQueryEx(h_process, (IntPtr)iAddress, out MBI, (uint)Marshal.SizeOf<MEMORY_BASIC_INFORMATION>());
                if ((iRead > 0) && ((long)MBI.RegionSize > 0))
                {

                    bBuffer = Driver.Read((int)MBI.BaseAddress, (int)MBI.RegionSize);

                    for (int i = 0; i < bBuffer.Length; i++)
                    {
                        if (bBuffer[i] != arrPattern[0])
                            continue;
                        if (PatternCheck(i, arrPattern, bBuffer))
                        {
                            matchAddvs.Add((long)(iAddress + i));
                            i += arrPattern.Length;
                        }
                    }
                }
                iAddress = (MBI.BaseAddress.ToInt64() + MBI.RegionSize.ToInt64());
            } while (iAddress <= iEndAddress);
            CloseHandle(h_process);
            long[] tmpViewWorlds = matchAddvs.ToArray();
            long[] viewWorlds = new long[tmpViewWorlds.Length];
            for (int i = 0; i < viewWorlds.Length; i++)
            {
                viewWorlds[i] = tmpViewWorlds[i] - 32;
            }

            long tmp;
            float t1, t2, t3, t4;
            for (int i = 0; i < viewWorlds.Length; i++)
            {
                tmp = Driver.Read<int>(Driver.Read<int>((int)viewWorlds[i]) + 32) + 512;
                t1 = Driver.Read<float>((int)tmp + 56);
                t2 = Driver.Read<float>((int)tmp + 40);
                t3 = Driver.Read<float>((int)tmp + 24);
                t4 = Driver.Read<float>((int)tmp + 8);
                if (t1 >= 3 && t2 == 0 && t3 == 0 && t4 == 0)
                {
                    return viewWorlds[i];
                }
            }
            return -1;
        }

        #endregion
    }
}
