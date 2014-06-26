namespace qdaf.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;

    public class PacketReceiver : IPacketReceiver
    {
        private readonly Lazy<List<string>> _errors; 
        private readonly IPacketValidator _packetValidator;
        private readonly IPacketProcessor _packetProcessor;
        private readonly IQdafLogger _logger;

        public PacketReceiver(IPacketValidator packetValidator, IPacketProcessor packetProcessor, IQdafLogger logger)
        {
            _packetValidator = packetValidator;
            _packetProcessor = packetProcessor;
            _logger = logger;
            _errors = new Lazy<List<string>>(() => new List<string>()); 
        }

        public async Task ProcessPacketAsync(DataPacket dataPacket)
        {
            try
            {
                _packetValidator.Validate(dataPacket);

                dataPacket.Success = true;
            }
            catch (QdafException ex)
            {
                _errors.Value.Add(ex.Message);
            }
            catch (Exception ex)
            {
                _errors.Value.Add(ex.Message);
                _logger.Error("Error validating packet", ex);
            }

            if (dataPacket != null && _errors.IsValueCreated)
            {
                dataPacket.Errors = _errors.Value;
                return;
            }

            try
            {
                await _packetProcessor.ProcessAsync(dataPacket);
            }
            catch (QdafException ex)
            {
                _errors.Value.Add(ex.Message);
            }
            catch (Exception ex)
            {
                _errors.Value.Add(ex.Message);
                _logger.Error("Error processing packet", ex);
            } 
            
            if (dataPacket != null && _errors.IsValueCreated)
            {
                dataPacket.Errors = _errors.Value;
            }
        }
    }
}
