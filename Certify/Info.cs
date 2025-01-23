using System;

namespace EnterpriseAdmin
{
    public static class Info
    {
        public static void ShowLogo()
        {
            Console.WriteLine("\r\n    ____  _               __                     ___       __          _     ");
            Console.WriteLine("   / __ \\(_)_______  ____/ /_____  _______  __/   | ____/ /___ ___  (_)___ ");
            Console.WriteLine("  / / / / / ___/ _ \\/ __  / ___/ / / / __ \\/ _/ /| |/ __  / __ `__ \\/ / __ \\");
            Console.WriteLine(" / /_/ / / /  /  __/ /_/ / /  / /_/ / /_/ / // ___ / /_/ / / / / / / / / / /");
            Console.WriteLine("/_____/_/_/   \\___/\\__,_/_/   \\__, /\\____/_//_/  |_\\__,_/_/ /_/ /_/_/_/ /_/ ");
            Console.WriteLine("                              /____/                                           ");
            Console.WriteLine($"  v{EnterpriseAdmin.Version.version}\r\n");
        }

        public static void ShowUsage()
        {
            var usage = @"
  List information about all registered Certificate Authorities:

    DirectoryAdmin.exe cas [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List all enabled certificate templates:

    DirectoryAdmin.exe enumerate [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List certificate templates with specific configurations:

    DirectoryAdmin.exe enumerate /filter [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List certificate templates accessible by current user:

    DirectoryAdmin.exe enumerate /currentuser [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List enabled certificate templates with specific enrollment settings:

    DirectoryAdmin.exe enumerate /enrollment [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List enabled certificate templates for client authentication:

    DirectoryAdmin.exe enumerate /clientauth [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local] [/quiet]

  List all enabled certificate templates with full permission details:

    DirectoryAdmin.exe enumerate /showAllPermissions /quiet [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local]

  Export certificate template information to JSON:

    DirectoryAdmin.exe enumerate /json /outfile:C:\Temp\out.json [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local]

  List access control information for PKI objects:

    DirectoryAdmin.exe pkiobjects [/domain:domain.local] [/server:server.domain.local] [/quiet]

  Request a new certificate using current user context:

    DirectoryAdmin.exe request /ca:SERVER\ca-name [/subject:X] [/template:Y] [/install]

  Request a new certificate using machine context:

    DirectoryAdmin.exe request /ca:SERVER\ca-name /machine [/subject:X] [/template:Y] [/install]

  Request a certificate with alternate name (if template allows):

    DirectoryAdmin.exe request /ca:SERVER\ca-name /template:Y /altname:USER

  Request a certificate with alternate name and SID (if template allows):

    DirectoryAdmin.exe request /ca:SERVER\ca-name /template:Y /altname:USER /sid:S-1-5-21-...

  Request a certificate with alternate name and URL (if template allows):

    DirectoryAdmin.exe request /ca:SERVER\ca-name /template:Y /altname:USER /url:tag:domain.com,2024-01-01:sid:S-1-5-21-...

  Request a certificate on behalf of another user (requires enrollment agent certificate):

    DirectoryAdmin.exe request /ca:SERVER\ca-name /template:Y /onbehalfof:DOMAIN\USER /enrollcert:C:\Temp\enroll.pfx [/enrollcertpw:PASSWORD]

  Download a pending certificate:

    DirectoryAdmin.exe download /ca:SERVER\ca-name /id:X [/install] [/machine]
";
            Console.WriteLine(usage);
        }
    }
}
