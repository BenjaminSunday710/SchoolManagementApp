using System.Collections.Generic;

namespace Shared.Application.ArchitectureBuilder.Commands
{
    public class CommandResponse
    {
        public CommandResponse(int id)
        {
            Id = id;
            InvalidItems = new List<string>();
        }

        public void NotifyInvalidItems(string error)
        {
            InvalidItems.Add(error);
        }

        public int Id { get; set; }
        public List<string> InvalidItems { get; set; }
    }
}
