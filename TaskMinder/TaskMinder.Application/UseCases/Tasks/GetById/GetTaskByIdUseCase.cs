using TaskMinder.Communication.Responses;

namespace TaskMinder.Application.UseCases.Tasks.GetById;
public class GetTaskByIdUseCase
{
    public ResponseTaskJson Execute(int id)
    {
        return new ResponseTaskJson
        {
            Id = id,
            Name = "Test the Task Minder API",
            Description = "Need to test the development of the Task Minder API Project",
            CreationDate = new DateTime(year: 2024, month: 8, day: 20),
            LimitDate = new DateTime(year: 2024, month: 8, day: 21),
            Priority = Communication.Enums.PriorityLevel.High,
            Status = Communication.Enums.TaskProgress.Doing,
        };
    }
}
