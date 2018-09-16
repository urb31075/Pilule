// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingAspect.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Defines the LoggingAspect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace PiluleDAL
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using AspectInjector.Broker;

    /// <summary>
    /// The logging aspect.
    /// </summary>
    [Aspect(Aspect.Scope.Global)]
    public class LoggingAspect
    {
        /// <summary>
        /// The handle method.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="returnType">
        /// The return type.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        [Advice(Advice.Type.Around, Advice.Target.Method)]
        public object HandleMethod(
            [Advice.Argument(Advice.Argument.Source.Type)] Type type,
            [Advice.Argument(Advice.Argument.Source.Name)] string name,
            [Advice.Argument(Advice.Argument.Source.ReturnType)] Type returnType,
            [Advice.Argument(Advice.Argument.Source.Method)] Type method,
            [Advice.Argument(Advice.Argument.Source.Arguments)] object[] arguments,
            [Advice.Argument(Advice.Argument.Source.Target)] Func<object[], object> target)
        {
            var sw = Stopwatch.StartNew();
            var result = target(arguments);
            sw.Stop();

            var methodFullName = method.ToString();
            var logFileName = string.Empty;
            var logMode = false;
            var exportable = false;

            if (method.DeclaringType != null)
            {
                // Проход по атрибутам класса
                foreach (var customAttributes in method.DeclaringType.CustomAttributes)
                {
                    if (customAttributes.AttributeType.FullName == typeof(DebugLogFileNameAttribute).FullName)
                    {
                        logFileName = customAttributes.ConstructorArguments[0].Value.ToString();
                    }

                    if (customAttributes.AttributeType.FullName == typeof(DebugLogModeAttribute).FullName)
                    {
                        logMode = Convert.ToBoolean(customAttributes.ConstructorArguments[0].Value);
                    }
                }

                var declaringType = (TypeInfo)method.DeclaringType;
                if (declaringType != null)
                {
                    var implementedInterfaces = declaringType.ImplementedInterfaces;
                    foreach (var type1 in implementedInterfaces)
                    {
                        var implementedInterface = (TypeInfo)type1;
                        
                        // Проход по атрибутам интерфейса
                        foreach (var customAttribute in implementedInterface.CustomAttributes)
                        {
                            if (customAttribute.AttributeType.FullName == typeof(DebugExportableAttribute).FullName)
                            {
                                exportable = true;
                            }
                        }

                        // Проход по атрибутам методов интерфейса
                        foreach (var declaredMethod in implementedInterface.DeclaredMethods.Where(m => m.ToString() == methodFullName))
                        {
                            foreach (var customAttribute in declaredMethod.CustomAttributes)
                            {
                                if (customAttribute.AttributeType.FullName == typeof(DebugExportableAttribute).FullName)
                                {
                                    exportable = true;
                                }
                            }
                        }
                    }
                }
            }

            // Проход по методам класса
            foreach (var customAttributes in method.CustomAttributes)
            {
                if (customAttributes.AttributeType.FullName == typeof(DebugLogFileNameAttribute).FullName)
                {
                    logFileName = customAttributes.ConstructorArguments[0].Value.ToString();
                }

                if (customAttributes.AttributeType.FullName == typeof(DebugLogModeAttribute).FullName)
                {
                    logMode = Convert.ToBoolean(customAttributes.ConstructorArguments[0].Value);
                }
            }

            if (logMode)
            {
                var argumentStr = arguments.Aggregate($"Execute method: {name}(", (current, arg) => current + (arg + ", ")).Trim().Trim(',') + ")";
                if (!string.IsNullOrEmpty(logFileName))
                {
                    File.AppendAllText(logFileName, $"{DateTime.Now} {argumentStr} in {sw.ElapsedMilliseconds} ms\n"); // используем значение атрибута для формирования результата
                }
            }

            if (exportable)
            {
                File.AppendAllText(logFileName, $"{DateTime.Now} Method {methodFullName} is Exportable!\n");
            }

            return result;
        }
    }
}
