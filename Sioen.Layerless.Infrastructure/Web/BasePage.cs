using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Command;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Infrastructure.Web
{
    public abstract class BasePage : Page
    {
        public IQueryExecutor Db { get; set; }
        public ICommandExecutor Command { get; set; }

        public override ViewStateMode ViewStateMode
        {
            get { return ViewStateMode.Disabled; }
            set { }
        }

        protected void OnCommand(object sender, CommandEventArgs e)
        {
            var method = GetType().GetMethod(e.CommandName);
            var redirect = method.Invoke(this, test(method));

            if (redirect is string)
            {
                Response.Redirect(redirect as string, true);
            }
        }

        private object[] test(MethodInfo methodInfo)
        {
            var parameters = new Dictionary<object, object>();

            IModelBinder defaultBinder = ModelBinders.Binders.DefaultBinder;
            ModelBindingExecutionContext executionContext = this.ModelBindingExecutionContext;
            executionContext.PublishService<Page>(this);
            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                object newValue = (object)null;
                string name = parameterInfo.Name;
                if (parameterInfo.ParameterType == typeof(ModelMethodContext))
                {
                    newValue = (object)new ModelMethodContext(this);
                }
                else if (!parameterInfo.IsOut)
                {
                    bool validateRequest;
                    IValueProvider customValueProvider = this.GetCustomValueProvider(executionContext, parameterInfo, ref name, out validateRequest);
                    ModelBindingContext bindingContext = new ModelBindingContext()
                    {
                        ModelBinderProviders = ModelBinderProviders.Providers,
                        ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType((Func<object>)null, parameterInfo.ParameterType),
                        ModelState = this.ModelState,
                        ModelName = name,
                        ValueProvider = customValueProvider,
                        ValidateRequest = validateRequest
                    };
                    if (customValueProvider != null && parameterInfo.ParameterType.IsSerializable)
                    {
                        //    if (!this.SelectParameters.ContainsKey(parameterInfo.Name))
                        //        this.SelectParameters.Add(parameterInfo.Name, new MethodParameterValue());
                        if (defaultBinder.BindModel(executionContext, bindingContext))
                            newValue = bindingContext.Model;
                        //this.SelectParameters[parameterInfo.Name].UpdateValue(newValue);
                        //}
                        //else if (!isPageLoadComplete)
                        //{
                        //    if (customValueProvider == null)
                        //        bindingContext.ValueProvider = providerFromDictionary;
                        //    if (defaultBinder.BindModel(executionContext, bindingContext))
                        //        newValue = bindingContext.Model;
                    }
                    //else
                    //    continue;
                    //if (!isPageLoadComplete)
                    //    this.ValidateParameterValue(parameterInfo, newValue, methodInfo);
                }
                parameters.Add((object)parameterInfo.Name, newValue);
            }

            return parameters.Values.ToArray();
        }

        private IValueProvider GetCustomValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, ParameterInfo parameterInfo, ref string modelName, out bool validateRequest)
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
                return valueProviderSource.GetValueProvider(modelBindingExecutionContext);
            }
        }

        
    }
}
