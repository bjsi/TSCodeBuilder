using CodeBuilder.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TSCodeBuilder.Expressions
{
    class TSMethodInvoke : MethodInvoke
    {
        public TSMethodInvoke(string name, params string[] args)
        {
            this.MethodName = name;
        }
    }
}
