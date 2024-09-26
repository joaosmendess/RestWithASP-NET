using RestWithASPNETErudio.Data.Converter.Implementations;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model.PersonModel;
using RestWithASPNETErudio.Repository;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<PersonModel> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<PersonModel> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
           var personEntity = _converter.Parse(person);
           personEntity = _repository.Create(personEntity);
           return _converter.Parse(personEntity);
        }

       
        public void Delete(long id)
        {
            
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            
          var personEntity = _converter.Parse(person);
           personEntity = _repository.Update(personEntity);
           return _converter.Parse(personEntity);

        }

       
    }
}

   