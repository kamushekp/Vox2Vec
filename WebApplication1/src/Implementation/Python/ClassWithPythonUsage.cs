using System.IO;
using System.Reflection;

namespace Vox2Vec.Implementation.Python
{
    public abstract class ClassWithPythonUsage
    {
        public const string PythonPath = "D:\\VisualStudio\\Shared\\Python36_64\\python.exe";
        private readonly string ScriptDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Python");
        protected string ScriptPath => Path.Combine(this.ScriptDirectory, this.ScriptName);
        public abstract string ScriptName { get; }
    }
}