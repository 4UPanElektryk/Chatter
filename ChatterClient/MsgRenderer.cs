using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.Client.Transfer;
using System.Windows.Forms;
using System.Drawing;

namespace Chatter.Client
{
    public class MsgRenderer
    {
        public static RichTextBox RederMsgs(RichTextBox textBox, List<UMsg> msgs)
        {
            textBox.Text = "";
            List<Loc> collocs = new List<Loc>();
            foreach (UMsg item in msgs)
            {
                textBox.Text += item._Sent.ToString("dd.MM.yyyy HH:mm:ss") + "\n";
                textBox.Text += item._UserName + "\n";
                int start = textBox.Text.Length;
                textBox.Text += string.Join("\n", item._Message) + "\n";
                int len = textBox.Text.Length - start;
                collocs.Add(new Loc
                {
                    start_index = start,
                    length = len,
                    color = item._Color,
                });
            }
            foreach (Loc item in collocs)
            {
                textBox.Select(item.start_index, item.length);
                textBox.SelectionColor = item.color;
            }
            textBox.Select(textBox.Text.Length, 0);
            return textBox;
        }
    }
    public class Loc
    {
        public int start_index;
        public int length;
        public Color color;
    }
}
