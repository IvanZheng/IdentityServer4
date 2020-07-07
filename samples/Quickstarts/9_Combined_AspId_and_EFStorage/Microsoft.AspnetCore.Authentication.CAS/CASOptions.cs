using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Authentication.CAS
{
    public class CASOptions: OAuthOptions
    {
        public CASOptions()
        {
            CallbackPath = new PathString("/signin-cas");
        }
    }
}
