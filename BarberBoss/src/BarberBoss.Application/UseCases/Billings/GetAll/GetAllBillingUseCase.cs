using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Billing;

namespace BarberBoss.Application.UseCases.Billings.GetAll;
public class GetAllBillingUseCase : IGetAllBillingUseCase
{
    private readonly IBillingReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllBillingUseCase(IBillingReadOnlyRepository repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBillingListJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseBillingListJson
        {
            Billings = _mapper.Map<List<ResponseShortBillingJson>>(result)
        };
    }
}
