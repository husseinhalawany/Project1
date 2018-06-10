using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MangamentProject.Models;

namespace MangamentProject
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {

            OAuthWebSecurity.RegisterTwitterClient(
                            consumerKey: "hlCG7KZTmfkSezDG9HPUeBQYS ",
                            consumerSecret: "SWANIH2WBYqo3aHLWGDWLXtKuE6Rk7rKMx5vSs8ULDCpq1XHQ4");

            OAuthWebSecurity.RegisterFacebookClient(
                   appId: "969193766480070",
                   appSecret: "5677cbb21ef90465696c1662d8c4df45");
            
        }
    }
}
