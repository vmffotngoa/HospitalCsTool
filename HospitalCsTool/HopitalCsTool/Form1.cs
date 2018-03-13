using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.Win32;
using static System.Threading.Thread;
using CefSharp;
using CefSharp.WinForms;

namespace HopitalCsTool
{
   public partial class frmCstoolmain : Form
   {
      public frmCstoolmain()
      {
         InitializeComponent();
      #region---<디버깅용 좌표타이머>------------------

         trDebug.Stop();
         trDebug.Interval = 100;
         trDebug.Enabled = true;
         trDebug.Start();

      #endregion
      }
      #region---<전역>------------------

      int Mousx = 0;
      int Mousy = 0;
      int Frmx = 0;
      int Frmy = 0;
      int Sizx = 0;
      int Sizy = 0;
      int iProccnt = 0;
      int iDutyline = 0;
      int iDelcnt;
      int handle;
      int handle2;
      int[] iDelarrayX = { 115, 310, 105, 335 };
      int[] iDelarrayY = { 820, 730, 515, 675 };
      string sNow = "";
      string downAddress = "";
      string zolvueexe = Properties.Settings.Default.zolvuezippath + "ZOLVUE_Installation.exe";
      string sUrl = "";
      bool nowDownloading = false;
      bool bOldurl = false;
      bool bgwsts = false;
      
      WebClient cWebdown = new WebClient();
      ProgContainProc ac1 = new ProgContainProc();
      Panel pnOlddel = new Panel();
      Panel pnChromebr = new Panel();
      TextBox tbOlddel = new TextBox();
      Button btnOlddel = new Button();
      Button btnOlddelstop = new Button();
      Label lblOlddel = new Label();

      public ChromiumWebBrowser cwbBrowser;

      #endregion
      #region---<Win32Api>------------------

      [DllImport("user32")]
      public static extern int FindWindow(string lpClassName, string lpWindowName);

      [DllImport("user32")]
      public static extern uint FindWindowEx(uint hWnd1, uint hWnd2, string lpsz1, string lpsz2);

      [DllImport("user32")]
      public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

      [DllImport("user32")]
      private static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);

      [DllImport("user32")]
      public static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);
      const int WM_LBUTTONDOWN = 0x0201;
      const int WM_LBUTTONUP = 0x0202;
      const int BM_CLICK = 0x00F5;

      [DllImport("user32")]
      public static extern int SendMessage(int hwnd, int wMsg, string wParam, int lParam);
      const int WM_CHAR = 0x0102;

      [DllImport("user32")]
      public static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);

      [DllImport("user32")]
      private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

      [DllImport("user32")]
      public static extern int PostMessage(int hwnd, int wMsg, int wParam, int lParam);

      [DllImport("user32")]
      private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

      [DllImport("user32.dll")]
      static extern void keybd_event(byte vk, byte scan, int flags, ref int extrainfo);

      [DllImport("user32.dll")]
      static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
      private const uint MOUSEMOVE = 0x0001;   // 마우스 이동
      private const uint ABSOLUTEMOVE = 0x8000;   // 전역 위치
      private const uint LBUTTONDOWN = 0x0002;   // 왼쪽 마우스 버튼 눌림
      private const uint LBUTTONUP = 0x0004;   // 왼쪽 마우스 버튼 떼어짐
      private const uint LEFTDOWN = 0x00000002;
      private const uint LEFTUP = 0x00000004;
      private const uint LEFTCLICK = 0x203;
      private const uint MIDDLEDOWN = 0x00000020;
      private const uint MIDDLEUP = 0x00000040;
      private const uint MOVE = 0x00000001;
      private const uint ABSOLUTE = 0x00008000;
      private const uint RIGHTDOWN = 0x00000008;
      private const uint RIGHTUP = 0x00000010;
      private const uint RIGHTCLICK = 0x206;
      private const uint MOUSE_WHEEL = 0x00000800;

      [DllImport("user32.dll")]
      public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
      private const int MOUSEEVENTF_LEFTDOWN = 0x02;
      private const int MOUSEEVENTF_LEFTUP = 0x04;

      [DllImport("User32", EntryPoint = "SetCursorPos")]
      public static extern void SetCursorPos(int x, int y);

      //[DllImport("user32.dll")]
      //internal static extern bool GetWindowPlacement(int hWnd, ref WINDOWPLACEMENT lpwndpl);
      //
      //internal struct WINDOWPLACEMENT
      //{
      //   public int length;
      //   public int flags;
      //   public Point ptMinPosition;
      //   public Point ptMaxPosition;
      //   public Rectangle rcNormalPosition;
      //}

      internal enum ShowWindowCommands : int
      {
         Hide = 0,
         Normal = 1,
         Minimized = 2,
         Maximized = 3,
      }

      internal enum WNDSTATE : int
      {
         SW_HIDE = 0,
         SW_SHOWNORMAL = 1,
         SW_NORMAL = 1,
         SW_SHOWMINIMIZED = 2,
         SW_MAXIMIZE = 3,
         SW_SHOWNOACTIVATE = 4,
         SW_SHOW = 5,
         SW_MINIMIZE = 6,
         SW_SHOWMINNOACTIVE = 7,
         SW_SHOWNA = 8,
         SW_RESTORE = 9,
         SW_SHOWDEFAULT = 10,
         SW_MAX = 10
      }

      #endregion
      #region---<크롬 웹작업>-----------------

      private void chromebrSet()
      {
         this.Size = new Size(1000, 1200);
         this.Controls.Add(pnChromebr);
         pnChromebr.Location = new Point(25, 40);
         pnChromebr.Size = new Size(700, 1000);
      }

      private void DelStop(object sender, MouseEventArgs e)
      {
         bgw.CancelAsync();
      }

      public void Initcwbbr()
      {
         if (bOldurl == false)
         {
            Cef.Initialize(new CefSettings());
            cwbBrowser = new ChromiumWebBrowser(sUrl);
            pnChromebr.Controls.Add(cwbBrowser);
            cwbBrowser.Dock = DockStyle.Fill;
            bOldurl = true;
         }
         else
         {
            pnChromebr.Controls.Remove(cwbBrowser);
            cwbBrowser = new ChromiumWebBrowser(sUrl);
            pnChromebr.Controls.Add(cwbBrowser);
            cwbBrowser.Dock = DockStyle.Fill;
         }
      }

      private void NccDelwork(object sender, MouseEventArgs e)
      {
         Frmx = this.Location.X;
         Frmy = this.Location.Y;

         if (tbOlddel.Text == null || tbOlddel.Text == "")
         {
            MessageBox.Show("횟수를 입력하세요");
            return;
         }

         iDelcnt = Convert.ToInt32(tbOlddel.Text, 10);
         bgw.RunWorkerAsync();
      }

      private void runcmd(string sTag, params object[] oArgs)
      {
         switch (sTag.ToUpper())
         {
            case "QNA":
               {
                  //webBrowser1.Navigate("https://humic.co.kr:451/03_service/service01.htm");
                  sUrl = "https://humic.co.kr:451/03_service/service01.htm";
                  Initcwbbr();
                  break;
               }
            case "WAITLIST":
               {
                  sUrl = "https://humic.co.kr:451/itplus/login.htm";
                  Initcwbbr();
                  break;
               }
            case "MWAITLIST":
               {
                  sUrl = "https://admin.humic.co.kr/common";
                  Initcwbbr();
                  break;
               }
            case "ID":
               {
                  Clipboard.SetText("admin");
                  break;
               }
            case "PW":
               {
                  Clipboard.SetText("humic2013");
                  break;
               }
            case "MID":
               {
                  Clipboard.SetText("csh");
                  //admin
                  break;
               }
            case "MPW":
               {
                  Clipboard.SetText("qwer1234");
                  //Clipboard.SetText("admin1324");
                  break;
               }
            case "CLOSE":
               {
                  pnChromebr.Controls.Remove(cwbBrowser);
                  this.Size = new Size(666, 333);
                  break;
               }
         }
      }

      #endregion
      #region---<백그라운드작업>-----------------
      private void bgw_DoWork(object sender, DoWorkEventArgs e)
      {
         switch (sNow)
         {
            #region---<의뢰취소부>-----------------

            case "OLDDEL":
               {
                  int k = 0;
                  while (k < iDelcnt)
                  {
                     for (int i = 0; i < 4; i++)
                     {
                        SetCursorPos(iDelarrayX[i] + Frmx, iDelarrayY[i] + Frmy);
                        mouse_event(LBUTTONDOWN, iDelarrayX[i] + Frmx, iDelarrayY[i] + Frmy, 0, 0);
                        mouse_event(LBUTTONUP, iDelarrayX[i] + Frmx, iDelarrayY[i] + Frmy, 0, 0);
                        Sleep(1500);
                     }
                     k++;
                     //lblOlddel.Text = k.ToString();
                  }
                  break;
               }


            #endregion
            #region---<CV3설치부>-----------------

            case "CV3":
               {
                  Sleep(2000);
                  string sCv3Setid = "";
                  string sCv3Setpw = "";

                  while (iProccnt < 2)
                  {
                     if (bgwsts == false)
                     {
                        Sleep(1500);
                        bgwsts = true;
                        SetCursorPos(this.Location.X + 45, this.Location.Y + 75);
                        mouse_event(LBUTTONDOWN, this.Location.X + 45, this.Location.Y + 75, 0, 0);
                        mouse_event(LBUTTONUP, this.Location.X + 45, this.Location.Y + 75, 0, 0);
                        mouse_event(LBUTTONDOWN, this.Location.X + 45, this.Location.Y + 75, 0, 0);
                        mouse_event(LBUTTONUP, this.Location.X + 45, this.Location.Y + 75, 0, 0);
                        iProccnt++;
                        bgwsts = false;
                        Sleep(1500);
                     }
                     else
                     {
                        Sleep(1000);
                     }
                  }

                  while (iProccnt < 3)
                  {
                     if (bgwsts == false)
                     {
                        Sleep(1000);
                        bgwsts = true;

                        for (int i = 0; i < 15; i++)
                        {
                           if (File.Exists(@"C:\Compact View III\CV3Setup.exe"))
                           {
                              break;
                           }
                           else
                           {
                              Sleep(1500);
                           }
                        }

                        iProccnt++;
                        bgwsts = false;
                        Sleep(1500);
                        break;
                     }
                     else
                     {
                        Sleep(1000);
                     }
                  }

                  Sleep(1500);

                  while (iProccnt < 4)
                  {
                     if (bgwsts == false)
                     {
                        Process[] found = Process.GetProcessesByName("CV3Setup");

                        Sleep(1000);
                        bgwsts = true;

                        sCv3Setid = "outside";
                        sCv3Setpw = "pacs";

                        Process vpnproc = found[0];
                        IntPtr edit1 = FindWindowEx(vpnproc.MainWindowHandle, IntPtr.Zero, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit2 = FindWindowEx(vpnproc.MainWindowHandle, edit1, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit3 = FindWindowEx(vpnproc.MainWindowHandle, edit2, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit4 = FindWindowEx(vpnproc.MainWindowHandle, edit3, "WindowsForms10.EDIT.app.0.378734a", null);

                        SendMessage(edit4, 0xC, IntPtr.Zero, sCv3Setid);
                        SendMessage(edit3, 0xC, IntPtr.Zero, sCv3Setpw);
                        PostMessage(edit4, 0x100, new IntPtr(0x0D), null);
                        PostMessage(edit3, 0x101, new IntPtr(0x0D), null);

                        iProccnt++;
                        bgwsts = false;
                        Sleep(1500);
                        break;
                     }
                     else
                     {
                        Sleep(1000);
                     }
                  }

                  while (iProccnt < 5)
                  {
                     if (bgwsts == false)
                     {
                        Sleep(1000);
                        bgwsts = true;

                        handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");

                        if (handle > 0)
                        {
                           handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                           handle2 = FindWindowEx(handle, 0, "Button", "&Next >");
                           SendMessage(handle2, BM_CLICK, 0, 1);
                           Sleep(1500);

                           handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                           handle2 = FindWindowEx(handle, 0, "Static", "Program Maintenance");

                           if (handle2 > 0)
                           {
                              handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                              handle2 = FindWindowEx(handle, 0, "Button", "");
                              handle2 = FindWindowEx(handle2, 0, "Button", "Re&pair");
                              SendMessage(handle2, BM_CLICK, 0, 1);
                              Sleep(1000);

                              handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                              handle2 = FindWindowEx(handle, 0, "Button", "&Next >");
                              SendMessage(handle2, BM_CLICK, 0, 1);
                              Sleep(1000);
                           }
                           else
                           {
                              handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                              handle2 = FindWindowEx(handle, 0, "Button", "&Next >");
                              SendMessage(handle2, BM_CLICK, 0, 1);
                              Sleep(1000);
                           }

                           handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                           handle2 = FindWindowEx(handle, 0, "Button", "&Install");
                           SendMessage(handle2, BM_CLICK, 0, 1);
                           Sleep(1000);

                           handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                           handle2 = FindWindowEx(handle, 0, "Static", "Wizard Completed");
                           while (handle2 > 0)
                           {
                              handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                              handle2 = FindWindowEx(handle, 0, "Static", "Wizard Completed");
                              Sleep(2000);

                              if (handle2 > 0)
                              {
                                 handle = FindWindow(null, "MySQL Connector/ODBC 5.1 - Setup Wizard");
                                 handle2 = FindWindowEx(handle, 0, "Button", "&Finish");
                                 SendMessage(handle2, BM_CLICK, 0, 1);
                                 iProccnt++;
                                 break;
                              }
                           }
                        }

                        iProccnt++;
                        bgwsts = false;
                        Sleep(1500);
                        break;
                     }
                     else
                     {
                        Sleep(1000);
                     }

                  }
                  #region---<사용보류>------------------

                  /* 무조건 본원을 제외한 모든 판독전문의는 OSPACS사용
                  while (iProccnt < 6)
                  {
                     if (bgwsts == false)
                     {
                        Sleep(1000);
                        bgwsts = true;

                        string sCv3set = "C:\\Compact View III\\CV3Setup.exe";
                        sCv3Setid = "";
                        sCv3Setpw = "test";

                        Process.Start(sCv3set);
                        if (Directory.Exists("C:\\windows\\syswow64"))
                        {
                           sCv3Setid = "win64";
                        }
                        else
                        {
                           sCv3Setid = "win32";
                        }

                        Process.Start(sCv3set);
                        Sleep(1500);

                        Process[] found = Process.GetProcessesByName("CV3Setup");

                        while (found == null || found?.Length == 0)
                        {
                           found = Process.GetProcessesByName("CV3Setup");
                           Sleep(1500);
                        }
                        Process vpnproc = found[0];
                        IntPtr edit1 = FindWindowEx(vpnproc.MainWindowHandle, IntPtr.Zero, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit2 = FindWindowEx(vpnproc.MainWindowHandle, edit1, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit3 = FindWindowEx(vpnproc.MainWindowHandle, edit2, "WindowsForms10.EDIT.app.0.378734a", null);
                        IntPtr edit4 = FindWindowEx(vpnproc.MainWindowHandle, edit3, "WindowsForms10.EDIT.app.0.378734a", null);

                        SendMessage(edit4, 0xC, IntPtr.Zero, sCv3Setid);
                        SendMessage(edit3, 0xC, IntPtr.Zero, sCv3Setpw);
                        PostMessage(edit4, 0x100, new IntPtr(0x0D), null);
                        PostMessage(edit3, 0x101, new IntPtr(0x0D), null);

                        iProccnt++;
                        bgwsts = false;
                        Sleep(1500);
                        break;
                     }
                     else
                     {
                        Sleep(1000);
                     }
                  }
                  */

                  #endregion
                  break;
               }

            #endregion
            #region---<판독스케쥴관리부>-----------------

            case "COD":
               {
                  if (iDutyline == 1 || iDutyline == 6 || iDutyline == 11 || iDutyline == 16 || iDutyline == 21)
                  {

                  }
                  else if (iDutyline == 2 || iDutyline ==7 || iDutyline ==12 || iDutyline == 17 || iDutyline == 22)
                  {

                  }
                  else if (iDutyline == 3 || iDutyline ==8 || iDutyline ==13 || iDutyline == 18 || iDutyline == 23)
                  {

                  }
                  else if (iDutyline == 4 || iDutyline ==9 || iDutyline ==14 || iDutyline == 19 || iDutyline == 24)
                  {

                  }
                  break;
               }


               #endregion
         }
      }
      #endregion
      #region---<폼컨트롤러 이벤트>-----------------

      private void btnGrpProgram_Click(object sender, EventArgs e)
      {
         this.pnThreepacs.Enabled = true;
         this.pnThreepacs.Visible = true;
      }

      private void btnHomecheck_Click(object sender, EventArgs e)
      {
         //webBrowser1.Visible = true;
         cmsPagesel.Show(Cursor.Position);
      }

      private void tsmiHumanhome_Click(object sender, EventArgs e)
      {
         chromebrSet();

         ToolStripItem itemsel = sender as ToolStripItem;

         if (itemsel != null && itemsel.Tag != null)
         {
            runcmd(itemsel.Tag.ToString().Trim());
         }
      }
      #region---<판독프로그램 설치>-----------------

      private void btnThreeset_Click(object sender, EventArgs e)
      {
         this.pnThreepacs.Visible = false;
         if (this.nowDownloading)
         {
            MessageBox.Show("이미 다운로드가 진행 중입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         else
         {
            if (Directory.Exists(Properties.Settings.Default.zolvuezippath) == true)
            {
               DirectoryInfo deletefolder = new DirectoryInfo(Properties.Settings.Default.zolvuezippath);
               FileInfo[] deletefiles = deletefolder.GetFiles("*.*", SearchOption.AllDirectories);

               foreach (FileInfo deletefile in deletefiles)
               {
                  deletefile.Attributes = FileAttributes.Normal;
               }
               Directory.Delete(Properties.Settings.Default.zolvuezippath, true);
            }
            Directory.CreateDirectory(Properties.Settings.Default.zolvuezippath);


            #region---<ZOLVUE설치>-----------------
            if (this.cbZolvue.Checked)
            {
               if (Directory.Exists(@"C:\HealthHub\ZOLVUE"))
               {
                  string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HealthHub\Updater";
                  FileInfo[] files = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
                  foreach (FileInfo info3 in files)
                  {
                     info3.Attributes = FileAttributes.Normal;
                  }
                  Directory.Delete(path, true);
                  Process process = new Process();
                  ProcessStartInfo info2 = new ProcessStartInfo
                  {
                     FileName = "CMD.exe",
                     WorkingDirectory = @"C:\HealthHub\ZOLVUE",
                     WindowStyle = ProcessWindowStyle.Hidden,
                     CreateNoWindow = true,
                     UseShellExecute = false,
                     RedirectStandardInput = true,
                     RedirectStandardOutput = true,
                     RedirectStandardError = true
                  };
                  process.EnableRaisingEvents = false;
                  process.StartInfo = info2;
                  process.Start();
                  string str2 = "ZVLauncher.exe";
                  process.StandardInput.WriteLine(str2);
                  process.StandardInput.Close();
               }
               else
               {
                  downAddress = @"http://121.166.70.170/zolvue.zip";
                  String fileName = String.Format(Properties.Settings.Default.zolvuezippath + "{0}", Path.GetFileName(downAddress));

                  try
                  {
                     cWebdown.DownloadFileAsync(new Uri(downAddress), fileName);
                     nowDownloading = true;
                     btnThreeset.Enabled = false;
                  }
                  catch (Exception ex)
                  {
                     MessageBox.Show(ex.Message, ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  }

                  Sleep(5000);

                  ZipFile.ExtractToDirectory(Properties.Settings.Default.zolvuezippath + "zolvue.zip", Properties.Settings.Default.zolvuezippath);

                  //졸뷰 설치프로그램 실행
                  var process = new Process();
                  var processStartInfo = new ProcessStartInfo
                  {
                     FileName = "CMD.exe",
                     WorkingDirectory = Properties.Settings.Default.zolvuezippath,
                     WindowStyle = ProcessWindowStyle.Hidden,
                     CreateNoWindow = true,
                     UseShellExecute = false,
                     RedirectStandardInput = true,
                     RedirectStandardOutput = true,
                     RedirectStandardError = true
                  };

                  process.EnableRaisingEvents = false;
                  process.StartInfo = processStartInfo;
                  process.Start();

                  string zolvuleinstall = "ZOLVUE_Installation.exe";

                  process.StandardInput.WriteLine(zolvuleinstall);
                  process.StandardInput.Close();

                  Sleep(2000);

                  int p = 0;

                  while (p < 3)
                  {
                     handle = FindWindow(null, "ZOLVUE");
                     handle2 = FindWindowEx(handle, 0, "Button", "다음(&N) >");
                     SendMessage(handle2, BM_CLICK, 0, 1);
                     p++;
                     Sleep(1000);
                  }

                  p = 0;
                  while (p < 75)
                  {
                     handle = FindWindow(null, "ZOLVUE");
                     handle2 = FindWindowEx(handle, 0, "Static", "설치 완료");
                     Sleep(2000);

                     if (handle2 > 0)
                     {
                        handle = FindWindow(null, "ZOLVUE");
                        handle2 = FindWindowEx(handle, 0, "Button", "닫기(&C)");
                        SendMessage(handle2, BM_CLICK, 0, 1);
                        break;
                     }
                     p++;
                  }
               }
            }

            #endregion
            #region---<CV3설치>------------------

            if (cbCV3.Checked == true)
            {
               sNow = "CV3";
               downAddress = @"http://www.optimumsolution.co.kr/software/OptimumInstaller.exe";

               try
               {
                  cWebdown.DownloadFileAsync(new Uri(downAddress), Properties.Settings.Default.cv3path);
                  nowDownloading = true;
                  btnThreeset.Enabled = false;
               }
               catch (Exception ex)
               {
                  MessageBox.Show(ex.Message, ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }

               this.Controls.Add(ac1);
               Sleep(1500);
               ac1.Location = new Point(25, 35);
               ac1.Size = new Size(500, 500);
               Sleep(1500);
               ac1.Start();

               iProccnt = 1;
               Sleep(1500);
               bgw.RunWorkerAsync();
            }

            #endregion
            #region---<온팩스설치>------------------

            if (cbOnpacs.Checked == true)
            {
               string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
               string shotcutdetail = Properties.Settings.Default.onpacsshotcut;
               File.AppendAllText(deskDir + "\\ONPACS.bat", shotcutdetail, Encoding.Default);

               RegistryKey onpacsreg;
               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Internet Explorer").CreateSubKey("New Windows");
               onpacsreg.SetValue("PopupMgr",00000000,RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Internet Settings").CreateSubKey("ZoneMap").CreateSubKey("Domains").CreateSubKey("onpacs.com");
               onpacsreg.SetValue("http", 2, RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Internet Settings").CreateSubKey("ZoneMap").CreateSubKey("Domains").CreateSubKey("onpacs.com");
               onpacsreg.SetValue("https", 2, RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Internet Settings").CreateSubKey("ZoneMap").CreateSubKey("Domains").CreateSubKey("onpacs.com").CreateSubKey("www");
               onpacsreg.SetValue("http", 2, RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Internet Settings").CreateSubKey("ZoneMap").CreateSubKey("Domains").CreateSubKey("onpacs.com").CreateSubKey("www");
               onpacsreg.SetValue("https", 2, RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Internet Explorer").CreateSubKey("BrowserEmulation").CreateSubKey("ClearableListData");
               onpacsreg.SetValue("UserFilter", new byte[] { 65, 31, 0, 0, 83, 8, 173, 186, 1, 0, 0, 0, 50, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 12, 0, 0, 0, 247, 158, 115, 85, 246, 183, 210, 1, 1, 0, 0, 0, 10, 0, 111, 0, 110, 0, 112, 0, 97, 0, 99, 0, 115, 0, 46, 0, 99, 0, 111, 0, 109, 0 }, RegistryValueKind.Binary);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Internet Explorer").CreateSubKey("BrowserEmulation");
               onpacsreg.SetValue("MSCompatibilityMode", 1, RegistryValueKind.DWord);

               onpacsreg = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Internet Settings").CreateSubKey("Zones").CreateSubKey("2");
               onpacsreg.SetValue("2001", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2004", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("DisplayName", "신뢰할 수 있는 사이트");
               onpacsreg.SetValue("PMDisplayName", "Trusted sites [Protected Mode]");
               onpacsreg.SetValue("Description", "이 영역에는 사용자 컴퓨터나 데이터를 손상시키지 않을 것으로 신뢰되는 웹 사이트가 포함됩니다.");
               onpacsreg.SetValue("Icon", "inetcpl.cpl#00004480");
               onpacsreg.SetValue("LowIcon", "inetcpl.cpl#005424");
               onpacsreg.SetValue("Flags", 67, RegistryValueKind.DWord);  //onpacsreg.SetValue("Flags", 71, RegistryValueKind.DWord); https 검증 여부 체크 시 
               onpacsreg.SetValue("1200", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1400", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1001", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1004", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1201", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1206", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("1207", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1208", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1209", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("120A", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1402", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1405", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1406", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("1407", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("1408", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1409", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("140A", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1601", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1604", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1605", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1606", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1607", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("1608", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1609", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("160A", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("160B", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1802", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1803", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1804", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("1809", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1812", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A00", 131072, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A02", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A03", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A04", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A05", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A06", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1C00", 65536, RegistryValueKind.DWord);
               onpacsreg.SetValue("2000", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2005", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2007", 65536, RegistryValueKind.DWord);
               onpacsreg.SetValue("2100", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2101", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2102", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2103", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2104", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2105", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2106", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2107", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2108", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2200", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2201", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2300", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("2301", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2302", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2400", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2401", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2402", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2600", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2700", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2701", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2702", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2703", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2704", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2708", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2709", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("270B", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("270C", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("270D", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("2500", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("2707", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1806", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("120C", 0, RegistryValueKind.DWord);
               onpacsreg.SetValue("1A10", 1, RegistryValueKind.DWord);
               onpacsreg.SetValue("120B", 3, RegistryValueKind.DWord);
               onpacsreg.SetValue("140C", 0, RegistryValueKind.DWord);
            }

            #endregion
            nowDownloading = false;
            btnThreeset.Enabled = true;
         }
      }

      #endregion
      #region---<라드넷 의뢰취소 자동화>-----------------

      private void btnNccdel_Click(object sender, EventArgs e)
      {
         sNow = "OLDDEL";
         chromebrSet();
         pnChromebr.Controls.Add(pnOlddel);
         pnOlddel.Controls.Add(tbOlddel);
         pnOlddel.Controls.Add(btnOlddel);
         pnOlddel.Controls.Add(btnOlddelstop);
         pnOlddel.Controls.Add(lblOlddel);
         pnOlddel.Height = 25;
         pnOlddel.Dock = DockStyle.Top;
         tbOlddel.Dock = DockStyle.Right;
         btnOlddel.Text = "작업시작";
         btnOlddel.MouseUp += new MouseEventHandler(this.NccDelwork);
         btnOlddel.Dock = DockStyle.Right;
         btnOlddelstop.Text = "작업중지";
         btnOlddelstop.MouseUp += new MouseEventHandler(this.DelStop);
         btnOlddelstop.Dock = DockStyle.Right;
         lblOlddel.Text = "작업횟수";
         btnOlddelstop.Dock = DockStyle.Right;

         sUrl = "https://www.radnet.kr/login/page";
         Initcwbbr();
      }

      #endregion
      #region---<ncc vpn갱신 메일보내는 기능>-----------------

      private void btnNccgmail_Click(object sender, EventArgs e)
      {
         if (MessageBox.Show("전송하시겠습니까?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
         {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 0x24b)
            {
               EnableSsl = true
            };
            MailMessage message = new MailMessage();
            client.Credentials = new NetworkCredential(Properties.Settings.Default.gmailid, Properties.Settings.Default.gmailpw);
            message.From = new MailAddress(Properties.Settings.Default.gmailid);
            message.To.Add("hh-all@radnet.kr");
            message.To.Add("cio@ncc.re.kr");
            message.Subject = "VPN 연결 갱신 요청드립니다.";
            message.Body = "안녕하십니까, 헬스허브(휴먼영상의학센터)의 천승훈입니다. \r\n 'os1005'계정과 'os1006'계정의갱신을 요청합니다. \r\n 감사합니다.";
            if (MessageBox.Show("파일을 첨부하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
               MessageBox.Show("'파일1'을 선택하세요");
               OpenFileDialog dialog = new OpenFileDialog
               {
                  DefaultExt = "pdf",
                  Filter = "PDF Files(*.pdf)|*.pdf"
               };
               dialog.ShowDialog();
               string fileName = dialog.FileName;
               MessageBox.Show("'파일2'을 선택하세요");
               OpenFileDialog dialog2 = new OpenFileDialog();
               dialog.DefaultExt = "pdf";
               dialog.Filter = "PDF Files(*.pdf)|*.pdf";
               dialog.ShowDialog();
               string str2 = dialog.FileName;
               Attachment item = new Attachment(fileName);
               Attachment attachment2 = new Attachment(str2);
               message.Attachments.Add(item);
               message.Attachments.Add(attachment2);
            }
            if (MessageBox.Show("전송을 시작합니다.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
               try
               {
                  client.Send(message);
                  MessageBox.Show("메일전송완료");
               }
               catch (SmtpException exception)
               {
                  Console.WriteLine(exception.Message);
               }
            }
            else
            {
               MessageBox.Show("취소합니다.");
            }
         }
         else
         {
            MessageBox.Show("취소합니다.");
         }
      }

      #endregion
      #region---<ncc vpn접속>-----------------

      private void btnNcclogin_Click(object sender, EventArgs e)
      {
         Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FutureSystems SSLplus\conf\p.ncc.re.kr.fro");
         Sleep(1500);
         Process[] vpnproc = Process.GetProcessesByName("frodo-pc-client");
         while ((vpnproc == null) || ((vpnproc != null) ? (vpnproc.Length == 0) : false))
         {
            vpnproc = Process.GetProcessesByName("frodo-pc-client");
            Sleep(1500);
         }
         Sleep(2000);
         Process process = vpnproc[0];
         IntPtr edit1 = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
         IntPtr edit2 = FindWindowEx(process.MainWindowHandle, edit1, "Edit", null);
         Sleep(1000);
         SendMessage(edit1, 0xC, IntPtr.Zero, Properties.Settings.Default.nccvpnid);
         SendMessage(edit2, 0xC, IntPtr.Zero, Properties.Settings.Default.nccvpnpw);
         PostMessage(edit1, 0x100, new IntPtr(0x0D), null);
         PostMessage(edit2, 0x101, new IntPtr(0X0D), null);
         Process.Start("mstsc");
         Sleep(6000);
         this.handle = FindWindow(null, "원격 데스크톱 연결");
         PostMessage(this.handle, 0x100, 13, 0x1c001);
         PostMessage(this.handle, 0x102, 13, 0xc01c001);
      }

      #endregion
      #region---<보령아산 파싱안되는리스트 복사>-----------------

      private void btnNoparsing_Click(object sender, EventArgs e)
      {
         Directory.CreateDirectory(Application.StartupPath + "\\ExportNonParsing");

         string sSource = Properties.Settings.Default.BRHCopypath;

         CopyFolder(sSource);// 각 의뢰명 으로 변경되게 반복문
         CopyFolder(sSource);// 각 의뢰명 으로 변경되게 반복문
         CopyFolder(sSource);// 각 의뢰명 으로 변경되게 반복문

         MessageBox.Show("완료");
         btnNoparsing.Text = "NoParsingCopy";
      }

      public void CopyFolder(string sSource)
      {
         string[] sElement = File.ReadLines("input.txt").ToArray();
         int iMaxcnt = sElement.Count();
         string sNocopy = "";

         for (int i = 0; i < iMaxcnt; i++)
         {
            string[] sFiles = Directory.GetFiles(sSource + "\\" + sElement[i]);
            Directory.CreateDirectory("ExportNonParsing\\" + sElement[i]);

            foreach (string file in sFiles)
            {
               string sName = Path.Combine("ExportNonParsing\\" + sElement[i], Path.GetFileName(file));
               try
               {
                  File.Copy(file, sName);
               }
               catch
               {
                  sNocopy += sName + "\r\n";
                  continue;
               }

            }
            btnNoparsing.Text = i.ToString();
         }
         File.AppendAllText(Application.StartupPath + "\\NoCopyList.log", "\r\n ---" + DateTime.Now.ToString() + "---\r\n" + sNocopy + "\r\n");
      }

      #endregion

      private void btnWindowtempdel_Click(object sender, EventArgs e)
      {
         //appdata\local\microsft\window\inetcache
         //
      }

      private void btnCallOfDuty_Click(object sender, EventArgs e)
      {
         sNow = "COD";

         string sTxtpath = Application.StartupPath + "\\dutyinput.txt";
         if (File.Exists(sTxtpath))
         {
            string[] sDutyinfo = File.ReadAllLines(sTxtpath);
         }
         else
         {
            MessageBox.Show("파일이 없습니다.");
         }
      }

      #endregion
      #region---<좌표용타이머>------------------

      private void trDebug_Tick(object sender, EventArgs e)
      {
         Mousx = Cursor.Position.X;
         Mousy = Cursor.Position.Y;
         Frmx = this.Location.X;
         Frmy = this.Location.Y;
         Sizx = this.Size.Width;
         Sizy = this.Size.Height;
         tsstslbDebug.Text = string.Format("마우스X({0})/마우스Y({1})/폼위치X({2})/폼위치Y({3})/폼크기X({4})/폼크기Y({5})", Cursor.Position.X, Cursor.Position.Y, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
      }


      #endregion


   }
}
