using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Users.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
