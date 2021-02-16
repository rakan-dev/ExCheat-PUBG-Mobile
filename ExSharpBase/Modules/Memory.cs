using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpDX;
using System.Windows.Forms;
using System.IO;

namespace ExSharpBase.Modules
{
    class Memory
    {
        private static readonly int FILE_DEVICE_UNKNOWN = 0x22;
        private static readonly int METHOD_BUFFERED = 0x0;
        private static readonly int FILE_ANY_ACCESS = 0x0;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private static readonly string DriverName = "nbcv53nc";
        private readonly uint ctl_openprocess = CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0365, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private readonly uint ctl_read = CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0367, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private readonly uint ctl_write = CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0366, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private readonly uint ctl_base = CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0368, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private readonly uint ctl_clear = CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0369, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public IntPtr h_driver = CreateFileA("\\\\.\\" + DriverName, FileAccess.Read, 0, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
        public Process ProcessID { get; private set; }
        public void LoadDriver()
        {
            if (!HasValidHandle())
            {
                string DriverPath = Application.StartupPath + "\\kernelmode.sys";
                string kdmapperPath = Application.StartupPath + "\\kdmapper.exe";
                if (!File.Exists(DriverPath) && !File.Exists(kdmapperPath))
                    throw new Exception("Mapper Or Driver Dosen't exists.");
                MappingDriver();
                h_driver = CreateFileA("\\\\.\\" + DriverName, FileAccess.Read, 0, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
            }

        }
        public void Attach(Process pid)
        {
            ProcessID = pid;
            if (!HasValidHandle())
                h_driver = CreateFileA("\\\\.\\" + DriverName, FileAccess.Read, 0, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
        }
        private void MappingDriver(string driverName = "kernelmode.sys")
        {
            string DriverPath = Application.StartupPath + "\\" + driverName;
            string kdmapperPath = Application.StartupPath + "\\kdmapper.exe";
            ProcessStartInfo processStart = new ProcessStartInfo();
            processStart.FileName = kdmapperPath;
            processStart.Arguments = DriverPath;
            processStart.WindowStyle = ProcessWindowStyle.Hidden;
            System.Diagnostics.Process.Start(processStart).WaitForExit();
            File.Delete(DriverPath);
            File.Delete(kdmapperPath);

        }
        #region "Driver"
        public unsafe byte[] Read(long targetAddress, int size)
        {
            if (HasValidHandle())
            {
                byte[] buffer = new byte[size];
                fixed (byte* p = buffer)
                {
                    IntPtr ptr = (IntPtr)p;
                    info_t operation = new info_t
                    {
                        pid = ProcessID.Id,
                        address = (IntPtr)targetAddress,
                        value = ptr,
                        size = size
                    };

                    IntPtr operationPointer = (IntPtr)(&operation);

                    bool result = DeviceIoControl(h_driver, ctl_read, operationPointer, Marshal.SizeOf<info_t>(), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
                    return buffer;
                }
            }
            return default;
        }
        public unsafe T Read<T>(long targetAddress) where T : struct
        {
            if (HasValidHandle())
            {
                T buffer = new T();
                TypedReference reference = __makeref(buffer);
                info_t operation = new info_t
                {
                    pid = ProcessID.Id,
                    address = (IntPtr)targetAddress,
                    value = *(IntPtr*)(&reference),
                    size = Marshal.SizeOf<T>()
                };

                IntPtr operationPointer = (IntPtr)(&operation);

                bool result = DeviceIoControl(h_driver, ctl_read, operationPointer, Marshal.SizeOf<info_t>(), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);

                return buffer;
            }
            return default;
        }
        public string ReadString(long targetAddress, int bufferSize)
        {
            return Encoding.Default.GetString(Read(targetAddress, bufferSize));
        }

        public void Write(long targetAddress, byte[] buffer)
        {
            int bufferSize = buffer.Length;
            IntPtr bufferPtr = AllocZeroFilled(bufferSize);
            Marshal.Copy(buffer, 0, bufferPtr, bufferSize);
            if (HasValidHandle())
            {
                info_t operation = new info_t
                {
                    pid = ProcessID.Id,
                    address = (IntPtr)targetAddress,
                    value = bufferPtr,
                    size = bufferSize
                };

                IntPtr operationPointer = CopyStructToMemory(operation);

                bool result = DeviceIoControl(h_driver, ctl_write, operationPointer, Marshal.SizeOf<info_t>(), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
                Marshal.FreeHGlobal(operationPointer);


            }
        }
        public T Write<T>(long targetAddress, T buffer) where T : struct
        {
            IntPtr bufferPtr = CopyStructToMemory<T>(buffer);
            if (HasValidHandle())
            {
                info_t operation = new info_t
                {
                    pid = ProcessID.Id,
                    address = (IntPtr)targetAddress,
                    value = bufferPtr,
                    size = Marshal.SizeOf(typeof(T))
                };

                IntPtr operationPointer = CopyStructToMemory(operation);

                bool result = DeviceIoControl(h_driver, ctl_write, operationPointer, Marshal.SizeOf<info_t>(), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
                Marshal.FreeHGlobal(operationPointer);

                return GetStructFromMemory<T>(bufferPtr);
            }
            return default;
        }

        public bool HasValidHandle() => h_driver != INVALID_HANDLE_VALUE;
        private static uint CTL_CODE(int deviceType, int function, int method, int access) => (uint)(((deviceType) << 16) | ((access) << 14) | ((function) << 2) | (method));
        #endregion
        #region "Driver Structure"

      


        [StructLayout(LayoutKind.Sequential)]
        internal struct info_t
        {
            public int pid;
            public IntPtr address;
            public IntPtr value;
            public int size;
            public IntPtr data;
        }
        public static IntPtr CopyStructToMemory<T>(T obj) where T : struct
        {
            IntPtr unmanagedAddress = AllocEmptyStruct<T>();
            Marshal.StructureToPtr(obj, unmanagedAddress, true);

            return unmanagedAddress;
        }

        public static IntPtr AllocEmptyStruct<T>() where T : struct
        {
            int structSize = Marshal.SizeOf<T>();
            IntPtr structPointer = AllocZeroFilled(structSize);

            return structPointer;
        }

        public static IntPtr AllocZeroFilled(int size)
        {
            IntPtr allocatedPointer = Marshal.AllocHGlobal(size);
            ZeroMemory(allocatedPointer, size);

            return allocatedPointer;
        }

        public static void ZeroMemory(IntPtr pointer, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Marshal.WriteByte(pointer + i, 0x0);
            }
        }

        public static T GetStructFromMemory<T>(IntPtr unmanagedAddress, bool freeMemory = true) where T : struct
        {
            T structObj = Marshal.PtrToStructure<T>(unmanagedAddress);

            if (freeMemory)
            {
                Marshal.FreeHGlobal(unmanagedAddress);
            }
            return structObj;
        }
        #endregion
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateFileA([MarshalAs(UnmanagedType.LPStr)] string filename, [MarshalAs(UnmanagedType.U4)] FileAccess access, [MarshalAs(UnmanagedType.U4)] FileShare share, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes, IntPtr templateFile);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, IntPtr lpBytesReturned, IntPtr lpOverlapped);
    }
}
