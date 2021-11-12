using System;
using Api.Application.Exceptions.Pagination;

namespace Api.Application.Common
{
    public class Pagination
    {
        private const int MaxPage = int.MaxValue;
        private const int MaxResultsPerPage = 100;
        private const int DefaultPage = 0;
        private const int DefaultResultsAmount = 20;

        public int ResultsAmount { get; }
        public int PageNumber { get; }

        public static Pagination Default
            => new(DefaultResultsAmount, DefaultPage);

        public Pagination()
        {
        }

        public Pagination(int resultsAmount, int page)
        {
            if (resultsAmount < 0)
                throw new NegativePaginationParameterException(nameof(ResultsAmount));

            if (page < 0)
                throw new NegativePaginationParameterException(nameof(PageNumber));

            ResultsAmount = Math.Min(resultsAmount, MaxResultsPerPage);
            PageNumber = Math.Min(page, MaxPage);
        }
    }
}