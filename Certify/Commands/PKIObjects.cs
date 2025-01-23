using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using EnterpriseAdmin.Domain;
using EnterpriseAdmin.DirectoryServices;

namespace EnterpriseAdmin.Commands
{
    public class PKIObjects : ICommand
    {
        public static string CommandName => "pkiobjects";
        private DirectoryServiceOperations _directoryService = new DirectoryServiceOperations();
        private bool hideAdmins;
        private string? domain;
        private string? directoryServer;

        public void Execute(Dictionary<string, string> arguments)
        {
            Console.WriteLine("[*] Action: List PKI object controllers");

            hideAdmins = !arguments.ContainsKey("/showAdmins");

            if (arguments.ContainsKey("/domain"))
            {
                domain = arguments["/domain"];
                if (!domain.Contains("."))
                {
                    Console.WriteLine("[!] /domain:X must be a FQDN");
                    return;
                }
            }

            if (arguments.ContainsKey("/server"))
            {
                directoryServer = arguments["/server"];
            }

            _directoryService = new DirectoryServiceOperations(new DirectorySearchOptions()
            {
                Domain = domain,
                DirectoryServer = directoryServer
            });

            Console.WriteLine($"[*] Using the search base '{_directoryService.ConfigurationPath}'");

            DisplayPKIObjectControllers();
        }

        private void DisplayPKIObjectControllers()
        {
            Console.WriteLine("\n[*] PKI Object Controllers:");
            var pkiObjects = _directoryService.GetPKIObjects();

            DisplayUtil.PrintPKIObjectControllers(pkiObjects, hideAdmins);
        }
    }
}
