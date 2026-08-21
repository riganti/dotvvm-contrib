using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Compilation.Javascript.Ast;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.LoadablePanel.Samples.Controls.Pager
{
    public static class PagerExtensions
    {
        public static void GoToPage(IGridViewDataSet set, int index, Func<IGridViewDataSet> loader)
        {
            throw new NotImplementedException("This method is intended for use in static command binding only.");
        }

        public static IServiceCollection AddPagerExtensions(this IServiceCollection services)
        {
            services.Configure((Action<JavascriptTranslatorConfiguration>)(c =>
            {
                RegisterGoToPage(c, nameof(PagerExtensions.GoToPage), "goToPage");
            }));
            return services;
        }

        private static void RegisterGoToPage(JavascriptTranslatorConfiguration c, string csharpName, string jsName)
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
        public Func<IGridViewDataSet> Loader
        {
            get { return (Func<IGridViewDataSet>)GetValue(LoaderProperty); }
            set { SetValue(LoaderProperty, value); }
        }
        public static readonly DotvvmProperty LoaderProperty
            = DotvvmProperty.Register<Func<IGridViewDataSet>, Pager>(c => c.Loader, null);
    }
}

