﻿namespace Nabs;

public static class ReflectionExtensions
{
    public static T CreateInstance<T>(
        this Type type,
        params object[]? args)
            where T : new()
    {
        var instance = args != null ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
        if (instance == null)
        {
            throw new TypeInitializationException(type.FullName, null);
        }

        return (T)instance;
    }

    public static async Task<T?> InvokeMethodAsync<T>(this Type type,
        string methodName,
        object obj,
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public,
        params object[] parameters)
    {
        var parametersTypes = Array.Empty<Type>();
        if (parameters.NotNullAndAny())
        {
            parametersTypes = parameters.Select(_ => _.GetType()).ToArray();
        }

        var methodInfo = type.GetMethod(
                methodName,
                bindingFlags,
                null,
                parametersTypes,
                null);

        if (methodInfo == null)
        {
            throw new ArgumentException($"The method, '{methodName}', could not be found or parameters did not match.");
        }

        var task = methodInfo.Invoke(obj, parameters);
        if (task == null)
        {
            return default;
        }

        await (Task)task;
        var resultProperty = task.GetType().GetProperty("Result");
        var result = resultProperty?.GetValue(task);
        return result == null ? default : (T)result;
    }
}