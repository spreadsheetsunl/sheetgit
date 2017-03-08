using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace GitExcelAddIn
{
    public class VirtualMouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);
        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }


        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        [Flags]
        enum KeyboardEventFlags : uint
        {
            KEYEVENTF_EXTENDEDKEY = 0x0001,
            KEYEVENTF_KEYUP = 0x0002,
            KEYEVENTF_SCANCODE = 0x0008,
            KEYEVENTF_UNICODE = 0x0004
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        public static void ClickLeftMouseButton()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }
        public static void ClickRightMouseButton()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }
        public static void sendKeyboard(string key)
        {
            INPUT keybInput = new INPUT();
            keybInput.type = SendInputEventType.InputKeyboard;
            keybInput.mkhi.ki.time = 0;
            keybInput.mkhi.ki.dwFlags = (uint) KeyboardEventFlags.KEYEVENTF_UNICODE;
            keybInput.mkhi.ki.wScan = new ushort();
            keybInput.mkhi.ki.wVk = 0;
            keybInput.mkhi.ki.dwExtraInfo = new IntPtr(0);
            SendInput(1, ref keybInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }
        public static void MoveMouse(int x, int y, int steps)
        {
            INPUT mouseMoveInput = new INPUT();
            mouseMoveInput.type = SendInputEventType.InputMouse;
            mouseMoveInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseMoveInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseMoveInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_ABSOLUTE | MouseEventFlags.MOUSEEVENTF_MOVE;

            VirtualMouse.POINT p = new VirtualMouse.POINT();
            p.x = x;
            p.y = y;
            VirtualMouse.LinearSmoothMove(p, steps);

            /*VirtualMouse.SetCursorPos(x, y);*/

            /*SendInput(1, ref mouseMoveInput, Marshal.SizeOf(new INPUT()));*/
        }
        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        static int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        static int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        public static void LinearSmoothMove(POINT newPosition, int steps)
        {
            POINT start;
            VirtualMouse.GetCursorPos(out start);
            // Find the slope of the line segment defined by start and newPosition
            POINT slope = new POINT();
            slope.x = newPosition.x - start.x;
            slope.y = newPosition.y - start.y;
            //700/x=35 700/35=x

            int xa = Math.Abs(slope.x / 30);
            int xb = Math.Abs(slope.y / 30);
            xa = Math.Max(xa, xb);

            // Divide by the number of steps
            slope.x = slope.x / xa;
            slope.y = slope.y / xa;

            var iterPoint = new POINT();
            iterPoint.x = start.x;
            iterPoint.y = start.y;
            // Move the mouse to each iterative point.
            for (int i = 0; i < xa; i++)
            {

                iterPoint.x = iterPoint.x + slope.x;
                iterPoint.y = iterPoint.y + slope.y;
                VirtualMouse.SetCursorPos(iterPoint.x, iterPoint.y);
                Thread.Sleep(100);
            }

            // Move the mouse to the final destination.
            SetCursorPos(newPosition.x, newPosition.y);
            Thread.Sleep(300);
        }
    }
}
