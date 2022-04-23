using Geohash;
using Orleans.Runtime;
using OrleansDemo.Domain.Grains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain.Grains.Implementations
{
    public class DeviceGrain: Orleans.Grain, IDeviceGrain
    {
        private readonly IPersistentState<List<ChatMessage>> _chatMessages;
        private readonly IPersistentState<List<string>> _locationHistory;

        private string? _locationIdentifier = null;
        private double _longitude;
        private double _latitude;

        public DeviceGrain([PersistentState("grainStorage")] IPersistentState<List<ChatMessage>> chatMessages,
            [PersistentState("grainStorage")] IPersistentState<List<string>> locationHistory)
        {
            _chatMessages = chatMessages;
            _locationHistory = locationHistory;
        }

        public Task<IEnumerable<ChatMessage>> GetChatHistory()
        {
            if (_chatMessages.RecordExists)
            {
                return Task.FromResult(Enumerable.Empty<ChatMessage>());
            }

            return Task.FromResult((IEnumerable<ChatMessage>)_chatMessages.State);
        }

        public Task<string> GetLocationIdentifier()
        {
            return Task.FromResult(_locationIdentifier ?? "not yet known");
        }

        public Task<(double latitude, double longitude)> GetPosition()
        {
            return Task.FromResult((_latitude, _longitude));
        }

        public Task<IEnumerable<string>> GetLocationHistory()
        {
            if (_locationHistory.RecordExists)
            {
                return Task.FromResult(Enumerable.Empty<string>());
            }

            return Task.FromResult((IEnumerable<string>)_locationHistory.State);
        }

        public async Task PostChatMessage(string message)
        {
            if (!_chatMessages.RecordExists)
            {
                _chatMessages.State = new List<ChatMessage>(); 
            }

            if (_locationIdentifier == null)
            {
                return;
            }

            ChatMessage newMessage = new()
            {
                LocationId = _locationIdentifier,
                PostedAt = DateTime.UtcNow,
                Message = message,
            };

            _chatMessages.State.Add(newMessage);

            await _chatMessages.WriteStateAsync();

            await GrainFactory.GetGrain<ILocationGrain>(_locationIdentifier).AddChatMessage(newMessage);
        }

        public async Task SetPosition(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;

            // calculate location identifier
            Geohasher geohasher = new();
            string newLocationIdentifier = geohasher.Encode(latitude, longitude, 5);

            if (newLocationIdentifier != _locationIdentifier)
            {
                // remove from previous location
                if (_locationIdentifier != null)
                {
                    await GrainFactory.GetGrain<ILocationGrain>(_locationIdentifier).RemoveDevice(this);
                }

                _locationIdentifier = newLocationIdentifier;

                if (!_locationHistory.RecordExists)
                {
                    _locationHistory.State = new List<string>();
                }

                _locationHistory.State.Add(_locationIdentifier);
                await _locationHistory.WriteStateAsync();

                await GrainFactory.GetGrain<ILocationGrain>(_locationIdentifier).AddDevice(this);
            }
        }
    }
}
