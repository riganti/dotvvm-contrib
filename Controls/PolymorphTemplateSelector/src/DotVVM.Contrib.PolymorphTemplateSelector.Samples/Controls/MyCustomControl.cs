using System.Collections.Generic;
using DotVVM.Contrib.PolymorphTemplateSelector.Samples.Model;
using DotVVM.Contrib.PolymorphTemplateSelector.Samples.ViewModels;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.PolymorphTemplateSelector.Samples.Controls
{
    public class MyCustomControl : HtmlGenericControl
    {
        private readonly BindingCompilationService bindingCompilationService;

        public MyCustomControl(BindingCompilationService bindingCompilationService)
        {
            this.bindingCompilationService = bindingCompilationService;
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var repeater = new Repeater();
            repeater.DataSource = ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => ((Sample3ViewModel) contexts[0]).Questions, this.GetDataContextType());
            repeater.ItemTemplate = new DelegateTemplate((provider, control) =>
            {
                var templateSelector = new PolymorphTemplateSelector();
                var templates = new List<PolymorphTemplate>();

                var flowActionControls = new IFlowActionControl[] { new YesNoQuestionControl(bindingCompilationService) };
                foreach (var action in flowActionControls)
                {
                    var template = new PolymorphTemplate()
                    {
                        ContentTemplate = new DelegateTemplate((_, c) => c.Children.Add((HtmlGenericControl)action))
                    };
                    
                    var dataContextStack = control.GetDataContextType();
                    template.SetBinding(x => x.DataContext, action.BuildDataContextBinding(dataContextStack));
                    ((HtmlGenericControl) action).SetDataContextType(action.BuildChildDataContextType(dataContextStack));

                    templates.Add(template);
                }

                templateSelector.Templates = templates;
                control.Children.Add(templateSelector);
            });
            Children.Add(repeater);

            base.OnInit(context);
        }
    }

    public interface IFlowActionControl
    {
        IValueBinding BuildDataContextBinding(DataContextStack dataContextType);
        DataContextStack BuildChildDataContextType(DataContextStack dataContextType);
    }

    public class YesNoQuestionControl : HtmlGenericControl, IFlowActionControl
    {
        private readonly BindingCompilationService bindingCompilationService;

        public YesNoQuestionControl(BindingCompilationService bindingCompilationService)
        {
            this.bindingCompilationService = bindingCompilationService;
        }

        public IValueBinding BuildDataContextBinding(DataContextStack dataContextType)
        {
            return ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => ((QuestionEntry) contexts[0]).YesNo, dataContextType);
        }

        public DataContextStack BuildChildDataContextType(DataContextStack dataContextType)
        {
            return DataContextStack.Create(typeof(YesNoQuestion), dataContextType);
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var p1 = new HtmlGenericControl("p");
            p1.SetBinding(x => x.InnerText, ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => ((YesNoQuestion) contexts[0]).Question, this.GetDataContextType()));
            Children.Add(p1);

            var p2 = new HtmlGenericControl("p");
            {
                var rb1 = new RadioButton() {Text = "yes"};
                rb1.SetBinding(x => x.CheckedItem, ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => ((YesNoQuestion) contexts[0]).Value, this.GetDataContextType()));
                rb1.SetBinding(x => x.CheckedValue, ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => true, this.GetDataContextType()));
                rb1.Attributes.AddBinding("name", ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => "q" + ((QuestionEntry)contexts[1]).Id, this.GetDataContextType()));
                p2.Children.Add(rb1);

                var rb2 = new RadioButton() { Text = "no" };
                rb2.SetBinding(x => x.CheckedItem, ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => ((YesNoQuestion)contexts[0]).Value, this.GetDataContextType()));
                rb2.SetBinding(x => x.CheckedValue, ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => false, this.GetDataContextType()));
                rb2.Attributes.AddBinding("name", ValueBindingExpression.CreateBinding(bindingCompilationService, contexts => "q" + ((QuestionEntry)contexts[1]).Id, this.GetDataContextType()));
                p2.Children.Add(rb2);
            }
            Children.Add(p2);
            
            base.OnInit(context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("fake-css");
            base.OnPreRender(context);
        }
    }
}
