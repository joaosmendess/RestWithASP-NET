using RestWithASPNETErudio.Model.BookModel;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        BookModel Create(BookModel book);
        BookModel FindById(long id);
        List<BookModel> FindAll();
        BookModel Update(BookModel book);

        void Delete(long id);
    }
}