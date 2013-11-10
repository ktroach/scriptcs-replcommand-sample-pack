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
                Console.WriteLine(command);
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

        public virtual string GetClearScript()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("System.Console.Clear();");
            return sb.ToString();
        }

        public virtual string GetPiScript()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("var pi = {0};", Math.PI);
            sb.AppendFormat("System.Console.WriteLine(pi);");
            return sb.ToString();
        }

        public virtual string GetTouchScript()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("using (System.IO.TextWriter textWriter = new System.IO.StreamWriter(arg1))");
            sb.Append("{");
            sb.AppendFormat(" textWriter.WriteLine(arg2); ");
            sb.Append("}");
            return sb.ToString();
        }

        public virtual string GetTimeScript()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("System.Console.WriteLine(System.DateTime.Now);");
            return sb.ToString();
        }

        public virtual string GetFilesScript()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("var files = System.IO.Directory.GetFiles(arg1); ");
            sb.AppendFormat("foreach (var file in files) ");
            sb.Append("{ ");
            sb.AppendFormat(" System.Console.WriteLine(file);");
            sb.Append("} ");
            return sb.ToString();
        }
    }
}
