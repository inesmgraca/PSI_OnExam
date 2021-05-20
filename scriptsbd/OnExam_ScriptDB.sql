create table Users(
UserID int identity(1,1) not null,
Name varchar(100) not null,
Email varchar(100) not null,
Username varchar(50) not null,
Password varchar(100) not null,
Salt varchar(50) not null,
constraint Pk_Users_UserID primary key (UserID)
);

create table Exams(
ExamID int identity(1,1) not null,
UserID int not null,
Name varchar(20) null,
Duration int not null,
isRandom bit not null,
constraint Pk_Exams_ExamID primary key (ExamID),
constraint Fk_Exams_Users foreign key (UserID) references Users (UserID)
);

create table Perguntas(
PerguntaID int identity(1,1) not null,
ExamID int not null,
Tipo int not null,
Enunciado varchar(1000) not null,
Notas varchar(500) not null,
constraint Pk_Perguntas_PerguntaID primary key (PerguntaID),
constraint Fk_Perguntas_Exams foreign key (ExamID) references Exams (ExamID)
);

create table DetalhesPergunta(
DetalhesPerguntaID int identity(1,1) not null,
PerguntaID int not null,
isRight bit default null,
Espaco1 varchar(500) not null,
Espaco2 varchar(500) default null,
constraint Pk_DetalhesPergunta_DetalhesPerguntaID primary key (DetalhesPerguntaID),
constraint Fk_DetalhesPergunta_Perguntas foreign key (PerguntaID) references Perguntas (PerguntaID)
);

create table Avaliados(
AvaliadoID int identity(1,1) not null,
ExamID int not null,
Nome varchar(100) not null,
Info varchar(500) not null,
constraint Pk_Avaliados_AvaliadoID primary key (AvaliadoID),
constraint Fk_Avaliados_Exams foreign key (ExamID) references Exams (ExamID)
);

create table Respostas(
RespostaID int identity(1,1) not null,
AvaliadoID int not null,
PerguntaID int not null,
DetalhesPerguntaID int not null,
OpcEscolhida bit default null,
Espaco varchar(500) default null,
constraint Pk_Respostas_RespostaID primary key (RespostaID),
constraint Fk_Respostas_Avaliados foreign key (AvaliadoID) references Avaliados (AvaliadoID),
constraint Fk_Respostas_Perguntas foreign key (PerguntaID) references Perguntas (PerguntaID),
constraint Fk_Respostas_DetalhesPergunta foreign key (DetalhesPerguntaID) references DetalhesPergunta (DetalhesPerguntaID)
);