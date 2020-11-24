using CodeBuilder.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Expressions
{
    public class TSMethodInvoke : MethodInvoke
    {
        public TSMethodInvoke(string methodName, params string[] args)
        {
            methodName.ThrowIfNullOrEmpty("Failed to create method invoke expression because method name is null or empty");
            this.MethodName = methodName;
            this.WithArguments(args);
        }

        public TSMethodInvoke WithArgument(string arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public TSMethodInvoke WithArguments(IEnumerable<string> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }
    }
}
