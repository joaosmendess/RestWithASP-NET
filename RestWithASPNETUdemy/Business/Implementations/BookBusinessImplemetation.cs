using RestWithASPNETErudio.Data.Converter.Implementations;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.BookModel;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class BookBusinessImplemetation : IBookBusiness
    {
        private readonly IRepository<BookModel> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplemetation(IRepository<BookModel> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
           
            var bookEntity = _converter.Parse(book);
           bookEntity = _repository.Create(bookEntity);
           return _converter.Parse(bookEntity);
        }

       
        public void Delete(long id)
        {
            
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
           return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            

            var bookEntity = _converter.Parse(book);
           bookEntity = _repository.Update(bookEntity);
           return _converter.Parse(bookEntity);

        }

       
    }
}

   