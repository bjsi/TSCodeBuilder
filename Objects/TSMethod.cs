using CodeBuilder.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TSCodeBuilder.Helpers;
using TSCodeBuilder.Statements;

namespace TSCodeBuilder.Objects
{
    public class TSMethod : Method
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "Method.Mustache");
        public bool IsAsync { get; }
        public string ArgumentsString => string.Join(", ", Arguments);
        public string CommentsString => string.Join("\n\t", Comments);

        public TSMethod(string name, bool async, string returnType) 
            : base(TemplateFilePath)
        {
            name.ThrowIfNullOrEmpty("Failed to create method because the name was null or empty");
            returnType.ThrowIfArgumentNull("Failed to create method because return type was null");

            this.Name = name;
            this.IsAsync = async;
            if (IsAsync) returnType = $"Promise<{returnType}>";
            this.ReturnType = returnType;
        }

        public TSMethod WithComment(string comment)
        {
            this.Comments.Add(comment);
            return this;
        }

        public TSMethod WithArgument(TSArgument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public TSMethod WithArguments(IEnumerable<TSArgument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public TSMethod WithStatement(TSAssignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public TSMethod WithStatement(TSReturn statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public override string ToString() => Generate();
    }
}
