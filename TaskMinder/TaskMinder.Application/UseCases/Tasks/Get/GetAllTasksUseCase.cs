using TaskMinder.Communication.Responses;

namespace TaskMinder.Application.UseCases.Tasks.Get;
public class GetAllTasksUseCase
{
    public ResponseAllTasksJson Execute() {
        return new ResponseAllTasksJson
        {
            Tasks = new List<ResponseShortTaskJson>
            {
                new ResponseShortTaskJson
                {
                    Id = 1,
                    Name = "Watch the classes",
                    Status = Communication.Enums.TaskProgress.Done
                },
                new ResponseShortTaskJson
                {
                    Id = 2,
                    Name = "Create Task Manager Project",
                    Status = Communication.Enums.TaskProgress.Doing
                },
                new ResponseShortTaskJson
                {
                    Id = 3,
                    Name = "Publish on GitHub",
                    Status = Communication.Enums.TaskProgress.ToDo
                },
                new ResponseShortTaskJson
                {
                    Id = 4,
                    Name = "Send the GitHub Project URL to RocketSeat",
                    Status = Communication.Enums.TaskProgress.ToDo
                }
            }
        };
    }
}
