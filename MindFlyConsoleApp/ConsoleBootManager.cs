using Umbraco.Core;

namespace MindFlyConsoleApp
{
    public class ConsoleBootManager : CoreBootManager
    {
        public ConsoleBootManager(UmbracoApplicationBase umbracoApplication)
            : base(umbracoApplication)
        {

        }

        protected override void InitializeApplicationEventsResolver()
        {
            base.InitializeApplicationEventsResolver();
        }

        protected override void InitializeResolvers()
        {
            base.InitializeResolvers();
        }
    }
}
