using Orleans;
using OrleansDemo.Domain.Grains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain.Grains
{
    public interface IDeviceGrain: IGrainWithStringKey
    {
        public Task SetPosition(double latitude, double longitude);
        public Task<(double latitude, double longitude)> GetPosition();

        public Task PostChatMessage(string message);

        public Task<string> GetLocationIdentifier();
        public Task<IEnumerable<string>> GetLocationHistory();
        public Task<IEnumerable<ChatMessage>> GetChatHistory();
    }
}
