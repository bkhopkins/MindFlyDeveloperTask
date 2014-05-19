using System;
using Umbraco.Core;

namespace MindFlyConsoleApp
{
    public class ConsoleApplicationBase : UmbracoApplicationBase
    {
        protected override IBootManager GetBootManager()
        {
            return new ConsoleBootManager(this);
        }

        public void Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
        }
    }
}
