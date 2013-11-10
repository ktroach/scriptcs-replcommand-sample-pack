using System.Collections.Generic;
using System.Linq;

namespace ScriptCs.ReplCommand.Pack
{
    public static class ReplCommandPackUtils
    {
        public static void MapCommand(IDictionary<string, string> commandStore, string moniker, 
            string script, List<string> commandSymbols)
        {
            if (commandSymbols != null && commandSymbols.Any())
            {
                foreach (var monikerKey in commandSymbols
                    .Select(commandSymbol => string.Format("{0}{1}", commandSymbol, moniker))
                    .Where(monikerKey => !commandStore.ContainsKey(monikerKey)))
                {
                    commandStore.Add(monikerKey, script);
                }
            }
            else
            {
                var scriptAdded = GetScript(moniker, commandStore);
                if (string.IsNullOrWhiteSpace(scriptAdded))
                {
                    commandStore.Add(moniker, script);
                }
            }
        }

        public static string GetScript(string command, IDictionary<string, string> commandStore)
        {
            return commandStore.ContainsKey(command) ? commandStore[command] : string.Empty;
        }
    }
}
