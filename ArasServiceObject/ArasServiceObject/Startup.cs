﻿using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ArasServiceObject.Startup))]
namespace ArasServiceObject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);   
            app.MapSignalR();
        }
    }
}
