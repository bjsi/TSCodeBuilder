using CodeBuilder.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSCodeBuilder.Statements
{
    public class TSAssignment : Assignment
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "Assignment.Mustache");
        public override string ToString() => Generate();
        public TSAssignment(string lhs, string rhs)
            : base(TemplateFilePath)
        {
            this.LHS = lhs;
            this.RHS = rhs;
        }
    }
}
