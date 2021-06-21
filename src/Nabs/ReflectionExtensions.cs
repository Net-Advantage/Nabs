using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nabs
{
    public static class ReflectionExtensions
    {
        public static T CreateInstance<T>(
            this Type type,
            params object[] args)
                where T : new()
        {
            var result = args != null ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
            return (T)result;
        }

        public static async Task<T> InvokeMethodAsync<T>(this Type type,
            string methodName,
            object obj,
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public,
            params object[] parameters)
        {
            MethodInfo methodInfo;
            if (parameters?.Any() ?? false)
            {
                var parametersTypes = parameters.Select(_ => _.GetType()).ToArray();
                methodInfo = type.GetMethod(
                    methodName,
                    bindingFlags,
                    null,
                    parametersTypes,
                    null);
            }
            else
            {
                methodInfo = type.GetMethod(
                    methodName,
                    bindingFlags,
                    null,
                    Array.Empty<Type>(),
                    null);
            }

            if (methodInfo == null)
            {
                throw new ArgumentException($"The method, '{methodName}', could not be found or parameters did not match.");
            }

            var task = methodInfo.Invoke(obj, parameters);
            if (task == null)
            {
                return default;
            }

            await ((Task)task).ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var result = resultProperty?.GetValue(task);
            return result == null ? default : (T)result;
        }
    }
}