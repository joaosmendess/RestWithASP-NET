    using RestWithASPNETErudio.Data.Converter.Contract;
    using RestWithASPNETErudio.Data.VO;
    using RestWithASPNETErudio.Model.BookModel;


    namespace RestWithASPNETErudio.Data.Converter.Implementations
    {
        public class BookConverter : IParser<BookVO, BookModel>, IParser< BookModel, BookVO>
        {
            public BookModel Parse(BookVO origin)
            {
            if (origin == null) return null;
                return new BookModel 
                {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate,
                
                
                };
            }


            public BookVO Parse(BookModel origin)
            {
            if (origin == null) return null;
                return new BookVO
                {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate,
                };
            }

            public List<BookModel> Parse(List<BookVO> origin)
            {
                if (origin == null) return null;
                return origin.Select(item => Parse(item)).ToList();
            }
            public List<BookVO> Parse(List<BookModel> origin)
            {
                if (origin == null) return null;
                return origin.Select(item => Parse(item)).ToList();
            }
        }
    }