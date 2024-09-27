
using RestWithASPNETUdemy.Hipermedia.Filters;

namespace RestWithASPNETUdemy.Hipermedia.Abstract;

public interface ISupportsHypermedia
{
  List<HyperMediaLink> Links {get; set;}

}
