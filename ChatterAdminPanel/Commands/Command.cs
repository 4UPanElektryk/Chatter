﻿namespace Chatter.AdminPanel.Commands
{
    public class Command
    {
        public string Name;
        public Command(string name)
        {
            Name = name;
        }
        public virtual bool Execute(string text)
        {
            return true;
        }
    }
}
