using FluentValidation.Results;
using MediatR;
using System;

namespace DevBoost.DroneDelivery.Core.Domain.Messages
{
    public class Command : Message, IRequest<bool>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
