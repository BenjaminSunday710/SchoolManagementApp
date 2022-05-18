namespace Shared.Application.ArchitectureBuilder.Commands
{
    public interface ICommand
    {
        bool IsValid { get; }
    }
}
