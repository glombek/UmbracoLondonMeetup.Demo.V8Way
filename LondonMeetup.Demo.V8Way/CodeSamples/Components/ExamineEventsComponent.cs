using System;
using System.Text;
using Examine;
using Examine.Providers;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Examine;
using Umbraco.Web;

namespace LondonMeetup.Demo.V8Way.CodeSamples.Components
{
    public class ExamineEventsComponent : IComponent
    {
        private readonly IExamineManager _examineManager;

        private readonly IUmbracoContextFactory _umbracoContextFactory;
        public ExamineEventsComponent(IExamineManager examineManager, IUmbracoContextFactory umbracoContextFactory)
        {
            this._examineManager = examineManager;
            this._umbracoContextFactory = umbracoContextFactory;
        }

        /// <summary>Initializes the component.</summary>
        public void Initialize()
        {
            //get the index 
            if (!_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out IIndex index))
                return;

            //we need to cast because BaseIndexProvider contains the TransformingIndexValues event
            if (!(index is BaseIndexProvider indexProvider))
                throw new InvalidOperationException("Could not cast");

            //handler
            indexProvider.TransformingIndexValues += IndexProviderTransformingIndexValues;

            //add a new field definition to the index
            index.FieldDefinitionCollection.TryAdd(new FieldDefinition("blogDateLong", FieldDefinitionTypes.Long));

        }

        private void IndexProviderTransformingIndexValues(object sender, IndexingItemEventArgs e)
        {
            if (e.ValueSet.Category == IndexTypes.Content)
            {
                var combinedFields = new StringBuilder();

                foreach (var fieldValues in e.ValueSet.Values)
                {
                    foreach (var value in fieldValues.Value)
                    {
                        if (value != null)
                        {
                            combinedFields.AppendLine(value.ToString());
                        }
                    }
                }

                e.ValueSet.TryAdd("combinedField", combinedFields.ToString());

                //get node id
                if (int.TryParse(e.ValueSet.Id, out var nodeId))
                {
                    switch (e.ValueSet.ItemType)
                    {
                        case "blogpost": //check whether the node being published is blogpost
                            using (var umbracoContext = _umbracoContextFactory.EnsureUmbracoContext())
                            {
                                //get the content node
                                var contentNode = umbracoContext.UmbracoContext.Content.GetById(nodeId);
                                if (contentNode != null)
                                {
                                    //get the date
                                    var blogDate = contentNode.Value<DateTime>("blogDate");

                                    //set the value for the newly added field
                                    e.ValueSet.Set("blogDateLong", blogDate.Date.Ticks);
                                }
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>Terminates the component.</summary>
        public void Terminate()
        {
        }
    }
}