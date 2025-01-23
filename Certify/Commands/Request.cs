using System;
using System.Collections.Generic;
using EnterpriseAdmin.Services;

namespace EnterpriseAdmin.Commands
{
    public class Request : ICommand
    {
        public static string CommandName => "request";

        public void Execute(Dictionary<string, string> arguments)
        {
            Console.WriteLine("[*] Action: Request a Certificate");

            var CA = "";
            var subject = "";
            var altName = "";
            var url = "";
            var sidExtension = "";
            var template = "User";
            var machineContext = false;
            var install = false;
            var domain = "";
            var username = "";
            var password = "";
            var ldapServer = "";

            if (arguments.ContainsKey("/ca"))
            {
                CA = arguments["/ca"];
                if (!CA.Contains("\\"))
                {
                    Console.WriteLine("[X] /ca format of SERVER\\CA-NAME required, you may need to specify \\\\ for escaping purposes");
                    return;
                }
            }
            else
            {
                Console.WriteLine("[X] A /ca:CA is required! (format SERVER\\CA-NAME)");
                return;
            }

            if (arguments.ContainsKey("/domain"))
            {
                domain = arguments["/domain"];
            }

            if (arguments.ContainsKey("/username"))
            {
                username = arguments["/username"];
            }

            if (arguments.ContainsKey("/password"))
            {
                password = arguments["/password"];
            }

            if (arguments.ContainsKey("/server"))
            {
                ldapServer = arguments["/server"];
            }

            // If any auth parameter is provided, verify all required auth parameters are present
            if (!string.IsNullOrEmpty(domain) || !string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(ldapServer))
                {
                    Console.WriteLine("[X] When using authentication, all of /domain, /username, /password, and /server are required!");
                    return;
                }
            }

            if (arguments.ContainsKey("/template"))
            {
                template = arguments["/template"];
            }

            if (arguments.ContainsKey("/subject"))
            {
                subject = arguments["/subject"];
            }

            if (arguments.ContainsKey("/altname"))
            {
                altName = arguments["/altname"];
            }

            if (arguments.ContainsKey("/url"))
            {
                url = arguments["/url"];
            }

            if (arguments.ContainsKey("/sid"))
            {
                sidExtension = arguments["/sid"];
            }

            if (arguments.ContainsKey("/machine") || arguments.ContainsKey("/computer"))
            {
                machineContext = true;
            }

            if (arguments.ContainsKey("/install"))
            {
                install = true;
            }

            if (arguments.ContainsKey("/onbehalfof"))
            {
                if (!arguments.ContainsKey("/enrollcert"))
                {
                    Console.WriteLine("[X] /enrollcert:X is required when requesting a certificate on behalf of another user!");
                    return;
                }

                var enrollCertPassword = "";
                if (arguments.ContainsKey("/enrollcertpw"))
                {
                    enrollCertPassword = arguments["/enrollcertpw"];
                }

                CertificateService.RequestCertOnBehalf(CA, template, arguments["/onbehalfof"], arguments["/enrollcert"], enrollCertPassword, machineContext);
            }
            else
            {
                CertificateService.RequestCert(CA, machineContext, template, subject, altName, url, sidExtension, install, domain, username, password, ldapServer);
            }
        }
    }
}
