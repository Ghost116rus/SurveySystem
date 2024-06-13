using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SurveySystem.Domain.Exceptions;
using SurveySystem.RabbitMQ;
using System.Reflection;


namespace SurveySystem.Services
{
    /// <summary>
    /// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Добавить в проект логику брокера сообщений
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="options">Настройки брокера сообщений</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddBroker(this IServiceCollection services, BrokerOptions options)
        {    
            // получение из сборки всех классов, реализующих интерфейс IConsumer
            var consumers = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IConsumer<>)))
                .ToDictionary(x => x.Name, x => x);          

            return services.AddMassTransit(x =>
            {
                foreach (var consumer in consumers.Values)                
                    x.AddConsumer(consumer);

                x.UsingRabbitMq((context, config) =>
                {

                    config.Host(options.HostName, c =>
                    {
                        c.Username(options.UserName);
                        c.Password(options.Password);
                    });

                    foreach (var item in options.Consumers)
                    {
                        config.ReceiveEndpoint(item.Queue, e =>
                        {
                            var consumerName = item.Queue.Replace("Queue", "") + "Consumer";
                            if (consumers.ContainsKey(consumerName))
                            {
                                var type = consumers[consumerName];

                                e.Durable = item.Durable;
                                e.PrefetchCount = item.PrefetchCount;
                                e.Exclusive = item.Exclusive;
                                e.AutoDelete = item.AutoDelete;
                                e.ConfigureConsumer(context, type);
                                consumers.Remove(consumerName);
                            }
                            else
                            {
                                throw new ExceptionBase($"Для очереди {item.Queue} не были найдены Consumer-ы");
                            }
                        });
                    }

                    if (consumers.Any())
                        Console.WriteLine($"В системе остались не сконфигрурированные консьюемры {string.Join(" ", consumers.Keys)}");

                    config.ClearSerialization();
                    config.UseRawJsonSerializer();
                    config.ConfigureEndpoints(context);
                });
            });
        }
    }
}
