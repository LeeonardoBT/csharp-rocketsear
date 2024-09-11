using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestBillingJson, BillingService>();
    }

    private void EntityToResponse()
    {
        CreateMap<BillingService, ResponseBillingRegisterJson>();
        CreateMap<BillingService, ResponseShortBillingJson>();
        CreateMap<BillingService, ResponseBillingJson>();
    }
}
