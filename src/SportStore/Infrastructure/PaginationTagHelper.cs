using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStore.Models.ViewModels;

namespace SportStore.Infrastructure
{
    [HtmlTargetElement("ul", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        public PaginationTagHelper(IUrlHelperFactory  urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext {get; set;}
        public PagingInfo PageModel { get; set; }
        public string PageActions { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix= "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder listItemTag = null;
            for(int i = 1; i <= PageModel.TotalPages; i++)
            {
                listItemTag = new TagBuilder("li");

                if(PageModel.CurrentPage == i)
                {
                    listItemTag.AddCssClass("page-item active");
                } 
                else listItemTag.AddCssClass("page-item");

                TagBuilder tag = new TagBuilder("a");
                tag.AddCssClass("page-link");
                tag.Attributes["href"] = urlHelper.Action(PageActions, PageUrlValues);
                tag.InnerHtml.Append(i.ToString());
                listItemTag.InnerHtml.AppendHtml(tag);
                output.Content.AppendHtml(listItemTag);
            }
        }
    }
}