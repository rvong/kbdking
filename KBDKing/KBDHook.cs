using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KBDKing
{
    class KBDHook
    {
        public delegate int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        
        //public event System.Windows.Forms.KeyEventHandler KeyDown;
        //public event System.Windows.Forms.KeyEventHandler KeyUp;
        //	KeyUp(this, kea);

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;      // Virtual-key code, 1-254
            public int scanCode;    // Hardware scan code
            public int flags;       // LLHKF flags
            public int time;        // Timestamp
            public int dwExtraInfo; // Additional info
		}

        const int HC_ACTION = 0;

		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN     = 0x100;
		const int WM_KEYUP       = 0x101;
		const int WM_SYSKEYDOWN  = 0x104;
		const int WM_SYSKEYUP    = 0x105;

        const int LLKHF_EXTENDED = 0x1;   // Extended-key flag
        const int LLKHF_INJECTED = 0x10;  // Event-injected flag
        const int LLKHF_ALTDOWN  = 0x20;  // Context code  
        const int LLKHF_UP       = 0x80;  // Transition-state flag

        IntPtr kbdHook = IntPtr.Zero;

		public KBDHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            kbdHook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
        }

        ~KBDHook() { UnhookWindowsHookEx(kbdHook); }

        public int hookProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            if (nCode >= HC_ACTION)
            {
                wrt(ref lParam);
            }
            return CallNextHookEx(kbdHook, nCode, wParam, ref lParam);
		}

        public void wrt(ref KBDLLHOOKSTRUCT lParam)
        {
            Debug.WriteLine("--------");
            Debug.WriteLine("vkCode: " + lParam.vkCode);
            Debug.WriteLine("Scan: " + lParam.scanCode);
            Debug.WriteLine("Time: " + lParam.time);
            Debug.WriteLine("Flags: " + lParam.flags);
            Debug.WriteLine("Flags Extended: " + (lParam.flags == LLKHF_EXTENDED));
            Debug.WriteLine("Flags Injected: " + (lParam.flags == LLKHF_INJECTED));
            Debug.WriteLine("Flags Alt: " + (lParam.flags == LLKHF_ALTDOWN));
            Debug.WriteLine("Flags Trans: " + (lParam.flags == LLKHF_UP));


            Debug.WriteLine("ExtraInfo: " + lParam.dwExtraInfo);

        }

		[DllImport("user32.dll")] static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hMod, uint threadId);
		[DllImport("user32.dll")] static extern bool UnhookWindowsHookEx(IntPtr hHook);
		[DllImport("user32.dll")] static extern int CallNextHookEx(IntPtr hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
		[DllImport("kernel32.dll")] static extern IntPtr LoadLibrary(string fileName);
    }
}
