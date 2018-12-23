using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetSavedMenuItemsQuery : IRequest<List<FoodInfoDTO>>
    {
    }
}