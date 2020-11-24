using CodeBuilder.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Objects
{
    public class TSFile : CodeGenerator
    {
        private string FileName { get; }

        public List<TSClass> Classes { get; } = new List<TSClass>();

        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "File.Mustache");
        private List<TSImport> Imports { get; } = new List<TSImport>();

        public TSFile(string fileName)
            : base(TemplateFilePath)
        {
            fileName.ThrowIfNullOrEmpty("Failed to create ts file because filename was null or empty");
            this.FileName = fileName;
        }

        public TSFile WithClass(TSClass klass)
        {
            if (Classes.Any(c => c.Name == klass.Name))
                throw new Exception($"Failed to add class because there is already a class called {klass.Name} in the file");

            Classes.Add(klass);
            return this;
        }

        public TSFile WithModuleImport(TSImport import)
        {
            // Check duplication
            foreach (var m in Imports)
            {
                if (m.ModuleName == import.ModuleName)
                {
                    m.Objects.UnionWith(import.Objects);
                    return this;
                }
            }

            Imports.Add(import);
            return this;
        }

        public override string ToString() => Generate();
    }
}
