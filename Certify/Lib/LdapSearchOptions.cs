namespace EnterpriseAdmin.DirectoryServices
{
    class DirectorySearchOptions
    {
        public DirectorySearchOptions()
        {
            Domain = null;
            DirectoryServer = null;
            AuthenticationType = System.DirectoryServices.AuthenticationTypes.Secure |
                               System.DirectoryServices.AuthenticationTypes.Sealing |
                               System.DirectoryServices.AuthenticationTypes.Signing;
            Credential = null;
        }
        public string? Domain { get; set; }
        public string? DirectoryServer { get; set; }
        public System.DirectoryServices.AuthenticationTypes AuthenticationType { get; set; }
        public System.Net.NetworkCredential? Credential { get; set; }
    }
}
