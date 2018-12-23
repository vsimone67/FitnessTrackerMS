using AutoMapper;

namespace FitnessTracker.Application.Common
{
    public class HandlerBase<TResult>
    {
        protected IMapper _mapper;
        protected TResult _repository;

        public HandlerBase(TResult repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}