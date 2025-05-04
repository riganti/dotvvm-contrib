﻿using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Ganss.XSS;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders the contents of <see cref="Html" /> property as html. It's sanitized (script tags stripped) using HtmlSanitized library (see NuGet and npm)
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class SanitizedHtmlLiteral : DotvvmControl
    {
        [MarkupOptions(AllowBinding = true, Required = true)]
        public string Html
        {
            get { return (string)GetValue(HtmlProperty); }
            set { SetValue(HtmlProperty, value); }
        }
        public static readonly DotvvmProperty HtmlProperty =
            DotvvmProperty.Register<string, SanitizedHtmlLiteral>(nameof(Html));

        public SanitizedHtmlLiteral()
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            if (HasValueBinding(HtmlProperty))
                context.ResourceManager.AddRequiredResource("dotvvm.contrib.SanitizedHtmlLiteral");
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var valueBinding = GetValueBinding(HtmlProperty);
            if (valueBinding is object)
            {
                writer.WriteKnockoutDataBindComment("dotvvm-contrib-SanitizedHtmlLiteral", valueBinding.GetKnockoutBindingExpression(this));
            }


            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(Html);
            writer.WriteUnencodedText(sanitized);

            if (valueBinding is object)
            {
                writer.WriteKnockoutDataBindEndComment();
            }
        }
    }
}
