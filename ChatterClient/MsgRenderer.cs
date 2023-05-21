using Chatter.Client.Transfer;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chatter.Client
{
    public class MsgRenderer
    {
        public static RichTextBox RederMsgs(RichTextBox textBox, SMsg[] msgs)
        {
            if (msgs == null)
            {
                return textBox;
            }
            #region Rendering Text
            textBox.Text = "";
            List<Loc> collocs = new List<Loc>();
            List<Loc> Bolds = new List<Loc>();
            foreach (SMsg item in msgs)
            {
                int prstart = textBox.Text.Length;
                textBox.Text += item.Sent.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss") + "\n";
                textBox.Text += item.UserName + "\n";
                int start = textBox.Text.Length;
                Bolds.Add(new Loc
                {
                    start_index = prstart,
                    length = textBox.Text.Length - prstart,
                });
                textBox.Text += string.Join("\n", item.Message) + "\n";
                int len = textBox.Text.Length - start;
                collocs.Add(new Loc
                {
                    start_index = start,
                    length = len,
                    color = item.Color,
                });
            }
            #endregion
            #region Rendering Color
            foreach (Loc item in collocs)
            {
                textBox.Select(item.start_index, item.length);
                textBox.SelectionColor = item.color;
            }
            #endregion
            #region Rendering Bold
            Font font = textBox.Font;
            font = new Font(font,FontStyle.Bold);
            foreach (Loc item in Bolds)
            {
                textBox.Select(item.start_index, item.length);
                textBox.SelectionFont = font;
            }
            textBox.Select(textBox.Text.Length, 0);
            textBox.Select(textBox.Text.Length, 0);
            #endregion
            return textBox;
        }
    }
    public struct Loc
    {
        public int start_index;
        public int length;
        public Color color;
    }
}
