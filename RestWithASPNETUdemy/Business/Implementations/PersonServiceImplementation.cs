using RestWithASPNETErudio.Model.PersonModel;
using RestWithASPNETErudio.Repository;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public PersonModel Create(PersonModel person)
        {
           
            return _repository.Create(person);
        }

       
        public void Delete(long id)
        {
            
            _repository.Delete(id);
        }

        public List<PersonModel> FindAll()
        {
            return _repository.FindAll();
        }

        public PersonModel FindById(long id)
        {
            return _repository.FindById(id);
        }

        public PersonModel Update(PersonModel person)
        {
            

           return _repository.Update(person);

        }

       
    }
}

   