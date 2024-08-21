using Petfolio.Communication.Requests;
using Petfolio.Communication.Responses;

namespace Petfolio.Application.UseCases.Pets.Register;
public class RegisterPetUseCase
{
    public ResponseRegisterPetJson Execute(RequestPetJson request)
    {
        ResponseRegisterPetJson response = new ResponseRegisterPetJson
        {
            Id = 1,
            Name = request.Name,
        };

        return response;
    }
}
