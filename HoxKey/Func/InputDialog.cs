using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace HoxKey.Func
{
    class InputDialog
    {
        public static DialogResult InputBox(out string input,string title,string msg,bool OkButton,bool CancelButton)
        {
            Form nf = new Form();
            nf.Size = new System.Drawing.Size(300, 100);
            nf.Text = title;
            nf.AutoSize = false;

            Point p = new Point(nf.Bounds.Left, nf.Bounds.Top);

            Label l = new Label();
            l.AutoSize = true;
            l.Text = msg;
            l.Location = p;
            p.Offset(0, l.Size.Height);
            p.Offset(0, l.Bounds.Top);

            TextBox tb = new TextBox();
            tb.Size = new System.Drawing.Size(250, 50);
            tb.Location = p;
            p.Offset(0, tb.Size.Height);
            p.Offset(0, tb.Bounds.Top);

            p = new Point(nf.Size.Width, nf.Size.Height);
            p.Offset(-nf.Bounds.Right, -nf.Bounds.Bottom);

            Button Cancel = new Button();
            Cancel.Size = new System.Drawing.Size(50, 30);
            Cancel.Click += (object sender, EventArgs e) => { nf.DialogResult = DialogResult.Cancel; };
            p.Offset(Cancel.Size.Width, Cancel.Size.Height);
            Cancel.Location = p;

            Button Ok = new Button();
            Ok.Size = new System.Drawing.Size(50, 30);
            Ok.Click += (object sender, EventArgs e) => { nf.DialogResult = DialogResult.OK; };
            p.Offset(Ok.Size.Width, 0);
            Ok.Location = p;

            nf.Controls.Add(l);
            nf.Controls.Add(tb);
            if(OkButton)
                nf.Controls.Add(Ok);
            if (CancelButton)
                nf.Controls.Add(Cancel);

            DialogResult dr = nf.ShowDialog();

            input = tb.Text;
            return dr;
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
