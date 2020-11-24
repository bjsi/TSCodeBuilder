using CodeBuilder.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Objects
{
    public class TSArgument : Argument
    {
        public TSArgument(string name, string type)
        {
            name.ThrowIfNullOrEmpty("Failed to create argument because name was null or empty");
            type.ThrowIfNullOrEmpty("Failed to create argument because type was null or empty");
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return Type.IsNullOrEmpty()
                ? $"{Name}" // TODO: Check
                : $"{Name}: {Type}";
        }
    }
}
