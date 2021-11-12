using Api.Core.Exceptions;

namespace Api.Application.Exceptions.Pagination
{
    public class NegativePaginationParameterException : ApplicationException
    {
        public override string Code => "negative_pagination_parameter_exception";

        public NegativePaginationParameterException(string parameterName) : base(
            $"Value cannot be negative, parameter: {parameterName}")
        {
        }
    }
}