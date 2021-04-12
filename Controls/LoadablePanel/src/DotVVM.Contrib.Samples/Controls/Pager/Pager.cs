using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Compilation.Javascript.Ast;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.Samples.Controls.Pager
{
    public static class PagerExtensions
    {
        public static void GoToPage(IPageableGridViewDataSet set, int index, Func<IPageableGridViewDataSet> loader)
        {
            throw new NotImplementedException("JS");
        }

        public static IServiceCollection AddPagerExtensions(this IServiceCollection services)
        {
            services.Configure((Action<JavascriptTranslatorConfiguration>)(c =>
            {
                RegisterAlertMethod(c, nameof(PagerExtensions.GoToPage), "goToPage");
            }));
            return services;
        }

        private static void RegisterAlertMethod(JavascriptTranslatorConfiguration c, string csharpName, string jsName)
        {
            c.MethodCollection.AddMethodTranslator(
                           typeof(PagerExtensions),
                           csharpName,
                           new GenericMethodCompiler((a) =>
                           new JsIdentifierExpression(nameof(PagerExtensions))
                                          .Member(jsName)
                                          .Invoke(
                                                   a[1].WithAnnotation(ShouldBeObservableAnnotation.Instance),
                                                   a[2].WithAnnotation(ShouldBeObservableAnnotation.Instance),
                                                   a[3].WithAnnotation(ShouldBeObservableAnnotation.Instance)
                                               )), 3, allowMultipleMethods: true);
        }
    }

    public class Pager : DotvvmMarkupControl
    {
        public Func<IPageableGridViewDataSet> Loader
        {
            get { return (Func<IPageableGridViewDataSet>)GetValue(LoaderProperty); }
            set { SetValue(LoaderProperty, value); }
        }
        public static readonly DotvvmProperty LoaderProperty
            = DotvvmProperty.Register<Func<IPageableGridViewDataSet>, Pager>(c => c.Loader, null);
    }
}

