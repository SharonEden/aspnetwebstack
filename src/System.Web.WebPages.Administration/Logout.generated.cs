﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace System.Web.WebPages.Administration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.WebPages.Html;
    
    [System.Web.WebPages.PageVirtualPathAttribute("~/Logout.cshtml")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorSingleFileGenerator", "1.0.0.0")]
    public class Logout_cshtml : System.Web.WebPages.WebPage
    {
#line hidden

                    // Resolve package relative syntax
                    // Also, if it comes from a static embedded resource, change the path accordingly
                    public override string Href(string virtualPath, params object[] pathParts) {
                        virtualPath = ApplicationPart.ProcessVirtualPath(GetType().Assembly, VirtualPath, virtualPath);
                        return base.Href(virtualPath, pathParts);
                    }
        public Logout_cshtml()
        {
        }
        protected System.Web.HttpApplication ApplicationInstance
        {
            get
            {
                return ((System.Web.HttpApplication)(Context.ApplicationInstance));
            }
        }
        public override void Execute()
        {


WriteLiteral("\r\n\r\n");


      
    // Delete the admin auth cookie
    AdminSecurity.DeleteAuthCookie(Response);

    // Redirect home
    SiteAdmin.RedirectToHome(Response);



        }
    }
}
#pragma warning restore 1591
