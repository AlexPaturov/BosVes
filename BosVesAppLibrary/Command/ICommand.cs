namespace BosVesAppLibrary.Command;
public interface ICommand
{
   Task<bool> Execute();
   Task Undo();
}
