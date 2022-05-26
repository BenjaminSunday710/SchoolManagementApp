using System;
using System.Collections.Generic;

namespace Shared.Application.ArchitectureBuilder.Commands
{
    public class CommandResponse
    {
        public CommandResponse(Guid id)
        {
            Id = id;
            InvalidItems = new List<string>();
        }

        public void NotifyInvalidItems(string error)
        {
            InvalidItems.Add(error);
        }

        public CommandResponse NotifyInvalidItems(List<string> errors)
        {
            InvalidItems.AddRange(errors);
            return this;
        }

        public Guid Id { get; set; }
        public List<string> InvalidItems { get; set; }
    }
}
