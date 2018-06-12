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
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder listItemTag = null;
            for(int i = 1; i <= PageModel.TotalPages; i++)
            {
                listItemTag = new TagBuilder("li");
                listItemTag.AddCssClass("page-item");
                TagBuilder tag = new TagBuilder("a");
                tag.AddCssClass("page-link");
                tag.Attributes["href"] = urlHelper.Action(PageActions, new { page = i});
                tag.InnerHtml.Append(i.ToString());
                listItemTag.InnerHtml.AppendHtml(tag);
                // result.InnerHtml.AppendHtml(listItemTag);
                output.Content.AppendHtml(listItemTag);
            }
        }
    }
}