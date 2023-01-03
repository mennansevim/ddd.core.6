using System;
using Application.Common;
using Application.Common.Data;
using Application.Common.Outbox;
using Domain.B2BMasters;
using Domain.PreReservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Common.DomainEventsDispatching;
using Infrastructure.Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Services;

namespace Infrastructure.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureDependency(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<B2BContext>(options =>
                    options.UseInMemoryDatabase("OrderDb"));
            }
            else
            {
                services.AddDbContext<B2BContext>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")
                    )
                );
            }

            services.AddScoped<DbContext, B2BContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IB2BStatusHistoryService, B2BStatusHistoryService>();
            services.AddScoped<IB2BMasterRepository, B2BMasterRepository>(); 
            services.AddSingleton<DomainEventKafkaMapper>();
            services.AddScoped<IOutbox, Outbox.Outbox>(); 

            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
        }
    }
}