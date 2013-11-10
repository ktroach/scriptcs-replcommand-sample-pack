using ScriptCs.Contracts;
namespace ScriptCs.ReplCommand.Pack
{
    public class ReplCommandScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new ReplCommandPackContext();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
        }

        void IScriptPack.Terminate()
        {
        }
    }
}
