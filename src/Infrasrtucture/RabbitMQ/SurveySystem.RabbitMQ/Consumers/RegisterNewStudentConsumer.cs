using MassTransit;
using MediatR;
using SurveySystem.Aplication.Requests.Auth.Register;
using SurveySystem.Requests.MessageBroker;

namespace SurveySystem.RabbitMQ.Consumers
{
    public class RegisterNewStudentConsumer : IConsumer<RegisterNewStudentFromBrokerDTO>
    {
        private readonly IMediator _mediator;

        public RegisterNewStudentConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RegisterNewStudentFromBrokerDTO> context)
        {
            var request = context.Message;
            var registerStudentCommand = new RegisterStudentCommand()
            {
                Login = request.Login,
                Password = request.Password,
                FullName = request.FullName
            };

            await _mediator.Send(registerStudentCommand);
        }
    }
}
