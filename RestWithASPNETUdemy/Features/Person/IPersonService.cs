namespace RestWithASPNETErudio.Features.Person
{
    public interface IPersonService 
    {
        PersonModel Create(PersonModel person);
        PersonModel FindById(long id);
        List<PersonModel> FindAll();
        PersonModel Update(PersonModel person);

        void Delete(long id);
    }
}