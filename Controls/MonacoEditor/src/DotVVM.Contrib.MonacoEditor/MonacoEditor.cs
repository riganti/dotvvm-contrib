using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.MonacoEditor
{
    /// <summary>
    /// Renders a Monaco editor in edit, read-only, or diff mode.
    /// </summary>
    public class MonacoEditor : HtmlGenericControl
    {
        public MonacoEditor() : base("div")
        {
        }

        /// <summary>
        /// Gets or sets the editor content. In diff mode, this is the modified content.
        /// </summary>
        [MarkupOptions(Required = true, AllowHardCodedValue = false)]
        public string Code
        {
            get { return Convert.ToString(GetValue(CodeProperty)); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly DotvvmProperty CodeProperty =
            DotvvmProperty.Register<string, MonacoEditor>(t => t.Code, "");

        /// <summary>
        /// Gets or sets the original content displayed on the left side in diff mode.
        /// </summary>
        [MarkupOptions(AllowHardCodedValue = false)]
        public string OriginalCode
        {
            get { return Convert.ToString(GetValue(OriginalCodeProperty)); }
            set { SetValue(OriginalCodeProperty, value); }
        }

        public static readonly DotvvmProperty OriginalCodeProperty =
            DotvvmProperty.Register<string, MonacoEditor>(t => t.OriginalCode, "");

        /// <summary>
        /// Gets or sets the editor mode.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public MonacoEditorMode Mode
        {
            get { return (MonacoEditorMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DotvvmProperty ModeProperty =
            DotvvmProperty.Register<MonacoEditorMode, MonacoEditor>(t => t.Mode, MonacoEditorMode.Edit);

        /// <summary>
        /// Gets or sets the language identifier understood by Monaco.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Language
        {
            get { return Convert.ToString(GetValue(LanguageProperty)); }
            set { SetValue(LanguageProperty, value); }
        }

        public static readonly DotvvmProperty LanguageProperty =
            DotvvmProperty.Register<string, MonacoEditor>(t => t.Language, "plaintext");

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            if (Mode == MonacoEditorMode.Diff && !IsPropertySet(OriginalCodeProperty))
            {
                throw new DotvvmControlException("The OriginalCode property is required when MonacoEditor uses Diff mode.");
            }

            context.ResourceManager.AddRequiredResource("dotvvm.contrib.MonacoEditor");
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            AddDotvvmUniqueIdAttribute();
            base.AddAttributesToRender(writer, context);

            var group = new KnockoutBindingGroup();
            group.Add("code", this, CodeProperty);
            group.Add("originalCode", this, OriginalCodeProperty);
            group.Add("mode", KnockoutHelper.MakeStringLiteral(Mode.ToString()));
            group.Add("language", KnockoutHelper.MakeStringLiteral(Language));

            writer.AddKnockoutDataBind("dotvvm-contrib-MonacoEditor", group);
        }
    }

    /// <summary>
    /// Specifies how the Monaco editor displays its content.
    /// </summary>
    public enum MonacoEditorMode
    {
        Edit,
        ReadOnly,
        Diff
    }
}
