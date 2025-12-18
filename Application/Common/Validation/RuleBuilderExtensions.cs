using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Validation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, Guid> MustExist<T>(
            this IRuleBuilder<T, Guid> rule,
            Func<Guid, CancellationToken, Task<bool>> existsFunc,
            string entityName)
        {
            return rule.MustAsync(existsFunc)
                .WithMessage(id => $"{entityName} with Id {id} was not found.");
        }

        public static IRuleBuilderOptions<T, Guid?> MustExistIfProvided<T>(
            this IRuleBuilder<T, Guid?> rule,
            Func<Guid, CancellationToken, Task<bool>> existsFunc,
            string entityName)
        {
            return rule.MustAsync(async (id, ct) =>
                    !id.HasValue || await existsFunc(id.Value, ct))
                .WithMessage(id => $"{entityName} with Id {id} was not found.");
        }
    }
}
