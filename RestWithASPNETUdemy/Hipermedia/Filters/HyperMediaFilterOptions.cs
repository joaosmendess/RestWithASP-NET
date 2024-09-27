using System.Collections.Generic;
using RestWithASPNETUdemy.Hipermedia.Abstract;

namespace RestWithASPNETErudio.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; }

        public HyperMediaFilterOptions()
        {
            ContentResponseEnricherList = new List<IResponseEnricher>();
        }
    }
}