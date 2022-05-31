using FluentValidation;
using MediatR;

namespace Microservice.Application.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }



        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = this.validators.Select(x => x.Validate(context))
                .SelectMany(result => result.Errors).Where(f => f != null).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}
