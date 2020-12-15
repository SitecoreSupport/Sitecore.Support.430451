namespace Sitecore.XA.Foundation.Mvc.Wrappers
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Presentation;
    using Sitecore.XA.Foundation.Mvc;
    using Sitecore.XA.Foundation.Mvc.Wrappers;
    using Sitecore.XA.Foundation.Presentation.Services;
    using Sitecore.XA.Foundation.SitecoreExtensions.Interfaces;

    public class Rendering : IRendering
    {
        private Parameters _parameters;

        private readonly Sitecore.Mvc.Presentation.Rendering _sitecoreRendering;

        public RenderingProperties Properties => _sitecoreRendering.Properties;

        public Item Item
        {
            get
            {
                Item item = DataSourceItem;
                if (item == null)
                {
                    RenderingModel obj = _sitecoreRendering.Model as RenderingModel;
                    if (obj == null)
                    {
                        return null;
                    }
                    item = obj.PageItem;
                }
                return item;
            }
        }

        public Item DataSourceItem
        {
            get
            {
                if (!string.IsNullOrEmpty(_sitecoreRendering.DataSource))
                {
                    return _sitecoreRendering.Item;
                }
                return PlaceholderDatasourceContext.CurrentOrNull?.ContextItem;
            }
        }

        public string DataSource => _sitecoreRendering.DataSource;

        public virtual IParameters Parameters => _parameters ?? (_parameters = new Parameters(_sitecoreRendering.Parameters));

        public string Name
        {
            get
            {
                if (_sitecoreRendering?.RenderingItem != null)
                {
                    return _sitecoreRendering.RenderingItem.Name;
                }
                return string.Empty;
            }
        }

        public ID SnippetId
        {
            get
            {
                string id = _sitecoreRendering.Properties["sid"];
                if (ID.IsID(id))
                {
                    return new ID(id);
                }
                return null;
            }
        }

        public ID UniqueId
        {
            get
            {
                string id = Properties["uid"];
                if (ID.IsID(id))
                {
                    return new ID(id);
                }
                return null;
            }
        }

        public string UniqueIdString
        {
            get
            {
                if (!(UniqueId == (ID)null))
                {
                    return UniqueId.ToString();
                }
                return string.Empty;
            }
        }

        public bool IsFromSnippet => !ID.IsNullOrEmpty(SnippetId);

        public string OriginalDataSource => Properties["ods"];

        public ID RenderingId
        {
            get
            {
                string id = Properties["id"];
                if (ID.IsID(id))
                {
                    return new ID(id);
                }
                return null;
            }
        }

        public string RenderingCssClass
        {
            get
            {
                string text = _sitecoreRendering.RenderingItem.InnerItem[Templates.ExtendedOptions.Fields.RenderingCssClass];
                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
                return null;
            }
        }

        public string RenderingViewPath
        {
            get
            {
                string text = _sitecoreRendering.RenderingItem.InnerItem[Templates.ExtendedOptions.Fields.RenderingViewPath];
                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
                return null;
            }
        }

        public string ControllerType
        {
            get
            {
                string text = _sitecoreRendering.RenderingItem.InnerItem[Templates.ControllerRendering.Fields.Controller];
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }
                return text;
            }
        }

        public Rendering()
        {
            RenderingContext currentOrNull = RenderingContext.CurrentOrNull;
            _sitecoreRendering = ((currentOrNull != null) ? currentOrNull.Rendering : new Sitecore.Mvc.Presentation.Rendering());
        }

        public Rendering(Sitecore.Mvc.Presentation.Rendering sitecoreRendering)
        {
            _sitecoreRendering = sitecoreRendering;
        }
    }
}