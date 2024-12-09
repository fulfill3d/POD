using System.Collections.Specialized;
using FluentValidation;
using POD.API.Common.Core.Helper.Query;
using POD.Common.Service.Interfaces;

namespace POD.API.Common.Core.Mapper
{
    public class PaginationParametersMapper(IValidator<Pagination> validator) : IMapper<NameValueCollection, Pagination>
    {
        public Pagination Map(NameValueCollection nameValuesCollection)
        {
            var result = new Pagination
            {
                PageNumber = nameValuesCollection.TryGetIntValueOrDefault("pageNumber", 1),
                PageSize = nameValuesCollection.TryGetIntValueOrDefault("pageSize", 100),
            };
            validator.ValidateAndThrow(result);
            return result;
        }
    }
}