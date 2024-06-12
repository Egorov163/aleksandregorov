using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using WebStocks.Models.StockApiHelper;

namespace WebStocks.Services
{
    public class ReflectionService
    {
        public ApiHelperViewModel BuilderApiHelperViewModel(Type apiControllerType)
        {
            var controllerMethodNames = typeof(Controller)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToList();

            var apiHelperViewModel = new ApiHelperViewModel();
            apiHelperViewModel.Name = apiControllerType.Name;
            apiHelperViewModel.Methods = apiControllerType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => !controllerMethodNames.Contains(method.Name))
                .Select(method => new MethodViewModel()
                {
                    Name = method.Name,
                    MethodType = CalculateMethodTypeByAttributes(method),
                    Parameters = method.GetParameters()
                    .Select(parameter => new ParameterViewModel()
                    {
                        Name = parameter.Name,
                        TypeName = parameter.ParameterType.Name,
                        Exempel = GetDefaultValue(parameter.ParameterType)
                    })
                    .ToList()
                })
                .ToList();
            return apiHelperViewModel;
        }

        private string GenerateJsonExample(Type parameterType, int deep)
        {          
            var listGenericType = parameterType
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (listGenericType!=null)
            {
                var listType = listGenericType.GetGenericArguments()[0];

                return $"[{GetDefaultValue(listType, deep + 1)}]";
            }

            var stringBuilder = new StringBuilder();
            var properties = parameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            stringBuilder.AppendLine("{");

            var spaces = new string(' ', deep * 2);
            foreach (var property in properties)
            {                
                stringBuilder.AppendLine($"{spaces}{property.Name}:" +
                    $" {GetDefaultValue(property.PropertyType, deep + 1)},");
            }
            var endSpaces = new string(' ', (deep-1) * 2);
            stringBuilder.Append($"{endSpaces}}}");

            return stringBuilder.ToString();
        }

        private string GetDefaultValue(Type propertyType, int deep = 1)
        {
            if (propertyType == typeof(string))
            {
                return "'text'";
            }

            if (propertyType.IsValueType)
            {
                if (propertyType.IsGenericType
                    && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var realValueType = propertyType.GetGenericArguments()[0];
                    return Activator.CreateInstance(realValueType).ToString();
                }

                return Activator.CreateInstance(propertyType).ToString();
            }

            return GenerateJsonExample(propertyType, deep);
        }

        private MethodType CalculateMethodTypeByAttributes(MethodInfo method)
        {
            var attributes = method.GetCustomAttributes();

            if (attributes.Any(x => x is HttpPostAttribute))
            {
                return MethodType.Post;
            }

            if (attributes.Any(x => x is HttpGetAttribute))
            {
                return MethodType.Get;
            }

            return MethodType.None;
        }
    }
}
