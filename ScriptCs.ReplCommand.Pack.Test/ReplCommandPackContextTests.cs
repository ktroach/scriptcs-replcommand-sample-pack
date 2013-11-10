using System.Linq;
using Should;
using Xunit;

namespace ScriptCs.ReplCommand.Pack.Test
{
    public class ReplCommandPackContextTests
    {
        [Fact]
        public void ShouldGetReplCommandScriptSamples()
        {
            var context = new ReplCommandPackContext();
            var samples = context.GetReplCommandScriptSamples();
            samples.SampleCommands.ShouldNotBeNull();
            var command = samples.SampleCommands.Any(x=>x.Key=="#pi");
            command.ShouldBeTrue();
        }

        [Fact]
        public void ShouldLoadReplCommandScripts()
        {
            var context = new ReplCommandPackContext();
            var replCommands = context.ReplCommands;
            replCommands.ShouldNotBeNull();
            var command = replCommands.Any(x => x.Key == "#pi");
            command.ShouldBeTrue();
        }
    }
}
