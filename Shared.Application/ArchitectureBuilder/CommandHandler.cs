using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.ArchitectureBuilder
{
    public abstract class CommandHandler<TCommand,TResponse> where TCommand:ICommand, TResponse:
    {
    }
}
