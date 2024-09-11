using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Billings.GetById;
public class GetBillingByIdUseCase : IGetBillingByIdUseCase
{
    private readonly IBillingReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetBillingByIdUseCase(IBillingReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseBillingJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if (result == null) 
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }

        return _mapper.Map<ResponseBillingJson>(result);
    }
}
