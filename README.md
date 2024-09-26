# Padrão de Repositório Genérico (Generic Repository)

## O que é?

O **Padrão de Repositório Genérico** é uma abordagem que abstrai as operações comuns de acesso a dados, como criar, ler, atualizar e deletar (CRUD), em uma classe genérica. Isso permite que o código seja dinâmico e reutilizável para diferentes tipos de entidades sem precisar duplicar lógica para cada uma.

## Como funciona?

Em vez de criar repositórios separados para cada entidade (como `PersonRepository`, `ProductRepository`), o **Generic Repository** define operações de CRUD que podem ser aplicadas a qualquer entidade do sistema, seguindo algumas regras, como herdar de uma classe base ou ter um identificador único.

### Exemplo:

Um repositório genérico poderia ser assim:

```csharp
public interface IRepository<T> where T : class
{
    T Create(T entity);
    T Update(T entity);
    void Delete(long id);
    T FindById(long id);
    List<T> FindAll();
}
