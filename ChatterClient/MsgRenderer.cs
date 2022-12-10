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
        public static RichTextBox RederMsgs(RichTextBox textBox, List<Msg> msgs)
        {
            textBox.Text = "";
            int length = 0;
            foreach (Msg item in msgs)
            {
                item
                textBox.Find(length);
            }
            return textBox;
        }
    }
}
