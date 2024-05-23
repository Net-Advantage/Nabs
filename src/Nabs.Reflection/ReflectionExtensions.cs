using System.Reflection;

namespace Nabs.Reflection;

public static class ReflectionExtensions
{
    public static T CreateInstance<T>(
        this Type type,
        params object[] args)
            where T : new()
    {
        var instance = Activator.CreateInstance(type, args)!;
        return (T)instance;
    }

    public static async Task<T?> InvokeMethodAsync<T>(this Type type,
        string methodName,
        object obj,
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public,
        params object[] parameters)
    {
        var parametersTypes = Array.Empty<Type>();
        if (parameters.Length > 0)
        {
            parametersTypes = parameters.Select(_ => _.GetType()).ToArray();
        }

        var methodInfo = type.GetMethod(
                methodName,
                bindingFlags,
                null,
                parametersTypes,
                null)
                ?? throw new ArgumentException($"The method, '{methodName}', could not be found or parameters did not match.");

        var task = methodInfo.Invoke(obj, parameters)!;

        await (Task)task;

        var resultProperty = task.GetType().GetProperty("Result")!;
        var result = resultProperty.GetValue(task);
        return result == null ? default : (T)result;
    }

    public static async Task InvokeMethodAsync(this Type type,
            string methodName,
            object obj,
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public,
            params object[] parameters)
    {
        var parametersTypes = parameters.Length > 0
            ? parameters.Select(_ => _.GetType()).ToArray()
            : Array.Empty<Type>();

        var methodInfo = type.GetMethod(
                methodName,
                bindingFlags,
                null,
                parametersTypes,
                null)
            ?? throw new ArgumentException($"The method, '{methodName}', could not be found or parameters did not match.");

        var task = methodInfo.Invoke(obj, parameters) as Task;
        await task!;
    }
}