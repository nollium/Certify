using System.Collections.Generic;

namespace EnterpriseAdmin.Commands
{
    public interface ICommand
    {
        void Execute(Dictionary<string, string> arguments);
    }
}
