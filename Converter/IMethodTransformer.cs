using System;
using System.Reflection;
using TSCodeBuilder.Expressions;

namespace TSCodeBuilder.Converter
{
    public interface IMethodTransformer
    {
        bool Matches(ParameterInfo info, Type CSType);
        TSMethodInvoke Transform(TSMethodInvoke methodInvoke);
    }
}
