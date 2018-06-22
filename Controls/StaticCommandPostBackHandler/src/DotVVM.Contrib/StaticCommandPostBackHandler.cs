using System;
using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Executes static command before the postback.
    /// </summary>
    public class StaticCommandPostBackHandler : PostBackHandler
    {
        protected override string ClientHandlerName => "StaticCommandPostBackHandler";

        protected override Dictionary<string, object> GetHandlerOptions()
        {
            return new Dictionary<string, object>()
            {
                ["beforePostBack"] = GenerateCommandFunction(nameof(BeforePostBackProperty), GetCommandBinding(BeforePostBackProperty)),
            };
        }

        /// <summary>
        /// Gets or sets the command that executes before the postback.
        /// </summary>
        [MarkupOptions(AllowBinding = true, Required = true)]
        public Command BeforePostBack
        {
            get { return (Command)GetValue(BeforePostBackProperty); }
            set { SetValue(BeforePostBackProperty, value); }
        }
        public static readonly DotvvmProperty BeforePostBackProperty
            = DotvvmProperty.Register<string, StaticCommandPostBackHandler>(c => c.BeforePostBack, null);

        public StaticCommandPostBackHandler(ResourceManager resourceManager)
        {
            resourceManager.AddRequiredResource("dotvvm.contrib.StaticCommandPostBackHandler");
        }

        private object GenerateCommandFunction(string propertyName, ICommandBinding commandBinding)
        {
            var control = ((HtmlGenericControl)this.Parent);

            var postBackOptions = new PostbackScriptOptions(elementAccessor: "$element",
                commandArgs: CodeParameterAssignment.FromIdentifier("ar"), 
                allowPostbackHandlers: false);

            return $"(function(){{var ar=[].slice.call(arguments);return {KnockoutHelper.GenerateClientPostBackExpression(propertyName, commandBinding, control, postBackOptions)};}})";
        }
    }
}
