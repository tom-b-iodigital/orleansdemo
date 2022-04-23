using Orleans;
using OrleansDemo.Domain.Grains.Models;

namespace OrleansDemo.Domain.Grains
{
    public interface ILocationGrain: IGrainWithStringKey
    {
        public Task AddDevice(IDeviceGrain deviceGrain);
        public Task RemoveDevice(IDeviceGrain deviceGrain);

        public Task AddChatMessage(ChatMessage chatMessage);

        public Task<IEnumerable<IDeviceGrain>> GetDevices();

        public Task<IEnumerable<ChatMessage>> GetChatHistory();
    }
}
