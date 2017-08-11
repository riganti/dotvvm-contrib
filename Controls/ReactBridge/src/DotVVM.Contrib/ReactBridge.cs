using System;
using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Utils;
using Newtonsoft.Json;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a react control
    /// </summary>
    public class ReactBridge : HtmlGenericControl
    {
        public ReactBridge() : base("div")
        {
        }

        [MarkupOptions(Required = true)]
        public static readonly DotvvmProperty NameProperty =
            DotvvmProperty.Register<string, ReactBridge>("Name");

        [MarkupOptions(MappingMode = MappingMode.Attribute, AllowBinding = true, AllowHardCodedValue = true)]
        [PropertyGroup(new[] { "update:" })]
        public Dictionary<string, IValueBinding> Update { get; private set; } = new Dictionary<string, IValueBinding>();

        KnockoutBindingGroup CreateProps()
        {
            var props = new KnockoutBindingGroup();
            foreach (var attr in Attributes)
            {
                props.Add(attr.Key,
                    attr.Value is IValueBinding binding ? $"dotvvm.serialization.serialize({binding.GetKnockoutBindingExpression()})" :
                    (attr.Value is IStaticValueBinding staticBinding ? staticBinding.Evaluate(this, null) : attr.Value).Apply(JsonConvert.SerializeObject)
                );
            }
            foreach (var update in this.Update)
            {
                props.Add(update.Key, "function (a) {(" + update.Value.GetKnockoutBindingExpression() + ")(a)}");
            }
            return props;
        }
        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var binding = new KnockoutBindingGroup();
            binding.Add("component", (string)GetValue(NameProperty));
            binding.Add("props", CreateProps().ToString());
            writer.AddKnockoutDataBind("dotvvm-contrib-ReactBridge", binding);
            Attributes.Clear();
        }
    }
}
