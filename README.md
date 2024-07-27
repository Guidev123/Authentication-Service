   <h1>Projeto de Autenticação e Autorização Baseada em JWT</h1>
    <p>Este é um projeto de autenticação e autorização baseada em JWT, porém com uma alternativa mais personalizável sem utilizar as tabelas já feitas do Identity, e sim tabelas personalizadas.</p>

<h2>Arquitetura</h2>

![Screenshot_18](https://github.com/user-attachments/assets/4a14fabd-74c3-45b0-bce6-b89853e3184b)


<h2>Endpoints</h2>
    <ul>
        <li><strong>POST /api/create-user:</strong> Cria um usuário e gera um código de autenticação via email</li>
        <li><strong>POST /api/authenticate-user:</strong> Autentica o usuário e gera um JWT já carregado com as Roles</li>
    </ul>
    
![Screenshot_17](https://github.com/user-attachments/assets/ba9e5abf-300e-45ab-ba97-d40c9d40727b)

   
   <h2>Tecnologias</h2>
    <ul>
        <li>SQL Server</li>
        <li>Entity Framework</li>
        <li>.NET</li>
        <li>ASP.NET Minimal API's</li>
    </ul>
    
<h2>Pacotes</h2>
   <ul>
        <li>MediatR</li>
        <li>FluentValidation</li>
    </ul>

<h2>Princípios</h2>
    <ul>
        <li>Domain Driven Design</li>
        <li>SOLID</li>
        <li>Clean Code</li>
    </ul>

