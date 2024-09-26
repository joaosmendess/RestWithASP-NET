using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.BookModel;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class BookBusinessImplemetation : IBookBusiness
    {
        private readonly IRepository<BookModel> _repository;

        public BookBusinessImplemetation(IRepository<BookModel> repository)
        {
            _repository = repository;
        }

        public BookModel Create(BookModel book)
        {
           
            return _repository.Create(book);
        }

       
        public void Delete(long id)
        {
            
            _repository.Delete(id);
        }

        public List<BookModel> FindAll()
        {
            return _repository.FindAll();
        }

        public BookModel FindById(long id)
        {
            return _repository.FindById(id);
        }

        public BookModel Update(BookModel book)
        {
            

           return _repository.Update(book);

        }

       
    }
}

   