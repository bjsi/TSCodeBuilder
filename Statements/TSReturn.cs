using CodeBuilder.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSCodeBuilder.Statements
{
    public class TSReturn : ReturnStatement
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "Return.Mustache");

        public TSReturn(string rhs)
            : base(TemplateFilePath)
        {
            this.RHS = rhs;
        }

        public override string ToString() => Generate();
    }
}
