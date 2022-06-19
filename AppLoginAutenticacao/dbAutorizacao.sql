drop database dbautorizacao;

create database dbAutorizacao;

use dbAutorizacao;

CREATE TABLE tbUsuario (
    UsuarioID INT PRIMARY KEY AUTO_INCREMENT,
    UsuNome VARCHAR(100) NOT NULL UNIQUE,
    Login VARCHAR(50) NOT NULL UNIQUE,
    Senha VARCHAR(100) NOT NULL
);

-- método InsertUsuario
delimiter $$
create procedure InsertUsuario(vUsuNome varchar(100),vLogin varchar(150),vSenha varchar(100))
begin
insert into tbUsuario(UsuNome,Login,Senha) 
	        values(vUsuNome,vLogin,vSenha);
end $$

call InsertUsuario('enildo','churrasco','carne');

select * from tbUsuario;
truncate tbUsuario;

-- método select login
delimiter $$
create procedure SelectLogin(vLogin varchar(150))
begin
select Login from tbUsuario where Login = Login;   
end $$

-- método select usuario
delimiter $$
create procedure SelectUsuario(vLogin varchar(150))
begin
select * from tbUsuario where Login = Login;   
end $$

-- método update usuario
delimiter $$
create procedure UpdateUsuario(vLogin varchar(150),vSenha varchar(100))
begin
Update tbUsuario set Senha = vSenha 
                 where Login = vLogin;
end $$

delimiter $$
create procedure spUpdateSenha(vLogin varchar(150),vSenha varchar(100))
begin
Update tbUsuario set Senha = vSenha;
end $$

delimiter $$
create procedure spDeleteUsuario(vLogin varchar(150),vSenha varchar(100))
begin
Update tbUsuario set Senha = vSenha
				where Login = vLogin;
end $$

call InsertUsuario('lobao','porquinho','12345678');
call SelectLogin('porquinho');
call SelectUsuario('porquinho');


delimiter $$
create procedure spInsertUsuario(vUsuNome varchar(100),vLogin varchar(150),vSenha varchar(100))
begin
insert into tbUsuario(UsuNome,Login,Senha) 
	        values(vUsuNome,vLogin,vSenha);
end $$

call spInsertUsuario('Astrgildo','porquinho','12345678');
select * from tbUsuario;

delimiter $$
create procedure spSelectLogin(vLogin varchar(150))
begin
select Login from tbUsuario where Login = vLogin;   
end $$


delimiter $$
create procedure spSelectUsuario(vLogin varchar(150))
begin
select * from tbUsuario where Login = vLogin;   
end $$

call spUpdateSenha('porquinho0','87654321');

select * from tbUsuario;

call SelectLogin ('Cartomante');

truncate tbUsuario;
