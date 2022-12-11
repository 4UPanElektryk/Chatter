using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.Client.Transfer;
using System.Windows.Forms;

namespace Chatter.Client
{
    public class MsgRenderer
    {
        public static RichTextBox RederMsgs(RichTextBox textBox, List<UMsg> msgs)
        {
            textBox.Text = "";
            foreach (UMsg item in msgs)
            {
                textBox.Text += item._Sent.ToString("\ndd.MM.yyyy HH:mm:ss") + "\n";
                textBox.Text += item._UserName + "\n";
                int start = textBox.Text.Length;
                textBox.Text += string.Join("\n", item._Message);
                int len = textBox.Text.Length - start;
                textBox.Select(start,len);
                //textBox.sel
                textBox.SelectionColor = item._Color;
                textBox.Select(0, 0);
            }
            return textBox;
        }
    }
}
