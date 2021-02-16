using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;

namespace ExSharpBase.Modules
{
     public static class Utils
    {
        public static Process GetPubgProcess()
        {
            try
            {
                bool found = false;
                Process processPID = null;
                do
                {
                    int maxThread = 0;
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.ProcessName == "aow_exe" && process.Threads.Count > maxThread)
                        {
                            maxThread = process.Threads.Count;
                            processPID = process;
                        }
                    }
                    if (processPID != null)
                        found = true;
                } while (!found);
                return processPID;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Could Not Find League of Legend's Process\n{Ex.ToString()}");
                return null;
            }
           
        }

        public static bool IsGameOnDisplay()
        {
            string windowTitle = NativeImport.GetActiveWindowTitle();
            if(windowTitle !=null)
            {
                if (windowTitle.Contains("Turbo AOW Engine"))
                    return true;
            }
           
            return false;
            
        }
        public static void EnterDebugMode()
        {
            Process.EnterDebugMode();
        }
        public static IntPtr GetPubgWindow()
        {

            IntPtr hwnd = NativeImport.FindWindow("TXGuiFoundation", "Gameloop【Turbo AOW Engine-4.4】");
            if (hwnd == IntPtr.Zero)
            {
                hwnd = NativeImport.FindWindow("TXGuiFoundation", "Gameloop【Turbo AOW Engine】");
            }
            hwnd = NativeImport.FindWindowEx(hwnd, 0, "AEngineRenderWindowClass", "AEngineRenderWindow");
            return hwnd;
           
        }
        public static bool IsKeyPressed(System.Windows.Forms.Keys keys)
        {
            return 0 != (NativeImport.GetAsyncKeyState((int)keys) & 0x8000);
        }

        public static bool IsKeyPressed(uint keys)
        {
            return 0 != (NativeImport.GetAsyncKeyState((int)keys) & 0x8000);
        }
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
