﻿using System;
using System.Net;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using Newtonsoft.Json;

namespace DotVVM.Contrib
{

    /// <summary>
    /// Renders a FileUpload control allowing users to upload one or multiple files asynchronously and doesn't use iframe.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class FileUploadNoIframe : HtmlGenericControl
    {
        public FileUploadNoIframe()
            : base("div")
        {
        }

        /// <summary>
        /// Gets or sets a collection of uploaded files.
        /// </summary>
        [MarkupOptions(AllowHardCodedValue = false, Required = true)]
        public UploadedFilesCollection? UploadedFiles
        {
            get { return (UploadedFilesCollection?)GetValue(UploadedFilesProperty); }
            set { SetValue(UploadedFilesProperty, value); }
        }

        public static readonly DotvvmProperty UploadedFilesProperty
            = DotvvmProperty.Register<UploadedFilesCollection?, FileUploadNoIframe>(p => p.UploadedFiles);
        
        /// <summary>
        /// Gets or sets whether the user can select multiple files at once. It is enabled by default.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool AllowMultipleFiles
        {
            get { return (bool)GetValue(AllowMultipleFilesProperty)!; }
            set { SetValue(AllowMultipleFilesProperty, value); }
        }

        public static readonly DotvvmProperty AllowMultipleFilesProperty
            = DotvvmProperty.Register<bool, FileUploadNoIframe>(p => p.AllowMultipleFiles, true);

        /// <summary>
        /// Gets or sets the types of files that the server accepts. It must be a comma-separated list of unique content type
        /// specifiers (e.g. ".jpg,image/png,audio/*"). All file types are allowed by default.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string? AllowedFileTypes
        {
            get { return GetValue(AllowedFileTypesProperty) as string; }
            set { SetValue(AllowedFileTypesProperty, value); }
        }

        public static readonly DotvvmProperty AllowedFileTypesProperty
            = DotvvmProperty.Register<string?, FileUploadNoIframe>(p => p.AllowedFileTypes);

        /// <summary>
        /// Gets or sets the maximum size of files in megabytes (MB). The size is not limited by default.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public int? MaxFileSize
        {
            get { return GetValue(MaxFileSizeProperty) as int?; }
            set { SetValue(MaxFileSizeProperty, value); }
        }

        public static readonly DotvvmProperty MaxFileSizeProperty
            = DotvvmProperty.Register<int?, FileUploadNoIframe>(c => c.MaxFileSize);

        /// <summary>
        /// Gets or sets the text on the upload button. The default value is "Upload".
        /// </summary>
        public string UploadButtonText
        {
            get { return (string)GetValue(UploadButtonTextProperty)!; }
            set { SetValue(UploadButtonTextProperty, value ?? throw new ArgumentNullException(nameof(value))); }
        }

        public static readonly DotvvmProperty UploadButtonTextProperty
            = DotvvmProperty.Register<string, FileUploadNoIframe>(c => c.UploadButtonText, DotVVM.Framework.Resources.Controls.FileUpload_UploadButtonText, true);


        /// <summary>
        /// Gets or sets the text on the indicator showing number of files. The default value is "{0} files". The number of files
        /// will be substituted for the "{0}" placeholder.
        /// </summary>
        public string? NumberOfFilesIndicatorText
        {
            get { return (string?)GetValue(NumberOfFilesIndicatorTextProperty); }
            set { SetValue(NumberOfFilesIndicatorTextProperty, value); }
        }

        public static readonly DotvvmProperty NumberOfFilesIndicatorTextProperty
            = DotvvmProperty.Register<string?, FileUploadNoIframe>(c => c.NumberOfFilesIndicatorText, DotVVM.Framework.Resources.Controls.FileUpload_NumberOfFilesText, true);

        /// <summary>
        /// Gets or sets the text that appears when there is an error during the upload.
        /// </summary>
        public string? UploadErrorMessageText
        {
            get { return (string?)GetValue(UploadErrorMessageTextProperty); }
            set { SetValue(UploadErrorMessageTextProperty, value); }
        }

        public static readonly DotvvmProperty UploadErrorMessageTextProperty
            = DotvvmProperty.Register<string?, FileUploadNoIframe>(c => c.UploadErrorMessageText, DotVVM.Framework.Resources.Controls.FileUpload_UploadErrorMessageText, true);

        /// <summary>
        /// Gets or sets the text that appears when all files are uploaded successfully.
        /// </summary>
        public string? SuccessMessageText
        {
            get { return (string?)GetValue(SuccessMessageTextProperty); }
            set { SetValue(SuccessMessageTextProperty, value); }
        }

        public static readonly DotvvmProperty SuccessMessageTextProperty
            = DotvvmProperty.Register<string?, FileUploadNoIframe>(c => c.SuccessMessageText, DotVVM.Framework.Resources.Controls.FileUpload_SuccessMessageText, true);

        /// <summary>
        /// Gets or sets a command that is triggered when the upload is complete.
        /// </summary>
        [MarkupOptions(AllowHardCodedValue = false)]
        public Command? UploadCompleted
        {
            get { return (Command?)GetValue(UploadCompletedProperty); }
            set { SetValue(UploadCompletedProperty, value); }
        }

        public static readonly DotvvmProperty UploadCompletedProperty
            = DotvvmProperty.Register<Command?, FileUploadNoIframe>(p => p.UploadCompleted);


        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            if (!IsPropertySet(UploadedFilesProperty))
            {
                throw new DotvvmControlException(this, "The UploadedFiles property of the FileUpload cannot be null!");
            }
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.FileUploadNoIframe");
            context.ResourceManager.AddRequiredResource(ResourceConstants.DotvvmFileUploadCssResourceName);
            base.OnPreRender(context);

            Attributes["data-dotvvm-upload-id"] = GetDotvvmUniqueId();
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddKnockoutDataBind("with", this, UploadedFilesProperty, () => { throw new DotvvmControlException(this, "The UploadedFiles property of the FileUpload control must be bound!"); });
            writer.AddAttribute("class", "dotvvm-upload", true);

            var uploadCompletedBinding = GetCommandBinding(UploadCompletedProperty);
            if (uploadCompletedBinding != null)
            {
                writer.AddAttribute("data-dotvvm-upload-completed", KnockoutHelper.GenerateClientPostBackScript(nameof(UploadCompleted), uploadCompletedBinding, this, useWindowSetTimeout: true, returnValue: null));
            }
            
            base.AddAttributesToRender(writer, context);
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            RenderUploadButton(writer);
            RenderInputControl(writer, context);
            RenderUploadedFilesTitle(writer);
            RenderProgressWrapper(writer);
            RenderResultTitle(writer);

            base.RenderContents(writer, context);
        }

        private void RenderResultTitle(IHtmlWriter writer)
        {
            // render result
            writer.AddAttribute("class", "dotvvm-upload-result");
            writer.AddKnockoutDataBind("html", $"Error() ? {KnockoutHelper.MakeStringLiteral(UploadErrorMessageText!)} : {KnockoutHelper.MakeStringLiteral(SuccessMessageText!)}");
            writer.AddKnockoutDataBind("attr", "{ title: Error }");
            writer.AddKnockoutDataBind("css", "{ 'dotvvm-upload-result-success': !Error(), 'dotvvm-upload-result-error': Error }");
            writer.AddKnockoutDataBind("visible", "!IsBusy() && Files().length > 0");
            writer.RenderBeginTag("span");
            writer.RenderEndTag();
        }

        private static void RenderProgressWrapper(IHtmlWriter writer)
        {
            // render progress wrapper
            writer.AddKnockoutDataBind("visible", "IsBusy");
            writer.AddAttribute("class", "dotvvm-upload-progress-wrapper");
            writer.RenderBeginTag("span");
            writer.AddAttribute("class", "dotvvm-upload-progress");
            writer.AddKnockoutDataBind("style", "{ 'width': (Progress() == -1 ? '50' : Progress()) + '%' }");
            writer.RenderBeginTag("span");
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
        
        private void RenderUploadedFilesTitle(IHtmlWriter writer)
        {
            // render upload files
            writer.AddAttribute("class", "dotvvm-upload-files");
            writer.AddKnockoutDataBind("html", $"dotvvm.globalize.format({KnockoutHelper.MakeStringLiteral(NumberOfFilesIndicatorText!)}, Files().length)");
            writer.RenderBeginTag("span");
            writer.RenderEndTag();
        }

        private void RenderUploadButton(IHtmlWriter writer)
        {
            // render upload button
            writer.AddAttribute("class", "dotvvm-upload-button");
            writer.AddKnockoutDataBind("visible", "!IsBusy()");
            writer.RenderBeginTag("span");
            writer.AddAttribute("href", "javascript:;");
            writer.AddAttribute("onclick", "this.parentElement.nextElementSibling.click(); return false;");
            writer.RenderBeginTag("a");
            writer.WriteText(UploadButtonText);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RenderInputControl(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddStyleAttribute("display", "none");
            writer.AddAttribute("type", "file");
            if (AllowMultipleFiles)
            {
                writer.AddAttribute("multiple", "multiple");
            }
            if (!string.IsNullOrWhiteSpace(AllowedFileTypes))
            {
                writer.AddAttribute("accept", AllowedFileTypes);
            }

            writer.AddKnockoutDataBind("dotvvm-contrib-FileUploadNoIframe", JsonConvert.SerializeObject(new { url = context.TranslateVirtualPath(GetFileUploadHandlerUrl()) }));
            writer.RenderSelfClosingTag("input");
        }

        private string GetFileUploadHandlerUrl()
        {
            var builder = new StringBuilder("~/");
            builder.Append(HostingConstants.FileUploadHandlerMatchUrl);
            var delimiter = "?";

            if (AllowMultipleFiles)
            {
                builder.AppendFormat("{0}multiple=true", delimiter);
                delimiter = "&";
            }

            if (!string.IsNullOrWhiteSpace(AllowedFileTypes))
            {
                builder.AppendFormat("{0}fileTypes={1}", delimiter, WebUtility.UrlEncode(AllowedFileTypes));
                delimiter = "&";
            }

            if (MaxFileSize != null)
            {
                builder.AppendFormat("{0}maxSize={1}", delimiter, MaxFileSize);
            }

            return builder.ToString();
        }
    }

}
