using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("markdown")]
    [HtmlTargetElement(Attributes = "markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await context.GetChildContentAsync();
            var lines = childContent
                .GetContent()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim());
            var content = string.Join(" ", lines);
            var parsedContent = CommonMark.CommonMarkConverter.Convert(content);

            output.Content.SetContentEncoded(parsedContent);

            output.Attributes.RemoveAll("markdown");
        }
    }
}
