using AutoMapper;

namespace FitnessTracker.Application.Common
{
    public class HandlerBase<TResult>
    {
        protected TResult _service;
        protected IMapper _mapper;

        public HandlerBase(TResult service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}