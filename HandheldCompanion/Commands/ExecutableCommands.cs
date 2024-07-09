﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HandheldCompanion.Commands
{
    [Serializable]
    public class ExecutableCommands : ICommands
    {
        public string Path { get; set; } = string.Empty;
        public string Arguments { get; set; } = string.Empty;

        public ExecutableCommands()
        {
            base.commandType = CommandType.Executable;

            base.Name = Properties.Resources.Hotkey_Executable;
            base.Description = Properties.Resources.Hotkey_ExecutableDesc;
            base.Glyph = "\ue756";
            base.OnKeyUp = true;
        }

        public override void Execute(bool IsKeyDown, bool IsKeyUp)
        {
            if (!File.Exists(this.Path))
                return;

            Task.Run(() =>
            {
                // Create a new instance of ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = this.Path,
                    Arguments = this.Arguments
                };

                // Start the process with the startInfo configuration
                Process process = new() { StartInfo = startInfo };
                process.Start();
            });

            base.Execute(IsKeyDown, IsKeyUp);
        }

        public override object Clone()
        {
            ExecutableCommands commands = new()
            {
                commandType = this.commandType,
                Name = this.Name,
                Description = this.Description,
                Glyph = this.Glyph,
                OnKeyUp = this.OnKeyUp,
                OnKeyDown = this.OnKeyDown,

                // specific
                Path = this.Path,
                Arguments = this.Arguments
            };

            return commands;
        }
    }
}