using RestWithASPNETErudio.Model.PersonModel;

namespace RestWithASPNETErudio.Repository
{
    public interface IPersonRepository
    {
        PersonModel Create(PersonModel person);
        PersonModel FindById(long id);
        List<PersonModel> FindAll();
        PersonModel Update(PersonModel person);

        void Delete(long id);

        bool Exists(long id);
    }
}