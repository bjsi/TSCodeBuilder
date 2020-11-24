using CodeBuilder.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TSCodeBuilder.Expressions
{
    public class TSThisField : ThisFieldReference
    {
        public override string ThisName => "this";

        public TSThisField(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }
}
