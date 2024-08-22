using TaskMinder.Communication.Requests;
using TaskMinder.Communication.Responses;

namespace TaskMinder.Application.UseCases.Tasks.Register;
public class RegisterTaskUseCase
{
    public ResponseTaskRegisterJson Execute(RequestTaskJson request)
    {
        ResponseTaskRegisterJson response = new ResponseTaskRegisterJson
        {
            Id = 1,
            Name = request.Name,
        };

        return response;
    }
}
