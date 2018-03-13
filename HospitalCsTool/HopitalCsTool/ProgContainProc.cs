using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.IO;
using static System.Threading.Thread;

namespace HopitalCsTool
{
   public partial class ProgContainProc : Panel
   {
      #region---<Win32Api>------------------

      [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
           CharSet = CharSet.Unicode, ExactSpelling = true,
           CallingConvention = CallingConvention.StdCall)]
      private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

      [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
      private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

      public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong)
      {
         if (IntPtr.Size == 4)
         {
            return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
         }
         return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
      }
      [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
      public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);
      [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
      public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, int dwNewLong);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

      [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
      private static extern bool PostMessage(IntPtr hwnd, uint Msg, uint wParam, uint lParam);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern IntPtr GetParent(IntPtr hwnd);

      [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
      static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

      private const int SWP_NOOWNERZORDER = 0x200;
      private const int SWP_NOREDRAW = 0x8;
      private const int SWP_NOZORDER = 0x4;
      private const int SWP_SHOWWINDOW = 0x0040;
      private const int WS_EX_MDICHILD = 0x40;
      private const int SWP_FRAMECHANGED = 0x20;
      private const int SWP_NOACTIVATE = 0x10;
      private const int SWP_ASYNCWINDOWPOS = 0x4000;
      private const int SWP_NOMOVE = 0x2;
      private const int SWP_NOSIZE = 0x1;
      private const int GWL_STYLE = (-16);
      private const int WS_VISIBLE = 0x10000000;
      private const int WM_CLOSE = 0x10;
      private const int WS_CHILD = 0x40000000;

      private const int SW_HIDE = 0;
      private const int SW_SHOWNORMAL = 1;
      private const int SW_NORMAL = 1;
      private const int SW_SHOWMINIMIZED = 2;
      private const int SW_SHOWMAXIMIZED = 3;
      private const int SW_MAXIMIZE = 3;
      private const int SW_SHOWNOACTIVATE = 4;
      private const int SW_SHOW = 5;
      private const int SW_MINIMIZE = 6;
      private const int SW_SHOWMINNOACTIVE = 7;
      private const int SW_SHOWNA = 8;
      private const int SW_RESTORE = 9;
      private const int SW_SHOWDEFAULT = 10;
      private const int SW_MAX = 10;

      private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
      private const int PROCESS_VM_READ = 0x0010;
      private const int PROCESS_VM_WRITE = 0x0020;

      #endregion

      Action<object, EventArgs> appldleAction = null;
      EventHandler appIdleEvent = null;

      public ProgContainProc()
      {
         InitializeComponent();
         appldleAction = new Action<object, EventArgs>(Application_Idle);
         appIdleEvent = new EventHandler(appldleAction);
      }

      public ProgContainProc(IContainer container)
      {
         container.Add(this);
         InitializeComponent();
         appldleAction = new Action<object, EventArgs>(Application_Idle);
         appIdleEvent = new EventHandler(appldleAction);
      }

      //실행할 프로그램 절대경로
      private string m_AppFilename = Properties.Settings.Default.cv3path;

      //프로세스실행 체크
      public bool IsStarted { get { return (this.m_AppProcess != null); } }

      public void Start()
      {
         if (m_AppProcess != null)
         {
            Stop();
         }

         try
         {
            Sleep(2000);
            ProcessStartInfo info = new ProcessStartInfo(m_AppFilename);
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Minimized;
            m_AppProcess = Process.Start(info);
            m_AppProcess.WaitForInputIdle();
            Application.Idle += appIdleEvent;
         }
         catch (Exception ex)
         {
            //MessageBox.Show(string.Format("프로그램로드불가 / {0}", ex.Message.ToString()));
            
            if (m_AppProcess != null)
            {
               if (!m_AppProcess.HasExited) m_AppProcess.Kill();
               m_AppProcess = null;
            }
         }
      }

      void Application_Idle(object sender, EventArgs e)
      {
         if (m_AppProcess == null || m_AppProcess.HasExited)
         {
            m_AppProcess = null;
            Application.Idle -= appIdleEvent;
            return;
         }

         if (m_AppProcess.MainWindowHandle == IntPtr.Zero) return;

         Application.Idle -= appIdleEvent;
         EmbedProcess(m_AppProcess,this);
      }

      void m_AppProcess_Exited(object sender, EventArgs e)
      {
         m_AppProcess = null;
      }

      public void Stop()
      {
         if (m_AppProcess != null)
         {
            try
            {
               if (!m_AppProcess.HasExited)
                  m_AppProcess.Kill();
            }
            catch (Exception)
            {
            }
            m_AppProcess = null;
         }
      }

      protected override void OnHandleDestroyed(EventArgs e)
      {
         Stop();
         base.OnHandleDestroyed(e);
      }

      protected override void OnResize(EventArgs eventargs)
      {
         if (m_AppProcess != null)
         {
            MoveWindow(m_AppProcess.MainWindowHandle, 0, 0, this.Width, this.Height, true);
         }
         base.OnResize(eventargs);
      }

      protected override void OnSizeChanged(EventArgs e)
      {
         Invalidate();
         base.OnSizeChanged(e);
      }

      Process m_AppProcess = null;
      public Process AppProcess
      {
         get { return this.m_AppProcess; }
         set { this.m_AppProcess = value; }
      }

      public void EmbedAgain()
      {
         EmbedProcess(m_AppProcess, this);
         MessageBox.Show("완료");
      }

      //컨트롤에 포함
      private void EmbedProcess(Process app, Control control)
      {
         // Get the main handle
         if (app == null || app.MainWindowHandle == IntPtr.Zero || control == null) return;
         try
         {
            // Put it into this form
            SetParent(app.MainWindowHandle, control.Handle);
         }
         catch (Exception)
         { }
         try
         {
            // Remove border and whatnot               
            SetWindowLong(new HandleRef(this, app.MainWindowHandle), GWL_STYLE, WS_VISIBLE);
         }
         catch (Exception)
         { }
         try
         {
            // Move the window to overlay it on this window
            MoveWindow(app.MainWindowHandle, 0, 0, control.Width, control.Height, true);
         }
         catch (Exception)
         { }
      }
   }
}
