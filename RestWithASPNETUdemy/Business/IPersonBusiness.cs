using RestWithASPNETErudio.Model.PersonModel;
namespace RestWithASPNETErudio.Business
{
    public interface IPersonBusiness
    {
        PersonModel Create(PersonModel person);
        PersonModel FindById(long id);
        List<PersonModel> FindAll();
        PersonModel Update(PersonModel person);

        void Delete(long id);
    }
}