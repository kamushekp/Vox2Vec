using System.IO;
using System.Reflection;
using System.Web.Hosting;

namespace Vox2Vec.Implementation
{
    public interface IResourcePaths
    {
        string ModelPath { get; }
        string LoadVoiceScriptPath { get; }
        string VoicePreprocessorPath { get; }
        string PythonPath { get; }
        string DataDumpFile { get; }
    }
    public class ResourcePaths:IResourcePaths
    {
        public string ModelPath => Path.Combine(this.HomePath, "base_model.pb");
        public string LoadVoiceScriptPath => Path.Combine(this.HomePath, "Scripts", "LoadVoice.py");
        public string VoicePreprocessorPath => Path.Combine(this.HomePath, "Scripts", "Preprocessing.py");
        public string PythonPath => "D:\\VisualStudio\\Shared\\Python36_64\\python.exe";
        public string DataDumpFile => Path.Combine(this.HomePath, "DataDump");
        private string HomePath => Path.Combine(HostingEnvironment.MapPath("~"), "src", "Implementation", "Python");
    }
}