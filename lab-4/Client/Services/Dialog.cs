using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;

namespace Client.Services
{
    public class DialogService
    {
        private List<IResponce> _modules;

        public DialogService()
        {
            _modules = new List<IResponce>();
            _modules.Add(CreateModule(new HelloResponce()));
        }

        private IResponce CreateModule(IResponce module)
        {
            module.KeyWords = new List<string>(){"Привет"};
            return module;
        }

        public async Task<string> GetResponce(string request)
        {
            foreach (IResponce module in _modules)
            {
                string responce = await Task.FromResult(module.GetResponce(request));
                if (responce != null)
                    return responce;
            }
            return null;
        }
    }
}