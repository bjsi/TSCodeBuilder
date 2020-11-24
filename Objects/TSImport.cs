using System;
using System.Collections.Generic;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Objects
{
    public class TSImport
    {

        public string ModuleName { get; }
        public HashSet<string> Objects { get; } = new HashSet<string>();

        public TSImport(string moduleName, params string[] objects)
        {
            moduleName.ThrowIfNullOrEmpty("Failed to create ts import because module name was null");
            this.ModuleName = moduleName;
            WithObjects(objects);
        }

        public TSImport WithObject(string obj)
        {
            Objects.Add(obj);
            return this;
        }

        public TSImport WithObjects(IEnumerable<string> objs)
        {
            Objects.UnionWith(objs);
            return this;
        }

        public override string ToString()
        {
            if (Objects.IsNullOrEmpty())
            {
                return null;
            }

            return $"import {{ {string.Join(", ", Objects)} }} from \"{ModuleName}\";";
        }
    }
}
