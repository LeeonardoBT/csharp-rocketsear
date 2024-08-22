using TaskMinder.Communication.Enums;

namespace TaskMinder.Communication.Responses;
public class ResponseShortTaskJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskProgress Status { get; set; }
}
