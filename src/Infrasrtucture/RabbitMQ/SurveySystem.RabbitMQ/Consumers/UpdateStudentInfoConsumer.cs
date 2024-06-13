using MassTransit;
using MediatR;
using SurveySystem.Aplication.Requests.MessageBroker.UpdateStudentInfio;
using SurveySystem.Requests.MessageBroker;

namespace SurveySystem.RabbitMQ.Consumers
{
    public class UpdateStudentInfoConsumer : IConsumer<UpdateStudentInfoFromBrokerDTO>
    {
        private readonly IMediator _mediator;

        public UpdateStudentInfoConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UpdateStudentInfoFromBrokerDTO> context)
        {
            var request = context.Message;

            var updateCommand = new UpdateStudentInfioCommand();

            await _mediator.Send(updateCommand);

            //var registerStudentCommand = new RegisterNewStudentCommand()
            //{
            //    Login = request.Login,
            //    Password = request.Password,
            //    FullName = request.FullName
            //};

            //await _mediator.Send(registerStudentCommand);

        }
    }
}
