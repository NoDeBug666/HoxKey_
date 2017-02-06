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

namespace HoxKey
{
    public partial class CustomMenu : Form
    {
        //Script Control Method
        public delegate void SaveScript(string name, CommandCollection cc);
        public delegate bool ReadScript(string name, out CommandCollection cc, out string Fail_Reason);
        void Add_Command(string CommandName, int AddAt, params object[] CommandInput)
        {
            string AssemblyName = (typeof(Commands).GetProperty(CommandName).GetValue(null) as Type).AssemblyQualifiedName;
            Type CmdType = Type.GetType(AssemblyName);
            var invoke = CmdType.GetConstructor(new Type[0]);

            object cmd = invoke.Invoke(new object[0]);

            //奇數個CommandInput是參數名稱
            //偶數個CommandInput是參數值
            for (int i = 0; i < CommandInput.Length; i += 2)
            {
                CmdType.GetField((string)CommandInput[i]).SetValue(cmd, CommandInput[i + 1]);
            }
            Add_Command((ICommand)cmd, AddAt);
        }
        void Add_Command(ICommand cmd, int AddAt)
        {
            cmds.Insert(AddAt, cmd);
        }
        bool Save_Script(string name, IList<ICommand> cmds, SaveScript ss)
        {
            //檢查資料夾存在
            if (!Directory.Exists(Const.ScriptDicPath))
                Directory.CreateDirectory(Const.ScriptDicPath);

            //檢查檔案名稱
            name = Const.ScriptDicPath + @"\" + name;
            if (File.Exists(name))
            {
                int Interval = 1;
                while (File.Exists(name + Interval.ToString()))
                    Interval++;
                name += Interval.ToString();
            }
            CommandCollection cc = new CommandCollection(cmds);
            //建立
            ss(name, cc);
            return true;
        }
        bool Read_Script(string name, out string Fail_Reason, ReadScript rs)
        {
            name = Const.ScriptDicPath + @"\" + name;
            if (!File.Exists(name))
            {
                Fail_Reason = "檔案不存在";
                return false;
            }
            CommandCollection list;
            if (!rs(name, out list, out Fail_Reason))
                return false;

            cmds.Clear();
            foreach (ICommand cmd in list.cmds)
                cmds.Add(cmd);

            Fail_Reason = String.Empty;
            return true;
        }
    }
}
