using TaskMinder.Communication.Enums;

namespace TaskMinder.Communication.Requests;
public class RequestTaskJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PriorityLevel Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LimitDate { get; set; }
    public TaskProgress Status { get; set; }
}
