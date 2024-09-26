# Value Object (VO) Design Pattern

## O que é um VO?

O **Value Object (VO)** é um padrão de projeto que encapsula dados, sendo definido pelos valores de seus atributos, e não por sua identidade. No contexto de uma aplicação, ele pode ser usado para transferir dados de forma clara e segura, servindo como uma interface entre a lógica de negócios e outras camadas da aplicação.

## Como funciona?

O VO é imutável, ou seja, uma vez criado, seus valores não mudam. Ele também não tem uma identidade própria, sendo comparado apenas pelos seus atributos. Ele pode ser utilizado tanto para exibir informações ao usuário quanto para validações e outras lógicas de negócios dentro da aplicação.

### Exemplo:

Em uma aplicação web, um VO pode ser usado para representar a resposta de uma API, contendo informações como nome, endereço e telefone, mas ele também pode ser usado para transferir dados entre diferentes camadas sem expor diretamente as entidades do banco de dados.

---

