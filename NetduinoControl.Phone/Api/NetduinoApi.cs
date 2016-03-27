using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetduinoControl.Api;
using Newtonsoft.Json;

namespace NetduinoControl.Phone.Api
{
    class NetduinoApi
    {
        public Settings Settings { get; private set; }

        private readonly HttpClient _client;

        public NetduinoApi(Settings settings)
        {
            Settings = settings;

            _client = new HttpClient();
        }

        public async Task<bool[]> GetOutletStates()
        {
            OutletApiResult result = await GetResult("Outlets", "GetStates");
            return result.States;
        }
        
        public async Task<bool> GetOutletState(int index)
        {
            OutletApiResult result = await GetResult("Outlets", "GetState", index);
            return result.State;
        }

        public async Task<bool> SetOutletState(int index, bool value)
        {
            OutletApiResult result = await GetResult("Outlets", "SetState", index, value);
            return result.State;
        }

        private async Task<OutletApiResult> GetResult(string controller, string action, params object[] args)
        {
            StringBuilder request = new StringBuilder(controller);

            if (action != null)
                request.Append("/" + action);

            if (args != null)
            {
                foreach (object o in args)
                {
                    request.Append("/" + o);
                }    
            }

            string url = "http://" + Settings.IPAddress + "/" + request;
            string response = await _client.GetStringAsync(url);
            OutletApiResult result = JsonConvert.DeserializeObject<OutletApiResult>(response);

            if (!result.Success)
                throw new Exception(result.Error);

            return result;
        }
    }
}
