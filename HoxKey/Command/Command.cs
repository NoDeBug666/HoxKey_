﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsHook;
using Win32_BigEdit;
using System.Runtime.InteropServices;
using System.IO;
using Win32_BigEdit.Enum;
using System.Diagnostics;
namespace HoxKey
{
    public static class Commands
    {
        //TODO: 改進這種奇妙的設計方式
        public static Type MoveMouse { get { return typeof(MoveMouse); } }
        public static Type ClickMouse { get { return typeof(ClickMouse); } }
        public static Type Pause { get { return typeof(Pause); } }
        public static Type KeyBoard { get { return typeof(KeyBoard); } }
        public static Type TypeWords { get { return typeof(TypeWords); } }
        public static Type Loop { get { return typeof(Loop); } }
        public static Type EndLoop { get { return typeof(EndLoop); } }
        public static Type Break { get { return typeof(Break); } }
        public static Type Continue { get { return typeof(Continue); } }
        public static Type EndScript { get { return typeof(EndScript); } }
        public static Type Variable { get { return typeof(Variable); } }
        public static Type hMouseMove { get { return typeof(hMouseMove); } }
        public static Type hMouseMoveAbs { get { return typeof(hMouseMoveAbs); } }
    }

    [Serializable]
    public class CommandCollection
    {
        public CommandCollection(IList<ICommand> cmds)
        {
            this.cmds = new ICommand[cmds.Count];
            for (int i = 0; i < cmds.Count; i++)
            {
                this.cmds[i] = cmds[i];
            }
        }

        public ICommand[] cmds;
    }

    [Serializable]
    public class Argument
    {
        /// <summary>
        /// 變數陣列,內部存有數據值
        /// </summary>
        public int[] IntegerArray;
        /// <summary>
        /// 執行跳出迴圈
        /// </summary>
        public bool Break;
        /// <summary>
        /// 執行繼續,如果不是在一個Loop內,則會回到最上方
        /// </summary> 
        public bool Continue;
        /// <summary>
        /// 結束執行
        /// </summary>
        public bool End;
        /// <summary>
        /// 跳去指定行操作,如果沒有操作需要保持-1
        /// </summary>
        public int Goto;
        /// <summary>
        /// 結束一個迴圈
        /// </summary>
        public bool EndLoop;
        /// <summary>
        /// 分配一個新的Loop,要跑NewLoop次,不新增此值需設0
        /// </summary>
        public uint NewLoop;
        /// <summary>
        /// 指令的回應
        /// </summary>
        public object Response;
    }

    [Serializable]
    public abstract class ICommand
    {
        public abstract void Action(Argument args);
    }
    [Serializable]
    public class EmptyCommand :ICommand
    {
        public override string ToString()
        {
            return String.Empty;
        }
        public override void Action(Argument args)
        {
        }
    }

    [Serializable]
    [CommandDescription(Description = "移動滑鼠游標,畫面右上角為起點(0,0)")]
    public class MoveMouse : ICommand
    {
        public int mx;
        public int my;
        public bool IsAbsolute;

        public override string ToString()
        {
            return String.Format("滑鼠游標 位移  ({0},{1})", mx, my);
        }
        public override void Action(Argument arg)
        {
            
            if (IsAbsolute)
                InputManagement.MouseMoveAbs(mx, my);
            else
                InputManagement.MouseMove(mx, my);
           
        }

    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "進行滑鼠操作")]
    public class ClickMouse : ICommand
    {
        public InputManagement.MOUSEFLAG MouseButton;
        public override string ToString()
        {
            return String.Format("滑鼠操作 {0}",MouseButton);
        }
        public override void Action(Argument arg)
        {
            InputManagement.MouseBtn(MouseButton);
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "暫停執行一段時間,單位是毫秒(0.001秒)")]
    public class Pause: ICommand
    {
        public uint PauseTime;

        public override string ToString()
        {
            return String.Format("暫停 {0}", PauseTime);
        }
        public override void Action(Argument arg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < PauseTime) ;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "進行鍵盤操作")]
    public class KeyBoard : ICommand
    {
        public VirtualKeyShort Vk;
        public bool IsPressed;
        public override string ToString()
        {
            return String.Format("鍵盤操作 {0} {1}", Vk, IsPressed ? "按下" : "放開");
        }
        public override void Action(Argument arg)
        {
            InputManagement.Send_Vk(Vk, IsPressed);
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "打一段字,本功能只會辨識a~z,A~Z,0~9,還有換行,其他字會無視" + "\n" + "Interval是打每個字的間隔")]
    public class TypeWords : ICommand
    {
        public string Words;
        public uint Interval;

        public override string ToString()
        {
            return String.Format("打字 {0}",Words.Length > 20 ? Words.Substring(0,20) : Words );
        }

        public override void Action(Argument args)
        {
            VirtualKeyShort key;

            foreach (char c in Words)
            {
                if ('0' <= c && c <= '9')
                {
                    key = (VirtualKeyShort)((int)VirtualKeyShort.KEY_0 + (c - '0'));
                    InputManagement.Send_Vk(key, true);
                    InputManagement.Send_Vk(key, false);
                }
                else if ('a' <= c && c <= 'z')
                {
                    //如果是大寫鎖定要先切換
                    if ((Win32_BigEdit.WinMethod.GetKeyState(Win32_BigEdit.Enum.VirtualKeyShort.CAPITAL) & 0x1) == 0x1)
                    {
                        InputManagement.Send_Vk(VirtualKeyShort.CAPITAL, true);
                        InputManagement.Send_Vk(VirtualKeyShort.CAPITAL, false);
                    }
                    key = (VirtualKeyShort)((int)VirtualKeyShort.KEY_A + (c - 'a'));
                    InputManagement.Send_Vk(key, true);
                    InputManagement.Send_Vk(key, false);
                }
                else if ('A' <= c && c <= 'Z')
                {
                    //如果是大寫鎖定要先切換
                    if ((Win32_BigEdit.WinMethod.GetKeyState(Win32_BigEdit.Enum.VirtualKeyShort.CAPITAL) & 0x1) != 0x1)
                    {
                        InputManagement.Send_Vk(VirtualKeyShort.CAPITAL, true);
                        InputManagement.Send_Vk(VirtualKeyShort.CAPITAL, false);
                    }
                    key = (VirtualKeyShort)((int)VirtualKeyShort.KEY_A + (c - 'A'));
                    InputManagement.Send_Vk(key, true);
                    InputManagement.Send_Vk(key, false);
                }
                else if (c == '\n')
                {
                    key = VirtualKeyShort.RETURN;
                    InputManagement.Send_Vk(key, true);
                    InputManagement.Send_Vk(key, false);
                }
                else if (c == ' ')
                {
                    key = VirtualKeyShort.SPACE;
                    InputManagement.Send_Vk(key, true);
                    InputManagement.Send_Vk(key, false);
                }
                if (Interval != 0)
                    System.Threading.Thread.Sleep((int)Interval);
            }
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "重複執行程式碼,迴圈範圍為Loop到下一個對應的EndLoop指令之間的所有指令")]
    public class Loop : ICommand
    {
        public uint LoopNum;
        public int EndLoopLine { get; set; }

        public override string ToString()
        {
            return String.Format("循環操作 {0} 次", LoopNum);
        }
        public override void Action(Argument arg)
        {
            arg.NewLoop = LoopNum;
            arg.Response = EndLoopLine;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "跳出迴圈")]
    public class Break : ICommand
    {
        public override string ToString()
        {
            return "跳出迴圈";
        }
        public override void Action(Argument arg)
        {
            arg.Break = true;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "直接結束此次迴圈循環,回到迴圈上方繼續執行")]
    public class Continue : ICommand
    {
        public override string ToString()
        {
            return "重新開始迴圈";
        }
        public override void Action(Argument arg)
        {
            arg.Continue = true;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "直接結束腳本")]
    public class EndScript : ICommand
    {
        public override string ToString()
        {
            return "結束腳本";
        }
        public override void Action(Argument arg)
        {
            arg.End = true;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "迴圈終點")]
    public class EndLoop : ICommand
    {
        public override string ToString()
        {
            return "結束迴圈";
        }
        public override void Action(Argument arg)
        {
            arg.EndLoop = true;
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "變數操作")]
    public class Variable : ICommand
    {
        public int VarIndex;
        public VarAct act;
        public bool IsFromDigit;
        public int NumOrIndex;
        public override string ToString()
        {
            if(IsFromDigit)
                return string.Format("操作變數 變數編號{0} {1} 數值{2}",VarIndex,act,NumOrIndex);
            return string.Format("操作變數 變數編號{0} {1} 變數編號{2}", VarIndex, act, NumOrIndex);
        }
        public override void Action(Argument arg)
        {
            int val = NumOrIndex;
            if (!IsFromDigit)
                val = arg.IntegerArray[val];
            switch (act)
            {
                case VarAct.Add:
                    arg.IntegerArray[VarIndex] += val;
                    break;
                case VarAct.Divide:
                    arg.IntegerArray[VarIndex] /= val;
                    break;
                case VarAct.Mod:
                    arg.IntegerArray[VarIndex] %= val;
                    break;
                case VarAct.Multiply:
                    arg.IntegerArray[VarIndex] *= val;
                    break;
                case VarAct.Remove:
                    arg.IntegerArray[VarIndex] -= val;
                    break;
                case VarAct.Set:
                    arg.IntegerArray[VarIndex] = val;
                    break;
            }
        }
        public enum VarAct
        {
            Set,Add,Remove,Multiply,Divide,Mod
        }
    }
    // TODO 新增Key Toggle Switch Command
    // TODO 新增Condition Command
    // TODO 圖片偵測
    // TODO 新增其他腳本呼叫功能 (IMPORTANT)
    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "呼叫其他腳本")]
    public class CallScript : ICommand
    {
        public string Path;
           
        public override string ToString()
        {
            return "呼叫腳本 [" + Path +"]";
        }
        public override void Action(Argument args)
        {
            
        }
    }
    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "平滑地移動滑鼠")]
    public class hMouseMove : ICommand
    {
        //移動的相對座標量
        public int mx, my;
        //總共移動時間
        public uint time;
        public override string ToString()
        {
            return String.Format("平滑游標 位移  ({0},{1}) , 耗時{2}", mx, my,time);
        }
        public override void Action(Argument arg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if(time == 0)
            {
                InputManagement.MouseMove(mx ,my);
                return;
            }
            //movement 
            double x_addup = 0;
            double y_addup = 0;
            double x_per_ms = (double)mx / time;
            double y_per_ms = (double)my / time;
            int x, y;
            //time
            long lastCheck = 0;
            long Difference = 0;
            long temp;

            int x_sum = 0,y_sum = 0;

            do
            {
                //compute current mouse position
                temp = sw.ElapsedMilliseconds;
                Difference = temp - lastCheck;
                lastCheck = temp;

                x_addup += x_per_ms * Difference;
                y_addup += y_per_ms * Difference;

                x = (int)x_addup;
                y = (int)y_addup;

                InputManagement.MouseMove(x, y);

                x_addup -= x;
                y_addup -= y;
                x_sum += x;
                y_sum += y;
            } while (sw.ElapsedMilliseconds < time);
            InputManagement.MouseMove(mx - x_sum, my - y_sum);
        }
    }

    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "平滑地移動滑鼠(絕對位置)")]
    public class hMouseMoveAbs : ICommand
    {
        //移動的相對座標量
        public int mx, my;
        //總共移動時間
        public uint time;
        //起始位置
        public int sx, sy;
        public override string ToString()
        {
            return String.Format("平滑游標 自({3},{4}) 位移 ({0},{1}) , 耗時{2}", mx, my, time, sx, sy);
        }
        public override void Action(Argument arg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (time != 0)
            {
                do
                {
                    //compute current mouse position
                    double progress = sw.ElapsedMilliseconds / (double)time;
                    InputManagement.MouseMoveAbs((int)(sx + mx * progress), (int)(sy + my * progress));
                } while (sw.ElapsedMilliseconds < time);
            }
            InputManagement.MouseMoveAbs(sx + mx, sy + my);
        }
    }


    [Serializable]
    [CommandVersion(1)]
    [CommandDescription(Description = "一連串的滑鼠操作")]
    public class hVkSerial : ICommand
    {
        public VirtualKeyShort[] vks;
        //Interval的高位元用來表示是否按下,1為Pressed,0為Release
        public ushort[] interval;
        public override string ToString()
        {
            return String.Format("[精密]序列鍵盤操作:{0}個,共{1}毫秒", vks.Length, interval.Sum((x) => x & 0x7FFF));
        }
        public override void Action(Argument args)
        {
            DateTime period;
            for (int i = 0;i < vks.Length;i++)
            {
                InputManagement.Send_Vk(vks[i], (interval[i] & 0x8000) == 0x8000);
                period = DateTime.Now.AddMilliseconds(interval[i] & 0x7FFF);
                while (DateTime.Now < period) ;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = false)]
    public class CommandDescription : Attribute
    {
        public string Description { set; get; }

        public string GetDescription()
        {
            return Description;
        }
        public override string ToString()
        {
            return Description;
        }
    }
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = false)]
    public class CommandVersion : Attribute
    {
        internal int Version { private set; get; }
        public CommandVersion(int v)
        {
            this.Version = v;
        }
    }
}
