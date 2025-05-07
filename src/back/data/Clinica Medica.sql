create database clinicamedica;

use clinicamedica;

create table if not exists Medico(
nomeCompleto varchar(100) not null,
sexo varchar(15) not null,
dataNascimento date not null,
telefone varchar(15) not null,
email varchar(50) not null,
crm varchar(25) not null primary key,
especialidade varchar(25) not null
)default charset utf8mb4;

create table if not exists Paciente(
nomeCompleto varchar(100) not null,
sexo varchar(15) not null,
dataNascimento date not null,
telefone varchar(15) not null,
email varchar(50) not null,
cpf varchar(11) not null,
numeroCarteirinha int not null primary key
)default charset utf8mb4;

create table if not exists statusConsulta(
numeroProtocolo int not null primary key,
statusConsulta varchar(15)
)default charset utf8mb4;

create table if not exists Consulta(
crm varchar(25) not null,
numeroCarteirinha int not null,
dataConsulta date not null,
statusConsulta int not null,
foreign key (statusConsulta) references statusConsulta (numeroProtocolo),
foreign key (crm) references Medico (crm),
foreign key (numeroCarteirinha) references Paciente (numeroCarteirinha),
primary key (crm,numeroCarteirinha)
)default charset utf8mb4;

INSERT INTO statusConsulta (numeroProtocolo,statusConsulta) VALUES (1,"Agendada");
INSERT INTO statusConsulta (numeroProtocolo,statusConsulta) VALUES (2,"Confirmada");
INSERT INTO statusConsulta (numeroProtocolo,statusConsulta) VALUES (3,"Em Aprovação");
INSERT INTO statusConsulta (numeroProtocolo,statusConsulta) VALUES (4,"Concluída");
INSERT INTO statusConsulta (numeroProtocolo,statusConsulta) VALUES (5,"Cancelada");