using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {        
        TResult Handle(TQuery query);
        Task<TResult> HandleAsync(TQuery query);

    }
}