namespace qdaf.PacketValidator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Properties;

    public class DefaultPacketValidator : IPacketValidator
    {
        public QdafValidationResult Validate(DataPacket dataPacket)
        {
            var errors = new Lazy<List<string>>(() => new List<string>());

            var result = new QdafValidationResult();

            if (dataPacket == null)
            {
                errors.Value.Add(string.Format(CoreResources.CannotBeNullMessage, "dataPacket"));
                result.Errors = errors.Value;
                return result;
            }

            if (dataPacket.Commands == null)
            {
                errors.Value.Add(string.Format(CoreResources.CannotBeNullMessage, "dataPacket.Commands"));
                result.Errors = errors.Value;
                return result;
            }

            var commands = dataPacket.Commands.ToList();

            for (var i = 0; i < commands.Count(); i++)
            {
                try
                {
                    Validate(commands[i]);
                }
                catch (Exception ex)
                {
                    var command = commands[i];

                    if (command == null || command.Command == null)
                    {
                        throw new QdafException(string.Format("Command at index {0} is null", i));
                    }

                    command.Errors = new List<string>(command.Errors ?? new string[0])
                        {
                            ex.Message
                        };
                }
            }

            if (errors.IsValueCreated && errors.Value.Any())
            {
                result.Errors = errors.Value;
            }

            result.Valid = true;

            return result;
        }

        private static void Validate(IPacketCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (command.Command == null)
            {
                throw new QdafException(string.Format(CoreResources.CannotBeNullMessage, "command.Command"));
            }
        }
    }
}
