using System.Configuration;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Logging;
using Imuless.Core;
using Imuless.Models;
using Imuless.Extensions;

namespace Imuless.Events
{
    public class ImulessEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            PublishingStrategy.Published += PublishingStrategy_Published;
        }

        void PublishingStrategy_Published(IPublishingStrategy sender, global::Umbraco.Core.Events.PublishEventArgs<IContent> e)
        {
            CompileCss(e);
        }

        void CompileCss(global::Umbraco.Core.Events.PublishEventArgs<IContent> e)
        {
            LogHelper.Info<ImulessEvents>("Begin Imuless Publish Event...");

            foreach (var content in e.PublishedEntities)
            {
                if (content.ContentType.Alias == ConfigurationManager.AppSettings["imuless:doctypeAlias"])
                {
                    LogHelper.Info<ImulessEvents>("Begin compile: " + content.Id);
                    var model = ImulessModel.Deserialize(content, ConfigurationManager.AppSettings["imuless:propertyAlias"]);

                    LogHelper.Info<ImulessEvents>(model.Theme);

                    ImulessCore.Compile(model, content.GetDomains());
                }
            }
        }
    }
}