-- criar DB
create database OnExamDB;

-- criar login
create login userOnExamDB with password = 'OnExamM16', default_database = OnExamDB;

-- criar user
use OnExamDB;
create user userOnExamDB for login userOnExamDB;

-- criar tables
create table Users(
UserID int identity(1,1) not null,
Nome varchar(100) not null,
Email varchar(100) not null,
Username varchar(50) not null,
[Password] varchar(50) not null,
PRIMARY KEY (UserID)
);

create table Exames(
ExameID int identity(1,1) not null,
UserID int not null foreign key references Users(UserID),
Duracao int not null,
IsRandom bit not null,
primary key (ExameID)
);

create table Perguntas(
PerguntaID int identity(1,1) not null,
ExameID int not null foreign key references Exames(ExameID),
Tipo int not null,
Enunciado varchar(1000) not null,
Notas varchar(500) not null,
primary key (PerguntaID)
);

create table DetalhesPergunta(
DetalhesPerguntaID int identity(1,1) not null,
PerguntaID int not null foreign key references Perguntas(PerguntaID),
isRight bit default null,
Espaco1 varchar(500) not null,
Espaco2 varchar(500) default null,
primary key (DetalhesPerguntaID)
);

create table Avaliados(
AvaliadoID int identity(1,1) not null,
ExameID int not null foreign key references Exames(ExameID),
Nome varchar(100) not null,
Info varchar(500) not null,
primary key (AvaliadoID)
);

create table Respostas(
RespostaID int identity(1,1) not null,
AvaliadoID int not null foreign key references Avaliados(AvaliadoID),
PerguntaID int not null foreign key references Perguntas(PerguntaID),
DetalhesPerguntaID int not null foreign key references DetalhesPergunta(DetalhesPerguntaID),
OpcEscolhida bit default null,
Espaco varchar(500) default null,
primary key (RespostaID)
);