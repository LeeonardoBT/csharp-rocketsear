using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Users.ChangePassword;
public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
