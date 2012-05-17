using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sioen.Layerless.Infrastructure.Web
{
    public class EventDispatcher
    {
        BasePage _page;
        ModelBindingExecutionContext _executionContext;

        public EventDispatcher(BasePage page)
        {
            _page = page;
            _executionContext = page.ModelBindingExecutionContext;
            _executionContext.PublishService<Control>(page);
        }

        public void OnEvent(object sender, CommandEventArgs e)
        {
            var result = HandleCommand(e as CommandEventArgs);
            Redirect(result);
        }      

        public void OnEvent(object sender, EventArgs e)
        {
            var result = HandleEvent(sender, e);
            Redirect(result);
        }

        public void Redirect(object result)
        {
            _page.ModelBindingExecutionContext.HttpContext.Response.Redirect((string)result);
        }

        public Object HandleEvent(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                var methodName = (sender as Control).ID + "Action";
                var method = FindMethod(methodName);
                return InvokeMethod(method);
            }
            return null;
        }

        public Object HandleCommand(CommandEventArgs e)
        {
            var method = FindMethod(e.CommandName);
            return InvokeMethod(method);            
        }

        public MethodInfo FindMethod(string name)
        {
            var method = _page.GetType().GetMethod(name);
            if (method == null)
            {
                throw new ArgumentException(string.Format("No method named '{0}' found. Please create a method to execute this event.", name), "name");
            }
            return method;
        }

        public object InvokeMethod(MethodInfo method)
        {
            var parameters = method.GetParameters().Select(p => BindParameter(p)).ToArray();
            return method.Invoke(_page, parameters);
        }

        public object BindParameter(ParameterInfo parameter)
        {
            var name = parameter.Name;
            bool validateRequest;
            
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType((Func<object>)null, parameter.ParameterType);
            var valueProvider = GetCustomValueProvider(parameter, ref name, out validateRequest);

            var context = new ModelBindingContext
            {
                ModelMetadata = modelMetadata,
                ValueProvider = valueProvider,
                ModelName = name
            };

            var binder = ModelBinders.Binders[parameter.ParameterType] ?? ModelBinders.Binders.DefaultBinder;

            return binder.BindModel(_executionContext, context) ? context.Model : null;
        }

        private IValueProvider GetCustomValueProvider(ParameterInfo parameterInfo, ref string modelName, out bool validateRequest)
        {
            validateRequest = true;
            object[] customAttributes = parameterInfo.GetCustomAttributes(typeof(IValueProviderSource), false);
            if (Enumerable.Count<object>((IEnumerable<object>)customAttributes) > 1)
            {
                throw new NotSupportedException("ModelDataSourceView_MultipleValueProvidersNotSupported");
            }
            else
            {
                if (Enumerable.Count<object>((IEnumerable<object>)customAttributes) <= 0)
                    return (IValueProvider)null;
                IValueProviderSource valueProviderSource = (IValueProviderSource)customAttributes[0];
                if (valueProviderSource is IModelNameProvider)
                {
                    string modelName1 = ((IModelNameProvider)valueProviderSource).GetModelName();
                    if (!string.IsNullOrEmpty(modelName1))
                        modelName = modelName1;
                }
                if (valueProviderSource is IUnvalidatedValueProviderSource)
                    validateRequest = ((IUnvalidatedValueProviderSource)valueProviderSource).ValidateInput;
                return valueProviderSource.GetValueProvider(_executionContext);
            }
        }
    }
}
