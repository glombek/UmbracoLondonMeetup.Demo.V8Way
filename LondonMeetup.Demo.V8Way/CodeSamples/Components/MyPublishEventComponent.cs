using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace LondonMeetup.Demo.V8Way.CodeSamples.Components
{
    public class MyPublishEventComponent : IComponent
    {
        //this is Umbraco.Core.Logging, I had a rip-my-hair-apart moment!!!!!
        private readonly ILogger _logger;

        public MyPublishEventComponent(ILogger logger)
        {
            this._logger = logger;
        }


        // initialize: runs once when Umbraco starts
        public void Initialize()
        {
            ContentService.Saved += this.ContentService_Saved;
        }

        // terminate: runs once when Umbraco stops
        public void Terminate()
        { }

        private void ContentService_Saved(IContentService sender, ContentSavedEventArgs e)
        {
            foreach (var content in e.SavedEntities
                // Check if the content item type has a specific alias
                .Where(c => c.ContentType.Alias.InvariantEquals("Product")))
            {
                // Do something if the content is using the MyContentType doctype
                this._logger.Info<MyPublishEventComponent>("{content} has been saved and event fired!", content.Name);


                // message template stored with the value of content.Name as a part of message template, not useful 
                this._logger.Info<MyPublishEventComponent>($"{content.Name} has been saved and event fired");
            }
        }
    }
}