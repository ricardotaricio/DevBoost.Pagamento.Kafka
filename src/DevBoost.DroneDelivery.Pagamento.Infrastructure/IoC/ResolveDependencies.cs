using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using DevBoost.DroneDelivery.Pagamento.Application.Bus;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.Events;
using DevBoost.DroneDelivery.Pagamento.Application.Queries;
using DevBoost.DroneDelivery.Pagamento.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.AutoMapper;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Contexts;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Repositories;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.IOC
{
    [ExcludeFromCodeCoverage]

    public static class ResolveDependencies
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            
            TokenGenerator.TokenConfig = configuration.GetSection("Token").Get<Token>();

            var assembly = AppDomain.CurrentDomain.Load("DevBoost.DroneDelivery.Pagamento.Application");
            services.AddMediatR(assembly);
            services.AddTransient<IMediatrHandler, MediatrHandler>();
            services.AddTransient<IBus, Bus>();

            services.AddScoped<IRequestHandler<AdicionarPagamentoCartaoCommand, bool>, PagamentoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarSituacaoPagamentoCartaoCommand, bool>, PagamentoCommandHandler>();
            services.AddScoped<INotificationHandler<PagamentoCartaoAdicionadoEvent>, PagamentoEventHandler>();
            services.AddScoped<IPagamentoQueries, PagamentoQueries>();

            services.AddAutoMapper(typeof(DtoToCommandMappingProfile), typeof(CommandToDomainMappingProfile), typeof(ViewModelToCommandMappingProfile), typeof(DomainToDtoMappingProfile));

            services.AddDbContext<PagamentoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
