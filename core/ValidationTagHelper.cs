using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace aspnet_markdown.core
{
  [HtmlTargetElement("input", Attributes = "asp-for")]
  [HtmlTargetElement("textarea", Attributes = "asp-for")]
  [HtmlTargetElement("select", Attributes = "asp-for")]
  public class ValidationTagHelper : TagHelper
  {
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      base.Process(context, output);

      var defaultMetadata = For.ModelExplorer.Metadata as DefaultModelMetadata;
      var attributes = defaultMetadata?.Attributes.Attributes;

      const string required = nameof(required);
      const string minlength = nameof(minlength);
      const string maxlength = nameof(maxlength);

      var minLengthAdded = false;
      var maxLengthAdded = false;

      var isDate = false;

      var q =
        from x in attributes where x is DataTypeAttribute select x;
      
      var q0 =
        from x in q
        where ((DataTypeAttribute)x).DataType == DataType.DateTime
        select x;

      var isDate0 = q0.Any();

      for (var i = 0; i < attributes?.Count; i++)
      {
        switch (attributes[i])
        {
          case DataTypeAttribute attribute:

            if (attribute.DataType == DataType.Date)
            {
              isDate = true;

              output.Attributes.Add(new TagHelperAttribute("type", "date"));
              
              // value, min, max
              
            }
            
            break;
          
          case RegularExpressionAttribute attribute:
            output.Attributes.Add(new TagHelperAttribute("pattern", attribute.Pattern));
            
            break;
          case CompareAttribute attribute:
            output.Attributes.Add(new TagHelperAttribute("data-html5-compare", attribute.OtherProperty));
            
            break;
          case EditableAttribute attribute:
            if (!attribute.AllowEdit)
            {
              output.Attributes.Add(new TagHelperAttribute("readonly", string.Empty, HtmlAttributeValueStyle.Minimized));
            }
            
            break;
          
          case RequiredAttribute attribute:
            output.Attributes.Add(new TagHelperAttribute(required, string.Empty, HtmlAttributeValueStyle.Minimized));
            break;
          
          case DisplayAttribute attribute:

            if (isDate)
            {
              attribute.Prompt = DateTime.Today.ToShortDateString();
            }

            break;
          
          case StringLengthAttribute attribute:
            if (attribute.MinimumLength > 0 && context.AllAttributes[minlength] == null && !minLengthAdded)
            {
              output.Attributes.Add(new TagHelperAttribute(minlength, attribute.MinimumLength));
              minLengthAdded = true;
            }
            
            if (attribute.MaximumLength > 0 && context.AllAttributes[maxlength] == null && !maxLengthAdded)
            {
              output.Attributes.Add(new TagHelperAttribute(maxlength, attribute.MaximumLength));
              maxLengthAdded = true;
            }
            
            break;
          
          case MinLengthAttribute attribute:
            if (attribute.Length > 0 && context.AllAttributes[minlength] == null && !minLengthAdded)
            {
              output.Attributes.Add(new TagHelperAttribute(minlength, attribute.Length));
              minLengthAdded = true;
            }
            
            break;  

          case MaxLengthAttribute attribute:
            if (attribute.Length > 0 && context.AllAttributes[maxlength] == null && !maxLengthAdded)
            {
              output.Attributes.Add(new TagHelperAttribute(maxlength, attribute.Length));
              maxLengthAdded = true;
            }
            
            break;
         
          case RemoteAttribute attribute:

            var actionName = attribute.Controller + "/" + attribute.Action;
            
            output.Attributes.Add(new TagHelperAttribute("data-html5-action", actionName));

            break;
          
        }
        
      }
      
      while (output.Attributes.Any(x => x.Name.StartsWith("data-val")))
      {
        output.Attributes.Remove(output.Attributes.First(x => x.Name.StartsWith("data-val")));
      }
      
      if (output.Attributes["value"]?.Value?.ToString() == string.Empty)
      {
        output.Attributes.Remove(output.Attributes["value"]);
      }
      
    }
    
  }
  
}