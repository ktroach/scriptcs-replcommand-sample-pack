using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCs.ReplCommand.Pack
{
    public interface IReplCommandScriptSamples
    {
        IDictionary<string, string> SampleCommands { get; set; }
    }

    public class ReplCommandScriptSamples : IReplCommandScriptSamples
    {
        public IDictionary<string, string> SampleCommands { get; set; }

        public ReplCommandScriptSamples()
        {
            SampleCommands = new Dictionary<string, string>();
            LoadSampleCommands();
        }

        private void LoadSampleCommands()
        {
            var fileSystem = new FileSystem();
            var commandSymbols = new List<string> { ":", "#" };
            var fileContents = new StringBuilder();
            var path = fileSystem.GetWorkingDirectory(fileSystem.CurrentDirectory);
            var files = fileSystem.EnumerateFiles(path, "*.scriptcommands", SearchOption.TopDirectoryOnly);

            foreach (var fileName in files.Select(file => Path.Combine(fileSystem.CurrentDirectory, file)))
            {
                using (TextReader textReader = new StreamReader(fileName))
                {
                    fileContents.AppendFormat("{0}", textReader.ReadToEnd());
                }
            }

            var commandstoParse = fileContents.ToString();
            if (string.IsNullOrWhiteSpace(commandstoParse)) return;

            var commandScripts = SplitLines(commandstoParse);

            foreach (var command in commandScripts.Where(command => !string.IsNullOrWhiteSpace(command)))
            {
                var commandNameLength = command.IndexOf("]", StringComparison.Ordinal);
                var commandName = command.Substring(0, commandNameLength);
                var commandScript = command.Substring(commandNameLength + 1);
                ReplCommandPackUtils.MapCommand(SampleCommands, commandName, commandScript, commandSymbols);
            }
        }

        public IEnumerable<string> SplitLines(string value)
        {
            return value.Split(new[] { "[" }, StringSplitOptions.None);
        }
    }
}
