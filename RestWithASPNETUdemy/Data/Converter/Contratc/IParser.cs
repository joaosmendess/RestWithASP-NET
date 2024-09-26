namespace RestWithASPNETErudio.Data.Converter.Contract
{
        public interface IParser<O, D> // ORIGEM E DESTINO
        {
            D Parse(O origin);
            List <D> Parse(List<O> origin);
        }
    
}