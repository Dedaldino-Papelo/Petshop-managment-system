create database dbPetshop

use dbPetshop



create table tbFuncionario(
id_Func int identity(1,1) primary key,
Nome varchar(50),
Morada varchar(100),
Telefone varchar(13),
Senha varchar(8)
)

alter table tbFuncionario
add Data_nascimento date null

create table tbCliente(
id_Cliente int identity(1,1) primary key,
Nome varchar(50),
Sobrenome varchar(30),
Telefone varchar(13),
)

create table tbCategoria(
id_Cat int identity(1,1) primary key,
Nome varchar(50),
)

create table tbProduto(
id_Prod int identity(1,1) primary key,
Nome varchar(40),
Quantidade int,
Preco decimal(10,2),
id_Cat int
)

alter table tbProduto
ALTER COLUMN Preco int not null;

create table tbVenda(
id_Venda int identity(1,1) primary key,
Data_venda date,
id_Cliente int,
id_Func int,
Total decimal(10,2)
)


alter table tbVenda
add  NomeFunc varchar(40)

alter table tbVenda
alter column Total int not null

create table tbVenda_Produto(
id_VendProd int identity(1,1) primary key,
id_Venda int,
id_Produto int
)

select p.Nome, p.Preco, p.Quantidade, p.id_Cat, c.Nome
from tbProduto p
inner join tbCategoria c on p.id_Cat = c.id_Cat


alter table tbProduto
ADD Constraint FK_CatProd FOREIGN KEY (id_Cat) REFERENCES tbCategoria(id_Cat);

alter table tbVenda
ADD Constraint FK_VendaCli FOREIGN KEY (id_Cliente) REFERENCES tbCliente(id_Cliente);

alter table tbVenda
ADD Constraint FK_VendaFunc FOREIGN KEY (idFunc) REFERENCES tbFuncionario(id_Func);




alter table tbVenda_Produto
ADD Constraint FK_Venda FOREIGN KEY (id_Venda) REFERENCES tbVenda(id_Venda);

alter table tbVenda_Produto
ADD Constraint FK_Prod FOREIGN KEY (id_Produto) REFERENCES tbProduto(id_Prod);


--consultar categoria --
Create procedure spconsultar_Funcionario
as
select*
from tbFuncionario
go
--Inserir Categoria--
create procedure spInserir_Funcionario(
@Nome varchar(50),
@Morada varchar(100),
@Telefone varchar(13),
@Senha varchar(8),
@Data_nascimento Date
)
as
insert into tbFuncionario values(@Nome,@Morada,@Telefone,@Senha,@Data_nascimento)
go
--Actualizar Categoria--
create procedure spActualizar_Funcionario(
@id_Func int,
@Nome varchar(50),
@Morada varchar(100),
@Telefone varchar(13),
@Senha varchar(8),
@Data_nascimento date
)
as
update tbFuncionario
set
Nome=@Nome,
Morada = @Morada,
Telefone=@Telefone,
Senha=@Senha,
Data_nascimento=@Data_nascimento
where id_Func=@id_Func
go

--Excluir Categoria--
create procedure spExcluir_Funcionario(
@id_Func int
)
as
begin
delete from tbFuncionario where id_Func = @id_Func
end
go


--consultar Cliente --
Create procedure spconsultar_Cliente
as
select*
from tbCliente
go

--Inserir Categoria--
create procedure spInserir_Cliente(
@Nome varchar(50),
@Sobrenome varchar(30),
@Telefone varchar(13)
)
as
insert into tbCliente values(@Nome,@Sobrenome,@Telefone)
go

--Actualizar Categoria--
create procedure spActualizar_Cliente(
@id_Cliente int,
@Nome varchar(50),
@Sobrenome varchar(30),
@Telefone varchar(13)
)
as
update tbCliente
set
Nome=@Nome,
Sobrenome=@Sobrenome,
Telefone=@Telefone
where id_Cliente=@id_Cliente
go


--Excluir Categoria--
create procedure spExcluir_Cliente(
@id_Cliente int
)
as
begin
delete from tbCliente where id_Cliente = @id_Cliente
end
go

--consultar Categoria --
Create procedure spconsultar_Categoria
as
select*
from tbCategoria
go

--Inserir Categoria--
create procedure spInserir_Categoria(
@Nome varchar(50)
)
as
insert into tbCategoria values(@Nome)
go

--Actualizar Categoria--
create procedure spActualizar_Categoria(
@id_Cat int,
@Nome varchar(50)
)
as
update tbCategoria
set
Nome=@Nome
where id_Cat=@id_Cat
go


--Excluir Categoria--
create procedure spExcluir_Categoria(
@id_Cat int
)
as
begin
delete from tbCategoria where id_Cat = @id_Cat
end
go

--consultar Categoria --
Create procedure spconsultar_Produto
as
select p.id_Prod, p.Nome, p.Quantidade, p.Preco, c.nome as Categoria
from tbProduto p
inner join tbCategoria c on p.id_Cat = c.id_Cat
go


--Inserir Categoria--
create procedure spInserir_Produto(
@Nome varchar(40),
@Quantidade int,
@Preco decimal(10,2),
@id_Cat int
)
as
insert into tbProduto values(@Nome,@Quantidade,@Preco,@id_Cat)
go

--Actualizar Produto--
create procedure spActualizar_Produto(
@id_Prod int,
@Nome varchar(40),
@Quantidade int,
@Preco decimal(10,2),
@id_Cat int
)
as
update tbProduto
set
Nome=@Nome,
Quantidade=@Quantidade,
Preco=@Preco,
id_Cat=@id_Cat
where id_Prod=@id_Prod
go

--Excluir Produto--
create procedure spExcluir_Produto(
@id_Prod int
)
as
begin
delete from tbProduto where id_Prod = @id_Prod
end
go

--consultar Categoria --
Create procedure spconsultar_Venda
as
select v.id_Venda, v.Data_venda, c.Nome, v.Total, v.NomeFunc
from tbVenda v
inner join tbCliente c on v.id_Cliente = c.id_Cliente 
go

--Inserir Categoria--
create procedure spInserir_Venda(
@Data_venda date,
@id_Cliente int,
@Total int,
@NomeFunc varchar(40)
)
as
insert into tbVenda values(@Data_venda,@id_Cliente,@Total,@NomeFunc)
go


--Actualizar Categoria--
create procedure spActualizar_Venda(
@id_Venda int,
@Data_venda date,
@id_Cliente int,
@Total int,
@NomeFunc varchar(40)
)
as
update tbVenda
set
Data_venda = @Data_venda,
id_Cliente = @id_Cliente,
Total = @Total,
NomeFunc = @NomeFunc
where id_Venda=@id_Venda
go

--Excluir Produto--
create procedure spExcluir_Venda(
@id_Venda int
)
as
begin
delete from tbVenda where id_Venda = @id_Venda
end
go




