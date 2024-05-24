using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.RabbitMQ;
using SurveySystem.RabbitMQ.Consumers;
using System.Reflection;


namespace SurveySystem.Services
{
    /// <summary>
    /// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Добавить службы проекта с логикой
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, BrokerOptions options)
        {            
            var consumers = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IConsumer<>)))
                .ToDictionary(x => x.Name, x => x);          

            return services.AddMassTransit(x =>
            {
                x.AddConsumer<RegisterNewStudentConsumer>();
                x.AddConsumer<UpdateStudentInfoConsumer>();

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(options.HostName, c =>
                    {
                        c.Username("root");
                        c.Password("123");
                    });

                    foreach (var item in options.Consumers)
                    {
                        config.ReceiveEndpoint(item.Queue, e =>
                        {
                            //var queueName = item.Queue.
                        })
                    }

                    config.ReceiveEndpoint("RegisterStudentsQueue", e =>
                    {
                        e.ConfigureConsumer<RegisterNewStudentConsumer>(context);
                    });
                    config.ReceiveEndpoint("UpdateStudentsInfoQueue", e =>
                    {
                        e.ConfigureConsumer<UpdateStudentInfoConsumer>(context);
                    });

                    config.ClearSerialization();
                    config.UseRawJsonSerializer();
                    config.ConfigureEndpoints(context);
                });
            });

        }
    }
}
