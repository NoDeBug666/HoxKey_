using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Win32_BigEdit;
using WindowsHook;
using Win32_BigEdit.Enum;
namespace HoxKey
{
    public partial class CustomMenu:Form
    {
        //Script Error Detecting Algorithm
        public bool LoopGoToSet(IList<ICommand> cmds, out string report)
        {
            StringBuilder sb = new StringBuilder();
            Stack<int> Loops = new Stack<int>();
            int len = cmds.Count;
            bool result = true;

            //遇到Loop就塞入LoopsStack他的Line
            //遇到Break就塞入BreaksStack他的Line
            //遇到Continue就讀取LoopsStack的Peek Line,且塞入ContinuesStack內
            //遇到EndLine就把BreaksStack全部清空,且所有內部的Break GoTo設為當前行數i,然後Loop減一

            //Scan Commands
            for (int i = 0, j = -1; i < len; i++, j = -1)
            {
                //Command identify
                if (cmds[i] is Loop)
                    Loops.Push(i);
                else if (cmds[i] is EndLoop)
                {
                    if (Loops.Count != 0)
                    {
                        (cmds[Loops.Pop()] as Loop).EndLoopLine = i;
                    }
                    else
                    {
                        sb.AppendFormat("[Line{0,5}]EndLoop指令找不到對應的迴圈\n", i);
                        result = false;
                    }
                }
            }

            while (Loops.Count > 0)
            {
                sb.AppendFormat("[Line{0,5}]Loop指令找不到對應的結束EndLoop\n", Loops.Pop());
                result = false;
            }

            if (!result)
                report = sb.ToString();
            else
                report = null;
            return result;
        }

        /// <summary>
        /// 優化腳本的位移命令,將多個位移命令轉成平滑的位移,缺點是會缺失位移過程的精準度
        /// </summary>
        /// <param name="cmds">命令來源</param>
        /// <param name="Distinction">允許的差距</param>
        /// <param name="CombineAbsolute">是否要整合絕對位移</param>
        /// <returns>共多少命令被整合</returns>
        public int MouseMoveOptimization(IList<ICommand> cmds,double Distinction)
        {
            int sum = 0;
            for(int i = 0;i < cmds.Count-1;i++)
            {
                if(cmds[i] is MoveMouse)
                {
                    MoveMouse begin = cmds[i] as MoveMouse; //取得第一個命令
                    MoveMouse end = null;
                    int xTotal = begin.mx;                  //總X座標位移
                    int yTotal = begin.my;                  //總Y座標位移
                    double Begin_m = GetAngle(begin);       //起始位移和正向X軸的夾角
                    uint CostTime = 0;                      //記錄位移總消耗時間
                    int Combined = 1;                       //記錄已經結合多少個命令
                    bool IsAbsolute = begin.IsAbsolute;     //是否為絕對位置位移
                    //開始向前搜尋,嘗試融合命令
                    int index = i + 1;
                    while(index < cmds.Count && (cmds[index] is MoveMouse || cmds[index] is Pause) )
                    {
                        //搜尋到的下個命令是暫停
                        if(cmds[index] is Pause)
                        {
                            //位移時間累計上升
                            CostTime += (cmds[index] as Pause).PauseTime;
                            cmds.RemoveAt(index);
                            Combined++;
                        }else if(cmds[index] is MoveMouse)
                        {
                            //是位移命令
                            MoveMouse temp = (cmds[index] as MoveMouse);
                            //跳過不同類的命令
                            if (temp.IsAbsolute != IsAbsolute)
                                break;
                            //確認命令的角度差,是否超過允許誤差範圍
                            double End_m = GetAngle(temp);
                            if (!InDifferenceRange(Begin_m, End_m, Distinction))
                                break;
                            //確認可以合併,統整此操作的資料
                            end = temp;
                            xTotal += end.mx;
                            yTotal += end.my;
                            cmds.RemoveAt(index);
                            Combined++;
                        }
                    }
                    //如果沒有整合到任何命令,直接離開
                    if (Combined == 0)
                        continue;
                    if (end == null)
                        end = new MoveMouse() { mx = begin.mx, my = begin.my };
                    sum += Combined;
                    //產生整合過的命令,並且加入命令列
                    ICommand res;
                    if (IsAbsolute)
                        res = new hMouseMoveAbs()
                        {
                            mx = end.mx - begin.mx,
                            my = end.my - begin.my,
                            time = CostTime,
                            sx = begin.mx,
                            sy = begin.my
                        };
                    else
                        res = new hMouseMove()
                        {
                            mx = xTotal,
                            my = yTotal,
                            time = CostTime
                        };
                    cmds.RemoveAt(i);
                    cmds.Insert(i,res);
                }
            }
            return sum;
        }
        /// <summary>
        /// 取得一個位移命令和假想地平線的夾角
        /// </summary>
        /// <param name="m1">第一個位移命令</param>
        /// <returns></returns>
        public double GetAngle(MoveMouse m1)
        {
            return GetAngle(m1.mx, m1.my);
        }
        public double GetAngle(double x,double y)
        {
            return (Math.Atan2(y, x)/Math.PI*180+360)%360;
        }
        public bool InDifferenceRange(double main,double check,double distinction)
        {
            double a1 = Math.Abs(main - check);
            double a2 = Math.Abs(main + 360 - check);
            return a1 < distinction || a2 < distinction;
        }

        //Recording Mouse and keyboard
        public bool _Recording;
        public bool Recording
        {
            private set
            {
                _Recording = value;
                if (_Recording)
                {
                    HookUpEvent();
                    KeyBoardHook.Enable = true;
                    MouseHook.Enable = true;
                    this.Text = "自定義腳本REC";
                }
                else
                {
                    HookRemoveEvent();
                    Done();
                    this.Text = "自定義腳本";
                }
            }
            get
            {
                return _Recording;
            }
        }
        Thread Zip;
        Queue<Tuple<EventArgs, DateTime>> Receiver;
        List<ICommand> Output;
        bool ShutDownByUserHotKey = false;
        private object RecordFunLock = new object();
        public void StartRecord()
        {
            lock (RecordFunLock)
            {
                if (Recording)
                    return;
                Zip = new Thread(new ThreadStart(NoZip));
                Zip.Priority = ThreadPriority.Normal;
                Receiver = new Queue<Tuple<EventArgs, DateTime>>(10000);
                Output = new List<ICommand>(10000);
                Zip.Start();
                Recording = true;
            }
        }

        private void HookUpEvent()
        {
            MouseHook.IsLowLevel = true;
            MouseHook.MouseMove += MouseEvent;
            MouseHook.MouseDown += MouseEvent;
            MouseHook.MouseUp += MouseEvent;
            KeyBoardHook.IsLowLevel = true;
            KeyBoardHook.KeyDown += KeyBoardEvent;
            KeyBoardHook.KeyUp += KeyBoardEvent;

        }
        private void HookRemoveEvent()
        {
            MouseHook.MouseMove -= MouseEvent;
            MouseHook.MouseDown -= MouseEvent;
            MouseHook.MouseUp -= MouseEvent;
            KeyBoardHook.KeyDown -= KeyBoardEvent;
            KeyBoardHook.KeyUp -= KeyBoardEvent;
        }
        private void Done()
        {
            //等待壓縮工作結束
            while (Receiver.Count > 0) ;

            //如果是透過熱鍵停止運作,刪除Ctrl之後的所有動作
            if (ShutDownByUserHotKey)
            {
                bool find = false;
                int i = Output.Count - 1;
                for (; i >= 0; i--)
                    if (Output[i] is KeyBoard &&
                        ((Output[i] as KeyBoard).Vk == VirtualKeyShort.CONTROL ||
                        (Output[i] as KeyBoard).Vk == VirtualKeyShort.RCONTROL ||
                        (Output[i] as KeyBoard).Vk == VirtualKeyShort.LCONTROL
                        )
                        )
                    {
                        find = true;
                        break;
                    }
                if (find)
                    Output.RemoveRange(i, Output.Count - i);


            }

            //放上記錄結果
            cmds.Clear();
            foreach (ICommand cmd in Output)
            {
                cmds.Add(cmd);
            }
            //清除記憶體
            Output.Clear();
        }

        private void MouseEvent(object sender, MouseHook.MouseEventArgs e)
        {
            if (Recording)
                Receiver.Enqueue(Tuple.Create<EventArgs, DateTime>(e, DateTime.Now));
        }
        private void KeyBoardEvent(object sender, KeyBoardHook.KeyBoardArgs e)
        {
            if (Recording)
                Receiver.Enqueue(Tuple.Create<EventArgs, DateTime>(e, DateTime.Now));
        }

        private void NoZip()
        {
            DateTime last = DateTime.Now;
            DateTime chk;
            while (Recording || Receiver.Count > 0)
            {
                if (Receiver.Count > 0)
                {
                    Tuple<EventArgs, DateTime> a = Receiver.Dequeue();
                    if (a.Item1 is WindowsHook.MouseHook.MouseEventArgs)
                    {

                        var arg = a.Item1 as WindowsHook.MouseHook.MouseEventArgs;

                        switch (arg.Button)
                        {
                            case Win32_BigEdit.Enum.MouseMessage.WM_LBUTTONDOWN:
                                Output.Add(new ClickMouse() { MouseButton = InputManagement.MOUSEFLAG.LEFTDOWN });
                                break;
                            case Win32_BigEdit.Enum.MouseMessage.WM_LBUTTONUP:
                                Output.Add(new ClickMouse() { MouseButton = InputManagement.MOUSEFLAG.LEFTUP });
                                break;
                            case Win32_BigEdit.Enum.MouseMessage.WM_RBUTTONDOWN:
                                Output.Add(new ClickMouse() { MouseButton = InputManagement.MOUSEFLAG.RIGHTDOWN });
                                break;
                            case Win32_BigEdit.Enum.MouseMessage.WM_RBUTTONUP:
                                Output.Add(new ClickMouse() { MouseButton = InputManagement.MOUSEFLAG.RIGHTUP });
                                break;
                            default:
                                Output.Add(new MoveMouse()
                                {
                                    mx = arg.X,
                                    my = arg.Y,
                                    IsAbsolute = true
                                });
                                break;
                        }

                    }
                    else if (a.Item1 is WindowsHook.KeyBoardHook.KeyBoardArgs)
                    {

                        var arg = a.Item1 as WindowsHook.KeyBoardHook.KeyBoardArgs;
                        Output.Add(new KeyBoard()
                        {
                            IsPressed = arg.IsPressed,
                            Vk = (VirtualKeyShort)arg.VirtualKey
                        });

                    }
                    chk = DateTime.Now;
                    Output.Add(new Pause() { PauseTime = (uint)((chk - last).Milliseconds) });
                    last = chk;
                }
            }
            chk = DateTime.Now;
            if ((chk - last).Milliseconds != 0)
                Output.Add(new Pause() { PauseTime = (uint)((chk - last).Milliseconds) });
            last = chk;

        }
        private void ZipMethod()
        {
            //指令壓縮演算法
            //移動滑鼠指令 
            ICommand Serial = null;
            //Mouse
            int x = 0, y = 0;
            double Begin_m = -1;
            //Keyboard
            List<VirtualKeyShort> keyboard = new List<VirtualKeyShort>();
            List<ushort> interval = new List<ushort>();
            //Global
            bool SerialDone = false;
            int Paused = 0;
            DateTime dt = DateTime.MinValue;

            while (Receiver.Count > 0)
            {
                //Peek Queue
                //if 能加入
                //加入
                if (Receiver.Count > 0)
                {
                    var t = Receiver.Peek();
                    #region 新串列建立
                    if (Serial == null)
                        if (t.Item1 is WindowsHook.MouseHook.MouseEventArgs)
                            Serial = new hMouseMove();
                        else if (t.Item1 is WindowsHook.KeyBoardHook.KeyBoardArgs)
                            Serial = new hVkSerial();
                        else
                        {
                            //無法偵測的命令
                            throw new Exception("沒有預期的輸入," + t.Item1.GetType().Name);
                        }
                    #endregion
                    #region 塞入串列
                    if (Serial is hMouseMove && t.Item1 is WindowsHook.MouseHook.MouseEventArgs)
                    {

                    }
                    else if (Serial is hVkSerial && t.Item1 is WindowsHook.KeyBoardHook.KeyBoardArgs)
                    {

                    }
                    #endregion
                    #region 是否載入此串列

                    #endregion
                }
            }
        }
    }
}
