using CodeBuilder.Objects;
using System.Collections.Generic;
using System.IO;
using TSCodeBuilder.Statements;

namespace TSCodeBuilder.Objects
{
    public class TSConstructor : Constructor
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "Constructor.Mustache");

        public TSConstructor(params TSArgument[] args) 
            : base(TemplateFilePath)
        {
            this.WithArguments(args);
        }

        public string ArgumentsString => string.Join(", ", Arguments);

        public override string ToString() => Generate();

        public TSConstructor WithArgument(TSArgument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public TSConstructor WithArguments(IEnumerable<TSArgument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public TSConstructor WithStatement(TSAssignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public TSConstructor WithStatement(TSReturn statement)
        {
            this.Statements.Add(statement);
            return this;
        }
    }
}
