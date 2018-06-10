using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using Xunit;
using SportStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SportStore.Tests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup (f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new Models.ViewModels.PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            var ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(ctx, output);

            Assert.Equal(@"<href=""Test/Page1"">1</a>"
                        + @"<href=""Test/Page2"">2</a>"
                        + @"<href=""Test/Page3"">3</a>",
                        output.Content.GetContent());
            
        }
    }
}