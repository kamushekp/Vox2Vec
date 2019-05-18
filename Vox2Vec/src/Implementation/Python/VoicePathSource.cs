using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation.Python
{
    public class VoicePathSource : IVoicePathSource
    {
        private readonly IResourcePaths resourcePaths;

        public VoicePathSource(IResourcePaths resourcePaths)
        {
            this.resourcePaths = resourcePaths;
        }


        public Voice Get(string filename)
        {
            var args = $"{AroundArg(this.resourcePaths.LoadVoiceScriptPath)} {AroundArg(filename)}";
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(this.resourcePaths.PythonPath, args)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();

            using (var reader = p.StandardOutput)
            {
                var result = reader.ReadToEnd();
                var preprocessed = result
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Replace(".", ",")
                    .Split(' ')
                    .Where(str => str.Length > 0);
                return new Voice
                {
                    Values = preprocessed
                        .Select(str => double.Parse(str, NumberStyles.Any))
                        .ToArray()
                };
        }
    }

        private static string AroundArg(string arg)
        {
            return $"\"{arg}\"";
        }
            
    }
}