﻿using System.Threading.Tasks;

namespace FitnessTracker.Application.Common
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);

        Task<TResult> HandleAsync(TQuery query);
    }
}