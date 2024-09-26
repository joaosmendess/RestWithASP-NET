using RestWithASPNETErudio.Data.Converter.Contract;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model.PersonModel;

namespace RestWithASPNETErudio.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, PersonModel>, IParser<PersonModel, PersonVO>
    {
        public PersonModel Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new PersonModel 
            {
              Id = origin.Id,
              FirstName = origin.FirstName,
              LastName = origin.LastName,
              Address = origin.Address,
              Gender = origin.Gender,
              
            };
            
        }


        public PersonVO Parse(PersonModel origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
              Id = origin.Id,
              FirstName = origin.FirstName,
              LastName = origin.LastName,
              Address = origin.Address,
              Gender = origin.Gender
            };
            
        }

        public List<PersonModel> Parse(List<PersonVO> origin)
        {
           if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<PersonVO> Parse(List<PersonModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}