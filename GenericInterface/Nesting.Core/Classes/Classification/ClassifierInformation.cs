using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nesting.Core.Classes.Classification
{
    public class ClassifierInformation
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Description { get; private set; }

        public string AssemblyName { get; private set; }

        public ClassifierInformation(string name, string version, string description, string assembyName)
        {
            this.Name = name;
            this.Version = version;
            this.Description = description;
            this.AssemblyName = assembyName;
        }

    }
}
