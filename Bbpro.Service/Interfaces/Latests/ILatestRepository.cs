﻿using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Latests;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Models.Latests;
using Bbpro.Domain.Models.PaginationParams;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Latests;

public interface ILatestRepository
{
    ValueTask<PagedResult<LatestModel>> GetAll(PaginationParams @params, Expression<Func<Latest, bool>> expression = null);
    ValueTask<LatestModel> GetAsync(Expression<Func<Latest, bool>> expression);
    ValueTask<LatestModel> CreateAsync(LatestCreateDto latestCreateDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<LatestModel> UpdateAsync(int id, LatestUpdateDto latestUpdateDTO);
}
