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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Win32_BigEdit.Enum;
using WindowsHook;

namespace HoxKey
{
    public partial class CustomMenu : Form
    {
        public System.Collections.ObjectModel.ObservableCollection<ICommand> cmds;
        private void Collection_Changed(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Script.Items.Insert(e.NewStartingIndex, (ICommand)e.NewItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    Script.Items.RemoveAt(e.OldStartingIndex);
                    if (e.OldStartingIndex < e.NewStartingIndex)
                        Script.Items.Insert(e.NewStartingIndex -1, (ICommand)e.NewItems[0]);
                    else
                        Script.Items.Insert(e.NewStartingIndex, (ICommand)e.NewItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Script.Items.RemoveAt(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    throw new Exception("Not support Replace");
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    Script.Items.Clear();
                    foreach (var cmd in cmds)
                        Script.Items.Add((ICommand)cmd);
                    break;
            }

        }

        public CustomMenu()
        {
            InitializeComponent();

            //Add List 
            CommandList.Items.Clear();
            foreach (var t in typeof(Commands).GetProperties())
                CommandList.Items.Add(((Type)t.GetValue(null)).Name);

            cmds = new System.Collections.ObjectModel.ObservableCollection<ICommand>();
            cmds.CollectionChanged += Collection_Changed;

            Script.Items.Add(new EmptyCommand());
        }

        // Layout Method
        private void CommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type t = null;
            foreach (System.Reflection.PropertyInfo pt in typeof(Commands).GetProperties())
            {
                Type tt = pt.GetValue(null) as Type;
                if(tt.Name == (string)CommandList.SelectedItem)
                {
                    t = tt;
                    break;
                }
            }
            if (t == null)
                throw new Exception("Type not found");

            panel1.Controls.Clear();
            Point p = new Point(panel1.Margin.Left, panel1.Margin.Top);

            var enumerable = t.GetCustomAttributes(typeof(CommandDescription), false);
            if (enumerable.Length > 0)
                toolStripStatusLabel1.Text = (enumerable[0] as CommandDescription).Description;

            foreach (var properity in t.GetFields())
            {
                //名稱Label
                Label l = new Label()
                {
                    Text = properity.Name,
                    Location = p,
                    AutoSize = true
                };
                panel1.Controls.Add(l);
                p.Y += l.Margin.Bottom + l.Size.Height;


                //輸入資料Control
                #region Input Type Determine

                //資料存放Control
                Control ctl = null;

                //形態資料取得
                Type InputType = Type.GetType(properity.FieldType.AssemblyQualifiedName);
                if (InputType == typeof(uint))
                    ctl = new NumericUpDown()
                    {
                        Value = 0,
                        Maximum = 100000,
                        Minimum = 0,
                        AutoSize = true,
                        Tag = typeof(uint)
                    };
                else if (InputType == typeof(int))
                    ctl = new NumericUpDown()
                    {
                        Value = 0,
                        Maximum = 100000,
                        Minimum = -100000,
                        AutoSize = true,
                        Tag = typeof(int)
                    };
                else if (InputType == typeof(string))
                    ctl = new TextBox()
                    {
                        Text = string.Empty,
                        Size = new Size((int)(panel1.Size.Width * 0.8), 40),
                        Multiline = true,
                        Tag = typeof(string)
                    };
                else if (InputType == typeof(bool))
                    ctl = new CheckBox()
                    {
                        Checked = false,
                        Text = "啟用",
                        Tag = typeof(bool)
                    };
                else if (InputType.IsEnum)
                {
                    ComboBox cb = new ComboBox();
                    cb.Size = new Size((int)(panel1.Size.Width * 0.8), 20);
                    foreach (var v in InputType.GetEnumNames())
                        cb.Items.Add(v);
                    cb.Tag = InputType;
                    ctl = cb;
                }

                //設定名稱後放上
                if (ctl != null)
                {
                    ctl.Location = p;
                    ctl.Name = properity.Name;
                    panel1.Controls.Add(ctl);
                    p.Y += ctl.Margin.Bottom + ctl.Size.Height;
                }
                #endregion
            }

        }

        private void Script_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Script.SelectedIndices.Count > 1)
                toolStripStatusLabel2.Text = String.Format("最後行數{0,5},共選了{1,5}行"
                    , Script.SelectedIndices[Script.SelectedIndices.Count-1],
                    Script.SelectedIndices.Count
                    );
            else if (Script.SelectedIndex != -1)
                toolStripStatusLabel2.Text = String.Format("行數{0,5}",Script.SelectedIndex);
            else
                toolStripStatusLabel2.Text = "行數";
        }
        private void AddScript_Btn_Click(object sender, EventArgs e)
        {
            if (CommandList.SelectedIndex == -1)
                return;
            object[] input = new object[panel1.Controls.Count];
            int i = 0;
            foreach (Control ctrl in panel1.Controls)
            {
                Type transType = ctrl.Tag as Type;
                if (ctrl is Label)
                    input[i++] = (ctrl as Label).Text;
                else if (ctrl is NumericUpDown)
                {
                    if (transType == typeof(int))
                        input[i++] = (int)((ctrl as NumericUpDown).Value);
                    else if(transType == typeof(uint))
                        input[i++] = (uint)((ctrl as NumericUpDown).Value);
                }
                else if (ctrl is TextBox)
                    input[i++] = (ctrl as TextBox).Text;
                else if (ctrl is CheckBox)
                    input[i++] = (ctrl as CheckBox).Checked;
                else if (ctrl is ComboBox)
                {
                    Type t = (Type)ctrl.Tag;
                    if ((ctrl as ComboBox).SelectedIndex == -1)
                        return;
                    input[i++] = t.GetEnumValues().GetValue((ctrl as ComboBox).SelectedIndex);
                }
            }
            if (Script.SelectedIndex != -1)
            {
                Add_Command(CommandList.SelectedItem.ToString(), Script.SelectedIndex, input);
            }
            else
            {
                Add_Command(CommandList.SelectedItem.ToString(), 0, input);
                Script.SelectedIndex = 0;
            }

        }
        private void RemoveScript_Btn_Click(object sender, EventArgs e)
        {
            if(Script.SelectedItem != null && !(Script.SelectedItem is EmptyCommand))
            {
                int Index = Script.SelectedIndex;
                //cmds.Remove(Script.SelectedItem as ICommand);
                object[] Selected = new object[Script.SelectedItems.Count];
                Script.SelectedItems.CopyTo(Selected, 0);
                foreach (object o in Selected)
                    cmds.Remove(o as ICommand);
                if (Script.Items.Count != Index)
                    Script.SelectedIndex = Index;
                else
                    Script.SelectedIndex = Script.Items.Count - 1;
            }
        }
        private void CustomMenu_Load(object sender, EventArgs e)
        {
            this.Text = "自定義腳本";
            Refresh_ScriptList();
            KeyBoardHook.KeyDown += Listen_Recording_Start;
            KeyBoardHook.IsLowLevel = true;
            KeyBoardHook.Enable = true;
        }
        private void CustomMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Recording)
                Recording = false;
            KeyBoardHook.KeyDown -= Listen_Recording_Start;
            if (KeyBoardHook.Enable)
                KeyBoardHook.Enable = false;
            if (MouseHook.Enable)
                MouseHook.Enable = false;
        }

        // Menu Strip
        private void 滑鼠移動整合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = "1";
            if (Func.InputDialog.InputBox("滑鼠位移整合", "輸入允許的誤差角度(輸入單位為角度(0~359))", ref input) != DialogResult.OK)
                return;
            double dis;
            if(!Double.TryParse(input,out dis))
            {
                MessageBox.Show("輸入的值無法轉乘數字! [" + input + "]");
                return;
            }

            int optimization = MouseMoveOptimization(cmds, dis);
            MessageBox.Show(string.Format("整合了{0}個命令",optimization));
        }
        private void 清空腳本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmds.Count == 0)
                return;
            if (MessageBox.Show("確定要清空當前的腳本內容嗎?", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                cmds.Clear();
        }
        private void 統計腳本總暫停時間ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (var item in cmds)
                if (item is Pause)
                    sum += (int)(item as Pause).PauseTime;
                else if (item is hVkSerial)
                    sum += (item as hVkSerial).interval.Sum((ushort s) => s);
                else if (item is hMouseMove)
                    sum += (int)(item as hMouseMove).time;
                else if (item is hMouseMoveAbs)
                    sum += (int)(item as hMouseMoveAbs).time;
            MessageBox.Show(String.Format("腳本的估計運行時間:{0}", sum));
        }
        void Refresh_ScriptList()
        {
            ScriptList.Items.Clear();
            if (Directory.Exists(Const.ScriptDicPath))
            {
                foreach (string info in Directory.GetFiles(Const.ScriptDicPath,"*."+Const.ScriptFileExtension))
                {
                    ScriptList.Items.Add(info.Substring(info.IndexOf('\\')+1));
                }
            }
        }

        void SystemBinaryFormatterSerialize(string name,CommandCollection cc)
        {
            FileStream fs = new FileStream(name, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, cc);
            fs.Dispose();
        }
        bool SystemBinaryFormatterDeSerialize(string name,out CommandCollection cc,out string Fail_Reason)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            CommandCollection list = bf.Deserialize(fs) as CommandCollection;
            fs.Dispose();
            if (list == null)
            {
                Fail_Reason = "還原檔案失敗";
                cc = null;
                return false;
            }
            Fail_Reason = String.Empty;
            cc = list;
            return true;
        }

        //Button Layout Method
        private void SaveScript_Btn_Click(object sender, EventArgs e)
        {
            Save_Script(ScriptName_tb.Text + "." + Const.ScriptFileExtension,cmds,SystemBinaryFormatterSerialize);
            Refresh_ScriptList();
        }
        private void LoadScript_btn_Click(object sender, EventArgs e)
        {
            if (ScriptList.SelectedItem != null)
            {
                string fail_reason;
                if (!Read_Script(ScriptList.SelectedItem.ToString(), out fail_reason,SystemBinaryFormatterDeSerialize))
                {
                    MessageBox.Show(fail_reason,"讀取失敗",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            Refresh_ScriptList();
        }
        private void Rename_btn_Click(object sender, EventArgs e)
        {
            if (ScriptList.SelectedItem == null)
                return;
            //Get new name
            string NewName = "";
            if (Func.InputDialog.InputBox("重新命名", "輸入新名稱", ref NewName) != DialogResult.OK)
                return;
            string path = Const.ScriptDicPath + "\\" + ScriptList.SelectedItem.ToString();
            string NewPath = Const.ScriptDicPath + "\\" + NewName + "." + Const.ScriptFileExtension;
            FileInfo info = new FileInfo(path);
            info.CopyTo(NewPath);
            info.Delete();
            Refresh_ScriptList();
        }
        private void Remove_btn_Click(object sender, EventArgs e)
        {
            if (ScriptList.SelectedItem == null)
                return;
            string scriptName = ScriptList.SelectedItem.ToString();
            if (MessageBox.Show(String.Format("確定要刪除腳本 \"{0}\" 嗎",scriptName),"確認",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string path = Const.ScriptDicPath + "\\" + scriptName;
                FileInfo info = new FileInfo(path);
                info.Delete();
                Refresh_ScriptList();
            }
        }
        private void StartRecord_Btn_Click(object sender, EventArgs e)
        {
            StartRecord();
        }
        private void StopRecording_btn_Click(object sender, EventArgs e)
        {
            if (Recording)
                Recording = false;
        }
        void Listen_Recording_Start(object sender,KeyBoardHook.KeyBoardArgs e)
        {
            short get = Win32_BigEdit.WinMethod.GetKeyState(Win32_BigEdit.Enum.VirtualKeyShort.CONTROL);
            if (!Recording)
            {
                if ((get & 0x80) == 0x80 && e.VirtualKey == (short)VirtualKeyShort.F12)
                    StartRecord();
            }
            else
            { 
                if ((get & 0x80) == 0x80 && e.VirtualKey == (short)VirtualKeyShort.F11)
                    Recording = false;
            }
        }
        private void Sure_Btn_Click(object sender, EventArgs e)
        {
            string report;
            if (!LoopGoToSet(this.cmds, out report))
            {
                MessageBox.Show(report, "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        //暫存Func
        private void label2_Click(object sender, EventArgs e)
        {
            hVkSerial s = new hVkSerial();
            s.vks = new VirtualKeyShort[] { VirtualKeyShort.KEY_A, VirtualKeyShort.KEY_B, VirtualKeyShort.KEY_C };
            s.interval = new ushort[] { 0x8000 + 500, 0x8000 + 1000, 0x8000 + 1500 };
            cmds.Add(s);
        }

    }
}
