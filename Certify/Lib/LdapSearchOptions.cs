namespace Certify.Lib
{
    class LdapSearchOptions
    {
        public LdapSearchOptions()
        {
            Domain = null;
            LdapServer = null;
            AuthenticationType = System.DirectoryServices.AuthenticationTypes.Secure;
            Credential = null;
        }
        public string? Domain { get; set; }
        public string? LdapServer { get; set; }
        public System.DirectoryServices.AuthenticationTypes AuthenticationType { get; set; }
        public System.Net.NetworkCredential? Credential { get; set; }
    }
}
