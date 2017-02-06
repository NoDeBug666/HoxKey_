using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using Win32_BigEdit;
using WindowsHook;
using Win32_BigEdit.Enum;

namespace HoxKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ModeSwitch();
        }
        CustomMenu cm = null;
        Thread Loop;
        private bool _IsWorking = false;
        internal bool IsWorking
        {
            set
            {
                WorkPropertiesPanel.Enabled = !value;
                _IsWorking = value;

            }
            get
            {
                return _IsWorking;
            }
        }
        bool ShutDownSign = false;
        Mode mode;
        ICommand[] cmds;

        #region 點擊測試
        int _clicked = 0;
        public int clicked
        {
            get
            {
                return _clicked;
            }
            set
            {
                _clicked = value;
                ClickTestLabel.Text = _clicked.ToString();
            }
        }
        private void ClickTestButton_Click(object sender, EventArgs e)
        {
            clicked += 1;
        }
        private void ClickClearButton_Click(object sender, EventArgs e)
        {
            clicked = 0;
        }
        #endregion

        #region 連點處理
        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartWork();
        }
        
        private void ShutDownBtn_Click(object sender, EventArgs e)
        {
            ShutDown(null);
        }
        private void StartWork()
        {
            if (IsWorking)
                return;
            IsWorking = true;
            ShutDownSign = false;

            #region 轉換表單參數
            TimePara.KeepTime = ((int)KeepTimeNum.Value)*1000;
            CommonPara.HoldTime = (int)HoldTimeNum.Value;
            FixedPara.ClickCount = (int)ClickCountNum.Value;
            CommonPara.BeginDelay = ((int)BeginDelayNum.Value)*1000;
            #endregion
            #region Thread分配任務
            ThreadStart Mission = null;
            switch (mode)
            {
                case Mode.TimeMode:
                    Mission = TimeMode;
                    break;
                case Mode.FixedMode:
                    Mission = FixedMode;
                    break;
                case Mode.CustomMode:
                    Mission = CustomMode;
                    break;
                default:
                    MessageBox.Show("啟動異常");
                    return;
            }
            #endregion

            Loop = new Thread(Mission);
            Loop.Priority = ThreadPriority.Highest;
            Loop.Start();
        }

        private void TimeMode()
        {
            Invoke(new Action(S_BeginDelay));
            Thread.Sleep(CommonPara.BeginDelay);
            Invoke(new Action(S_Begin));
            var timer = new System.Threading.Timer(ShutDown,null,TimePara.KeepTime,Timeout.Infinite);
            while (!ShutDownSign)
            {
                OneClick(CommonPara.HoldTime,CommonPara.HoldTime);
            }
            timer.Dispose();
            Invoke(new Action(S_Finish));
        }
        private void FixedMode()
        {
            Invoke(new Action(S_BeginDelay));
            Thread.Sleep(CommonPara.BeginDelay);
            Invoke(new Action(S_Begin));
            int c = 0;
            while (!ShutDownSign && c < FixedPara.ClickCount)
            {
                OneClick(CommonPara.HoldTime, CommonPara.HoldTime);
                c++;
            }

            Invoke(new Action(S_Finish));
        }
        private void CustomMode()
        {

            //初始化命令參數
            Argument args = new Argument();
            args.Break = args.Continue = args.End = args.EndLoop = false;
            args.Goto = -1;
            args.IntegerArray = new int[Const.IntegerArraySize];

            //開始
            int Line = 0;                       //目前執行行數
            uint[] LoopStack = new uint[100];   //暫存for迴圈的執行值
            int[] LoopLine = new int[100];      //Loop觸發行數
            int[] EndLoopLine = new int[100];   //EndLoop觸發行數
            int StackTop = -1;

            //起始預備
            Invoke(new Action(S_BeginDelay));
            Thread.Sleep(CommonPara.BeginDelay);
            Invoke(new Action(S_Begin));

            DateTime start = DateTime.Now;
            #region 整體命令操作
            while (Line < cmds.Length)
            {
                if (ShutDownSign)
                    break;
                //執行命令
                cmds[Line].Action(args);

                #region 回傳結果判斷
                {
                    if (args.NewLoop != 0)
                    {
                        //新迴圈要求
                        LoopStack[++StackTop] = args.NewLoop;
                        LoopLine[StackTop] = Line;
                        EndLoopLine[StackTop] = (int)args.Response;
                        args.Response = null;
                        args.NewLoop = 0;
                    }
                    else if (args.EndLoop)
                    {
                        //剛結束一個迴圈循環
                        LoopStack[StackTop]--;
                        if (LoopStack[StackTop] == 0)
                            --StackTop;
                        else
                            args.Goto = LoopLine[StackTop]+1;

                        args.EndLoop = false;
                    }
                    else if (args.Break)
                    {
                        //Break指令
                        args.Break = false;
                        args.Goto = EndLoopLine[StackTop] + 1;
                    }
                    else if (args.Continue)
                    {
                        //Continue指令
                        args.Continue = false;

                        LoopStack[StackTop]--;
                        if (LoopStack[StackTop] == 0)
                        {
                            args.Goto = EndLoopLine[StackTop] + 1;
                            --StackTop;
                        }
                        else
                            args.Goto = LoopLine[StackTop] + 1;
                    }
                    else if(args.End)
                    {
                        args.End = false;
                        args.Goto = cmds.Length;
                    }
                }
                #endregion
                if (args.Goto != -1)
                {
                    Line = args.Goto;
                    args.Goto = -1;
                }
                else
                    Line++;
            }
            #endregion
            DateTime end = DateTime.Now;

            Invoke(new Action(S_Finish));
            // TODO:改StopWatch
            Invoke(new Action(delegate ()
            {
                richTextBox1.Text += "\n[###]總操作時間:" + (end - start).TotalMilliseconds + "ms\n";
            }));
        }
        private void ShutDown(object state)
        {
            ShutDownSign = true;
        }

        private void S_BeginDelay()
        {
            msgLabel.Text = "準備開始";
        }
        private void S_Begin()
        {
            msgLabel.Text = "連點開始";
        }
        private void S_Finish()
        {
            msgLabel.Text = "連點結束";
            IsWorking = false;
        }
        #endregion

        #region 表單功能
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ModeSwitch();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ModeSwitch();
        }
        private void ModeSwitch()
        {
            string Used = "";
            List<string> NotUsed = new List<string>();
            if (TimeModeButton.Checked)
            {
                Used = "TimeMode";
                mode = Mode.TimeMode;
            }
            else if (FixedModeButton.Checked)
            {
                Used = "FixedMode";
                mode = Mode.FixedMode;
            }
            else if (CustomModeBtn.Checked)
            {
                Used = "CustomMode";
                mode = Mode.CustomMode;
            }

            Mode m = (Mode)0;
            while (m != Mode.NullChoice)
            {
                if (Used != m.ToString())
                    NotUsed.Add(m.ToString());
                m++;
            }

            
            foreach (var item in this.Controls)
                if (item is Control)
                {
                    Control ctl = item as Control;
                    if (ctl.Tag == null)
                        continue;
                    string s = ctl.Tag.ToString();
                    if (s == Used)
                        ctl.Enabled = true;
                    else if (NotUsed.IndexOf(s) != -1)
                        ctl.Enabled = false;
                }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Loop != null)
                Loop.Abort();
            if (KeyBoardHook.Enable)
                KeyBoardHook.Enable = false;
            if (MouseHook.Enable)
                MouseHook.Enable = false;
        }

        private void CustomMenuBtn_Click(object sender, EventArgs e)
        {
            if (cm == null)
                cm = new CustomMenu();
            if (KeyBoardHook.Enable)
            {
                KeyBoardHook.Enable = false;
                KeyBoardHook.KeyDown -= KeyBoardHook_KeyDown;
            }
            this.Visible = false;
            if(cm.ShowDialog() == DialogResult.OK)
            {
                this.cmds = new ICommand[cm.cmds.Count];
                for (int i = 0; i < cmds.Length; i++)
                    this.cmds[i] = cm.cmds[i];
            }
            if (!KeyBoardHook.Enable)
            {
                KeyBoardHook.Enable = true;
                KeyBoardHook.KeyDown += KeyBoardHook_KeyDown;
            }
            this.Visible = true;
        }

        private void KeyBoardHook_KeyDown(object sender, KeyBoardHook.KeyBoardArgs e)
        {
            short get = Win32_BigEdit.WinMethod.GetKeyState(Win32_BigEdit.Enum.VirtualKeyShort.CONTROL);
            if (!IsWorking)
            {
                if ((get & 0x80) == 0x80 && e.VirtualKey == (short)VirtualKeyShort.F12)
                    StartWork();
            }
            else
            {
                if ((get & 0x80) == 0x80 && e.VirtualKey == (short)VirtualKeyShort.F11)
                    ShutDown(null);
            }
        }

        #endregion

        private void OneClick(int pressDown, int pressUp)
        {
            InputManagement.MouseBtn(InputManagement.MOUSEFLAG.LEFTDOWN);
            Thread.Sleep(pressDown);
            InputManagement.MouseBtn(InputManagement.MOUSEFLAG.LEFTUP);
            Thread.Sleep(pressUp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.TopMost = true;
            KeyBoardHook.IsLowLevel = true;
            KeyBoardHook.KeyDown += KeyBoardHook_KeyDown;
            KeyBoardHook.Enable = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                InputManagement.INPUT[] ins = new InputManagement.INPUT[4];
                ins[0] = InputManagement.Get_keyboard_input(VirtualKeyShort.KEY_A, true, 1000);
                ins[1] = InputManagement.Get_keyboard_input(VirtualKeyShort.KEY_B, true, 1000);
                ins[2] = InputManagement.Get_keyboard_input(VirtualKeyShort.KEY_C, true, 1000);
                ins[3] = InputManagement.Get_keyboard_input(VirtualKeyShort.KEY_D, true, 1000);
                InputManagement.SendInput(4, ref ins[0], Marshal.SizeOf(ins[0]));
            });

            t.Start();
            
        }

    }

    static public class InputManagement
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 SendInput(UInt32 cInputs, ref INPUT pInputs, Int32 cbSize);
        [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public INPUTTYPE dwType;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBOARDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MOUSEINPUT
        {
            public Int32 dx;
            public Int32 dy;
            public Int32 mouseData;
            public MOUSEFLAG dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct KEYBOARDINPUT
        {
            public Int16 wVk;
            public Int16 wScan;
            public KEYBOARDFLAG dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HARDWAREINPUT
        {
            public Int32 uMsg;
            public Int16 wParamL;
            public Int16 wParamH;
        }

        public enum INPUTTYPE : int
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags()]
        public enum MOUSEFLAG : int
        {
            MOVE = 0x1,
            LEFTDOWN = 0x2,
            LEFTUP = 0x4,
            RIGHTDOWN = 0x8,
            RIGHTUP = 0x10,
            MIDDLEDOWN = 0x20,
            MIDDLEUP = 0x40,
            XDOWN = 0x80,
            XUP = 0x100,
            VIRTUALDESK = 0x400,
            WHEEL = 0x800,
            ABSOLUTE = 0x8000,
            MOVE_NOCOALESCE = 0x2000

        }

        [Flags()]
        public enum KEYBOARDFLAG : int
        {
            EXTENDEDKEY = 1,
            KEYUP = 2,
            UNICODE = 4,
            SCANCODE = 8
        }

        [Flags]
        internal enum KEYEVENTF : uint
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            SCANCODE = 0x0008,
            UNICODE = 0x0004
        }

        static public void MouseBtn(MOUSEFLAG flag)
        {
            INPUT leftdown = new INPUT();

            leftdown.dwType = 0;
            leftdown.mi = new MOUSEINPUT();
            leftdown.mi.dwExtraInfo = IntPtr.Zero;
            leftdown.mi.dx = 0;
            leftdown.mi.dy = 0;
            leftdown.mi.time = 0;
            leftdown.mi.mouseData = 0;
            leftdown.mi.dwFlags = flag;

            SendInput(1, ref leftdown, Marshal.SizeOf(typeof(INPUT)));
        }
        /// <summary>
        /// 模擬正常的滑鼠移動,需要注意滑鼠的移動會和許多參數有關,因此這裡輸入向左移動100不一定真的移動100Pixel
        /// </summary>
        /// <param name="dx">X座標位移</param>
        /// <param name="dy">Y座標位移</param>
        static public void MouseMoveReal(int dx,int dy)
        {
            INPUT input = new INPUT();

            input.dwType = 0;
            input.mi = new MOUSEINPUT();
            input.mi.dwExtraInfo = IntPtr.Zero;
            input.mi.dx = dx;
            input.mi.dy = dy;
            input.mi.time = 0;
            input.mi.mouseData = 0;
            // 游標移動需要改成ABSOLUTE移動
            input.mi.dwFlags = MOUSEFLAG.MOVE | MOUSEFLAG.MOVE_NOCOALESCE;

            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }
        /// <summary>
        /// 移動滑鼠游標(dx,dy)(Pixel單位)
        /// </summary>
        /// <param name="dx">X位移量(Pixel)</param>
        /// <param name="dy">Y位移量(Pixel)</param>
        static public void MouseMove(int dx,int dy)
        {
            point CursorPosition;
            if (!GetCursorPos(out CursorPosition))
                return;
            CursorSetAbsolute(
                (int)(((CursorPosition.x + dx + 1) * (65535.0 / SystemInformation.PrimaryMonitorSize.Width))),
                (int)(((CursorPosition.y + dy + 1) * (65535.0 / SystemInformation.PrimaryMonitorSize.Height)))
                );
        }
        /// <summary>
        /// 移動滑鼠游標(dx,dy)(Pixel單位)
        /// </summary>
        /// <param name="dx">X位移量(Pixel)</param>
        /// <param name="dy">Y位移量(Pixel)</param>
        static public void MouseMoveAbs(int dx, int dy)
        {
            point CursorPosition;
            if (!GetCursorPos(out CursorPosition))
                return;
            CursorSetAbsolute(
                (int)((( dx + 1) * (65535.0 / SystemInformation.PrimaryMonitorSize.Width))),
                (int)((( dy + 1) * (65535.0 / SystemInformation.PrimaryMonitorSize.Height)))
                );
        }
        /// <summary>
        /// 將游標移動至指定位置,輸入為游標的絕對位置
        /// </summary>
        /// <param name="dx">指定X座標</param>
        /// <param name="dy">指定Y座標</param>
        static public void CursorSetAbsolute(int dx, int dy)
        {
            INPUT input = new INPUT();

            input.dwType = 0;
            input.mi = new MOUSEINPUT();
            input.mi.dwExtraInfo = IntPtr.Zero;
            input.mi.dx = dx;
            input.mi.dy = dy;
            input.mi.time = 0;
            input.mi.mouseData = 0;
            // 游標移動需要改成ABSOLUTE移動
            input.mi.dwFlags = MOUSEFLAG.MOVE | MOUSEFLAG.MOVE_NOCOALESCE | MOUSEFLAG.ABSOLUTE ;

            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }

        static public void Send_Vk(VirtualKeyShort vk,bool Pressed = true,int time = 0)
        {
            INPUT input = new INPUT();
            KEYBOARDINPUT ki = new KEYBOARDINPUT();

            ki.wVk = (short)vk;
            ki.wScan = 0;
            ki.time = time;
            ki.dwFlags = Pressed ? 0 : KEYBOARDFLAG.KEYUP;
            
            input.dwType = INPUTTYPE.Keyboard;
            input.ki = ki;

            SendInput(1,ref input, Marshal.SizeOf(input));
        }
        static public INPUT Get_keyboard_input(VirtualKeyShort vk,bool Pressed = true,int time = 0)
        {
            INPUT input = new INPUT();
            KEYBOARDINPUT ki = new KEYBOARDINPUT();

            ki.wVk = (short)vk;
            ki.wScan = 0;
            ki.time = time;
            ki.dwFlags = Pressed ? 0 : KEYBOARDFLAG.KEYUP;

            input.dwType = INPUTTYPE.Keyboard;
            input.ki = ki;
            return input;
        }

        [DllImport("user32.dll",SetLastError =true)]
        public static extern bool GetCursorPos(out point p);
        public struct point { public Int32 x; public Int32 y; }
        public static string EchoCursor()
        {
            point p;
            GetCursorPos(out p);
            return string.Format("({0},{1})", p.x, p.y);
        }
    }

    static public class TimePara
    {
        static public int KeepTime;
    }
    static public class FixedPara
    {
        static public int ClickCount;
    }
    static public class CustomPara
    {

    }
    static public class CommonPara
    {
        static public int HoldTime;
        static public int BeginDelay;
    }

    enum Mode
    {
        FixedMode,
        TimeMode,
        CustomMode,
        NullChoice
    }
}
