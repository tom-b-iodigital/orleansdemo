using Orleans;
using OrleansDemo.Domain.Grains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain.Grains.Implementations
{
    public class LocationGrain : Orleans.Grain, ILocationGrain
    {
        private List<IDeviceGrain> _devices = new List<IDeviceGrain>();
        private List<ChatMessage> _chatMessages = new List<ChatMessage>();

        public Task<IEnumerable<IDeviceGrain>> GetDevices()
        {
            return Task.FromResult((IEnumerable<IDeviceGrain>)_devices);
        }

        public Task<IEnumerable<ChatMessage>> GetChatHistory()
        {
            return Task.FromResult((IEnumerable<ChatMessage>)_chatMessages);
        }

        public Task AddDevice(IDeviceGrain deviceGrain)
        {
            if (!_devices.Any(g => g.GetPrimaryKeyString() == deviceGrain.GetPrimaryKeyString()))
            {
                _devices.Add(deviceGrain);
            }

            return Task.CompletedTask;
        }

        public Task RemoveDevice(IDeviceGrain deviceGrain)
        {
            _devices.RemoveAll(g => g.GetPrimaryKeyString() == deviceGrain.GetPrimaryKeyString());

            return Task.CompletedTask;
        }

        public Task AddChatMessage(ChatMessage chatMessage)
        {
            _chatMessages.Add(chatMessage);

            return Task.CompletedTask;
        }
    }
}
