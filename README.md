# DirectoryAdmin

DirectoryAdmin is a C# tool for managing and auditing Active Directory Certificate Services (AD CS) in enterprise environments.

## Features

- Enumerate certificate templates and authorities
- Request and manage certificates
- Audit certificate template configurations
- Review certificate authority permissions
- Manage certificate deployments

## Usage

Basic commands:

```powershell
# List certificate templates
DirectoryAdmin.exe enumerate [/ca:SERVER\ca-name] [/domain:domain.local] [/server:server.domain.local]

# Request a certificate
DirectoryAdmin.exe request /ca:SERVER\ca-name /template:TemplateName [/subject:X] [/altname:Y]

# Download a certificate
DirectoryAdmin.exe download /ca:SERVER\ca-name /id:X [/install]
```

## Requirements

- .NET Framework 4.0 or later
- Active Directory environment
- Appropriate permissions for certificate operations

## Building

DirectoryAdmin is built with Visual Studio 2019 or later:

1. Open the solution file in Visual Studio
2. Select "Release" configuration
3. Build the solution

## License

Copyright (c) 2024. All rights reserved.

