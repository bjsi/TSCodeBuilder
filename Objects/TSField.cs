using CodeBuilder.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSCodeBuilder.Objects
{
    public class TSField : Field
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "Field.Mustache");

        public TSField(string name, string type)
            : base(TemplateFilePath)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString() => Generate();

        public TSField WithComment(string comment)
        {
            this.Comment = (comment);
            return this;
        }
    }
}
