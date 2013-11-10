using System.Collections.Generic;
using ScriptCs.Contracts;

namespace ScriptCs.ReplCommand.Pack
{
    public sealed class ReplCommandPackContext : IScriptPackContext
    {
        public IDictionary<string, string> ReplCommands { get; set; }

        private IReplCommandScriptSamples _replCommandScriptSamples;

        public ReplCommandPackContext()
        {
            ReplCommands = new Dictionary<string, string>();
            LoadReplCommandScripts();
         }

        public IReplCommandScriptSamples GetReplCommandScriptSamples()
        {
            return _replCommandScriptSamples ?? 
                (_replCommandScriptSamples = new ReplCommandScriptSamples());
        }

        public void LoadReplCommandScripts()
        {
            var sampleCommands = GetReplCommandScriptSamples().SampleCommands;
            foreach (var sampleCommand in sampleCommands)
            {
                ReplCommandPackUtils.MapCommand(ReplCommands, sampleCommand.Key, sampleCommand.Value, null);
            }
        }
    }
}