using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASPNETUdemy.Hipermedia.Abstract;

public interface IResponseEnricher
{
   bool CanEnrich(ResultExecutingContext context);
   Task Enrich(ResultExecutingContext context);

}
