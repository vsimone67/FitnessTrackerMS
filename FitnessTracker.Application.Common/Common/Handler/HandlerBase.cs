using AutoMapper;

namespace FitnessTracker.Application.Common
{
    public class HandlerBase<TResult>
    {
        protected TResult _service; //TODO: REMOVE
        protected IMapper _mapper;
        protected TResult _repository;

        public HandlerBase(TResult repository, IMapper mapper)
        {
            _service = repository;
            _repository = repository;
            _mapper = mapper;
        }
    }
}