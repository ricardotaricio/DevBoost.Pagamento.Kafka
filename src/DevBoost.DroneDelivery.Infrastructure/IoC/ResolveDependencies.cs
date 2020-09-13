using AutoMapper;
using Confluent.Kafka;
using DevBoost.DroneDelivery.Application.Bus;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.Resources;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using DevBoost.DroneDelivery.Domain.Interfaces;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using DevBoost.DroneDelivery.Infrastructure.AcessoAoUsuario;
using DevBoost.DroneDelivery.Infrastructure.AutoMapper;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using DevBoost.DroneDelivery.Infrastructure.Data.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Kafka;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using System;
using System.Diagnostics.CodeAnalysis;


namespace DevBoost.DroneDelivery.CrossCutting.IOC
{
    [ExcludeFromCodeCoverage]

    public static class ResolveDependencies
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
           

            services.AddScoped<IDroneItinerarioRepository, DroneItinerarioRepository>();
            services.AddScoped<IDroneRepository, DroneRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IUsuarioAutenticado, UsuarioAutenticado>();

            
            //Queries  DroneItinerario
            services.AddScoped<IDroneItinerarioQueries, DroneItinerarioQueries>();

            //Queries  Usuario
            services.AddScoped<IUsuarioQueries, UsuarioQueries>();

            //Queries  Pedido
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            //Queries Cliente
            services.AddScoped<IClienteQueries, ClienteQueries>();

            //Queries Drone
            services.AddScoped<IDroneQueries, DroneQueries>();

            //Event liente
            services.AddScoped<INotificationHandler<ClienteAdiconadoEvent>, ClienteEventHandler>();

            //Event Drone
            services.AddScoped<INotificationHandler<AutonomiaAtualizadaDroneEvent>, DroneEventHandler>();
            services.AddScoped<INotificationHandler<DroneAdicionadoEvent>, DroneEventHandler>();
            services.AddScoped<INotificationHandler<AutonomiaAtualizadaDroneEvent>, DroneEventHandler>();


            //Command Cliente 
            services.AddScoped<IRequestHandler<AdicionarClienteCommand, bool>, ClienteCommandHandler>();


            //Command Drone 
            services.AddScoped<IRequestHandler<AdicionarDroneCommand, bool>, DroneCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarAutonomiaDroneCommand, bool>, DroneCommandHandler>();

            //Command Usuario
            services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, bool>, UsuarioCommandHandler>();

            //Command Pedido
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarSituacaoPedidoCommand, bool>, PedidoCommandHandler>();

            //Command Drone Itinerario 
            services.AddScoped<IRequestHandler<AdicionarDroneItinerarioCommand, bool>, DroneItinerarioCommandHandler>();




            TokenGenerator.TokenConfig = configuration.GetSection("Token").Get<Token>();
            Loja.Localizacao = configuration.GetSection("Localizacao").Get<Localizacao>();

           

            var assembly = AppDomain.CurrentDomain.Load("DevBoost.DroneDelivery.Application");
            services.AddMediatR(assembly);
            services.AddTransient<IMediatrHandler, MediatrHandler>();


            services.AddAutoMapper(typeof(DtoToCommandMappingProfile),
                typeof(CommandToDomainMappingProfile),
                typeof(ViewModelToCommandMappingProfile),
                typeof(DomainToDtoMappingProfile),
                typeof(ViewModelToDomainMappingProfile));



            services.AddDbContext<DCDroneDelivery>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));



            return services;
        }

        public static IApplicationBuilder Register(this IApplicationBuilder app)
        {
            app.ApplicationServices.UseRebus();

            return app;
        }
    }
}
