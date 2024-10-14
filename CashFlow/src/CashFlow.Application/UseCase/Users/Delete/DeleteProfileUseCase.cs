using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCase.Users.Delete;
public class DeleteProfileUseCase : IDeleteProfileUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public DeleteProfileUseCase(
        IUnitOfWork unitOfWork,
        IUserWriteOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
