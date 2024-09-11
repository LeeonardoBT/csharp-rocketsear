using Microsoft.Extensions.DependencyInjection;
using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Application.UseCases.Billings.Update;
using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Delete;
using BarberBoss.Application.UseCases.Billings.Reports.Excel;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf;

namespace BarberBoss.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IGetAllBillingUseCase, GetAllBillingUseCase>();
        services.AddScoped<IGetBillingByIdUseCase, GetBillingByIdUseCase>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IGenerateBillingReportExcelUseCase, GenerateBillingReportExcelUseCase>();
        services.AddScoped<IGenerateBillingReportPdfUseCase, GenerateBillingReportPdfUseCase>();
    }
}
