using CodeBuilder.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Objects
{
    public class TSClass : Class
    {
        private static string TemplateFilePath { get; } = Path.Combine(Const.TemplateFolderPath, "Class.Mustache");

        public TSClass(string name)
            : base(TemplateFilePath)
        {
            name.ThrowIfNullOrEmpty("Failed to create argument because name was null or empty");
            this.Name = name;
        }

        public override string ToString() => Generate();
        public string BaseString => Bases.IsNullOrEmpty()
            ? null
            : "extends " + string.Join(", ", this.Bases);
        public string CommentString => string.Join("\n\t", this.Comments);

        public TSClass WithField(TSField field)
        {
            this.Fields.Add(field);
            return this;
        }

        public TSClass WithFields(IEnumerable<TSField> fields)
        {
            this.Fields.AddRange(fields);
            return this;
        }

        public TSClass WithBaseClass(string klass)
        {
            this.Bases = new List<string> { klass };
            return this;
        }

        public TSClass WithComment(string comment)
        {
            this.Comments.Add(comment);
            return this;
        }

        public TSClass WithConstructor(TSConstructor cons)
        {
            this.Constructor = cons;
            return this;
        }

        public TSClass WithMethod(TSMethod method)
        {
            this.Methods.Add(method);
            return this;
        }

    }
}
