using System;
using System.Collections.Generic;
using EnterpriseAdmin.Commands;

namespace EnterpriseAdmin
{
    public class CommandCollection
    {
        private readonly Dictionary<string, Func<ICommand>> _availableCommands = new Dictionary<string, Func<ICommand>>();

        // How To Add A New Command:
        //  1. Create your command class in the Commands Folder
        //      a. That class must have a CommandName static property that has the Command's name
        //              and must also Implement the ICommand interface
        //      b. Put the code that does the work into the Execute() method
        //  2. Add an entry to the _availableCommands dictionary in the Constructor below.

        public CommandCollection()
        {
            _availableCommands.Add(CAs.CommandName, () => new CAs());
            _availableCommands.Add(Request.CommandName, () => new Request());
            _availableCommands.Add(Download.CommandName, () => new Download());
            _availableCommands.Add(EnumerateTemplates.CommandName, () => new EnumerateTemplates());
            _availableCommands.Add(PKIObjects.CommandName, () => new PKIObjects());
        }

        public bool ExecuteCommand(Dictionary<string, string> arguments)
        {
            if (arguments == null || arguments.Count == 0)
                return false;

            var commandName = arguments.ContainsKey("command") ? arguments["command"] : "";

            if (string.IsNullOrEmpty(commandName) || !_availableCommands.ContainsKey(commandName))
                return false;

            // Create the command object
            var command = _availableCommands[commandName].Invoke();

            // Execute it with the arguments
            command.Execute(arguments);

            return true;
        }
    }
}
