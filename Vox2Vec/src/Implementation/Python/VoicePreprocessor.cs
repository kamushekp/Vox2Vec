using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation.Python
{
    public class VoicePreprocessor: IVoicePreprocessor
    {
        private readonly IResourcePaths resourcePaths;

        public VoicePreprocessor(IResourcePaths resourcePaths)
        {
            this.resourcePaths = resourcePaths;
        }

        public Vox PreprocessVoice(Voice voice)
        {
            var stringedVoice = $"[{string.Join(" ", voice.Values)}]".Replace(",", ".");
            var filepath = Path.Combine(Directory.GetCurrentDirectory(),"temp.txt");
            File.WriteAllText(filepath, stringedVoice);

            var args = $"{AroundArg(this.resourcePaths.VoicePreprocessorPath)} {AroundArg(filepath)}";
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
                var framesStringed = result
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace("[", string.Empty)
                    .Replace(".", ",")
                    .Split(']')
                    .Where(str => str.Length > 0)
                    .ToArray();

                var frames = framesStringed
                    .Select(array =>
                        array.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(number => double.Parse(number, NumberStyles.Any))
                            .Select(number => new[]{(float)number})
                            .ToArray()).ToArray();

                var properlyFormattedFrames = new float[1][][][] {frames};

                return new Vox()
                {
                    Values = properlyFormattedFrames
                };
            }
        }
        
        private static string AroundArg(string arg)
        {
            return $"\"{arg}\"";
        }
    }
}