using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TSCodeBuilder.Expressions;
using TSCodeBuilder.Helpers;
using TSCodeBuilder.Objects;
using TSCodeBuilder.Statements;

namespace TSCodeBuilder.Converter
{
    public class TSConverter
    {
        private Type CSType { get; }
        private TSClass Class { get; set; }
        private List<IMethodTransformer> MethodTransformers { get; } = new List<IMethodTransformer>();
        private TSTypeConverter TypeConverter { get; }

        public TSConverter(string name, Type csType, List<IMethodTransformer> methodTransformers, TSTypeConverter typeConverter)
        {
            name.ThrowIfArgumentNull("Failed to create converter because class name was empty");
            csType.ThrowIfArgumentNull("Failed to create converter because c# type was null");
            methodTransformers.ThrowIfArgumentNull("Failed to create converter because method transform map was null");
            typeConverter.ThrowIfArgumentNull("Failed to create converter because type converter was null");

            this.TypeConverter = typeConverter;
            this.CSType = csType;
            this.MethodTransformers = methodTransformers;
            Class = new TSClass(name);

        }

        public string Convert()
        {
            ConvertMethods();
            return Class.ToString();
        }

        private void ConvertMethods()
        {
            foreach (var m in CSType.GetMethods().Where(x => !x.IsSpecialName && x.DeclaringType == CSType))
            {
                var method = ConvertMethod(m);
                Class.WithMethod(method);
            }
        }

        private TSMethod ConvertMethod(MethodInfo method)
        {
            var name = method.Name;
            var pyReturnType = TypeConverter.Convert(method.ReturnType);
            var pyMethod = new TSMethod(name, true, pyReturnType);
            var thisFieldRef = new TSThisField("_server");
            var methodInvoke = new TSMethodInvoke($"{thisFieldRef}.{name}");

            foreach (var p in method.GetParameters())
            {
                bool handled = false;
                foreach (var t in MethodTransformers)
                {
                    if (t.Matches(p, CSType))
                    {
                        methodInvoke = t.Transform(methodInvoke);
                        handled = true;
                        break;
                    }
                }

                if (handled)
                    continue;

                var pyMethodName = p.Name;
                var pyParamType = TypeConverter.Convert(p.ParameterType);
                pyMethod.WithArgument(new TSArgument(pyMethodName, pyParamType));
                methodInvoke.WithArgument(pyMethodName);

            }

            pyMethod.WithStatement(new TSReturn(methodInvoke.ToString()));
            return pyMethod;
        }
    }
}
