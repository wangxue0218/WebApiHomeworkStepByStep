using System;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace MyApplication
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
            new Bootstrapper().Init(httpConfiguration);
        }

    }
}