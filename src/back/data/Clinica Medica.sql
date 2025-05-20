CREATE DATABASE IF NOT EXISTS clinicaMedica;
USE clinicaMedica;

-- Tabela Base: Pessoa 
CREATE TABLE Pessoa(
	Id INT AUTO_INCREMENT PRIMARY KEY,
    nomeCompleto VARCHAR(100) NOT NULL,
    sexo VARCHAR(15) NOT NULL,
    dataNascimento DATE NOT NULL,
    telefone VARCHAR(15) NOT NULL,
    email VARCHAR(50) NOT NULL,
    cpf VARCHAR(14) NOT NULL
)default charset utf8mb4;

-- Tabela Médico (Herança de Pessoa)
CREATE TABLE Medico(
	Id INT PRIMARY KEY,
    crm INT NOT NULL UNIQUE,
    especialidade VARCHAR(25) NOT NULL,
    foreign key (Id) REFERENCES Pessoa(Id) ON DELETE CASCADE 
)default charset utf8mb4;

-- Tabela Paciente (Também herda de pessoa)
CREATE TABLE Paciente(
	Id INT PRIMARY KEY,
    numeroCarteirinha INT NOT NULL UNIQUE,
    foreign key (Id) REFERENCES Pessoa(Id) ON DELETE CASCADE
)default charset utf8mb4;

-- Tabela Status consulta (vai possuir dados de status: cancelada, confirmada, marcada, etc)
-- TABELA BASE PARA AS CONSULTAS E REFERÊNCIAS DA TABELA CONSULTA
CREATE TABLE statusConsulta(
	numeroProtocolo INT PRIMARY KEY,
    statusConsulta varchar(15)
)default charset utf8mb4;
-- Tabela Consulta, utiliza dados das tabelas médico/paciente e status consulta
CREATE TABLE Consulta(
	Id INT AUTO_INCREMENT PRIMARY KEY,
    medicoId INT NOT NULL,
    crm INT NOT NULL UNIQUE,
    pacienteId INT NOT NULL,
    numeroCarteirinha INT NOT NULL UNIQUE,
    dataConsulta DATE NOT NULL,
    statusConsulta INT NOT NULL,
    descricao VARCHAR(100),
    foreign key (statusConsulta) REFERENCES statusConsulta(numeroProtocolo),
    foreign key (medicoId) REFERENCES Medico(Id),
    foreign key (pacienteId) REFERENCES Paciente(Id),
    foreign key (crm) REFERENCES Medico(crm),
    foreign key (numeroCarteirinha) REFERENCES Paciente(numeroCarteirinha)
)default charset utf8mb4;

INSERT INTO statusConsulta (numeroProtocolo, statusConsulta) VALUES 
(1, "Agendada"),
(2, "Em Aprovação"),
(3, "Confirmada"),
(4, "Concluída"),
(5, "Cancelada");

SELECT * FROM statusConsulta;
