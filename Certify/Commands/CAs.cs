﻿using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseAdmin.Domain;
using EnterpriseAdmin.DirectoryServices;
using EnterpriseAdmin.Lib;

namespace EnterpriseAdmin.Commands
{
    public class CAs : ICommand
    {
        public static string CommandName => "cas";
        private DirectoryServiceOperations _directoryService = new DirectoryServiceOperations();
        private bool skipWebServiceChecks;
        private bool hideAdmins;
        private bool showAllPermissions;
        private string? caArg;
        private string? domain;
        private string? directoryServer;

        public void Execute(Dictionary<string, string> arguments)
        {
            Console.WriteLine("[*] Action: List certificate authorities");

            showAllPermissions = arguments.ContainsKey("/showAllPermissions");
            skipWebServiceChecks = arguments.ContainsKey("/skipWebServiceChecks");
            hideAdmins = arguments.ContainsKey("/hideAdmins");

            if (arguments.ContainsKey("/ca"))
            {
                caArg = arguments["/ca"];
                if (!caArg.Contains("\\"))
                {
                    Console.WriteLine("[!] Warning: if using /ca format of SERVER\\CA-NAME, you may need to specify \\\\ for escaping purposes.\r\n");
                }
            }

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

            DisplayRootCAs();
            DisplayNtAuthCertificates();
            DisplayEnterpriseCAs();
        }

        private void DisplayRootCAs()
        {
            Console.WriteLine("\n\n[*] Root CAs\n");

            var rootCAs = _directoryService.GetRootCAs();
            if(rootCAs == null) throw new NullReferenceException("RootCAs are null");

            foreach (var ca in rootCAs)
            {
                if(ca.Certificates == null) continue;

                ca.Certificates.ForEach(cert =>
                {
                    DisplayUtil.PrintCertificateInfo(cert);
                    Console.WriteLine();
                });
            }
        }

        private void DisplayNtAuthCertificates()
        {
            Console.WriteLine("\n\n[*] NTAuthCertificates - Certificates that enable authentication:\n");

            var ntauth = _directoryService.GetNtAuthCertificates();

            if (ntauth.Certificates == null || !ntauth.Certificates.Any())
            {
                Console.WriteLine("    There are no NTAuthCertificates\n");
                return;
            }

            ntauth.Certificates.ForEach(cert =>
            {
                DisplayUtil.PrintCertificateInfo(cert);
                Console.WriteLine();
            });
        }

        private void DisplayEnterpriseCAs()
        {
            Console.WriteLine("\n[*] Enterprise/Enrollment CAs:\n");
            foreach (var ca in _directoryService.GetEnterpriseCAs(caArg))
            {
                DisplayUtil.PrintEnterpriseCaInfo(ca, hideAdmins, showAllPermissions);

                if (!skipWebServiceChecks)
                {
                    Console.WriteLine();
                    PrintCAWebServices(ca.GetWebServices());
                }

                Console.WriteLine("    Enabled Certificate Templates:");
                Console.WriteLine("        " + string.Join("\n        ", ca.Templates));
                Console.WriteLine("\n\n");
            }
        }

        private void PrintCAWebServices(CertificateAuthorityWebServices webServices)
        {
            var indent = "    ";
            var urlIndent = new string(' ', 36);
            if (webServices.LegacyAspEnrollmentUrls.Any())
            {
                var str = $"Legacy ASP Enrollment Website : " +
                          string.Join($"\n{urlIndent}", webServices.LegacyAspEnrollmentUrls);
                Console.WriteLine(indent + str);
            }

            if (webServices.EnrollmentWebServiceUrls.Any())
            {
                var str = $"Enrollment Web Service        : " +
                          string.Join($"\n{urlIndent}", webServices.EnrollmentWebServiceUrls);
                Console.WriteLine(indent + str);
            }

            if (webServices.EnrollmentPolicyWebServiceUrls.Any())
            {
                var str = $"Enrollment Policy Web Service : " +
                          string.Join($"\n{urlIndent}", webServices.EnrollmentPolicyWebServiceUrls);
                Console.WriteLine(indent + str);
            }

            if (webServices.NetworkDeviceEnrollmentServiceUrls.Any())
            {
                var str = $"NDES Web Service              : " +
                          string.Join($"\n{urlIndent}", webServices.NetworkDeviceEnrollmentServiceUrls);
                Console.WriteLine(indent + str);
            }
        }
    }
}
