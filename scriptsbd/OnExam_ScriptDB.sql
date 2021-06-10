create table Users(
UserID int identity not null constraint Pk_Users_UserID primary key,
Name varchar(100) not null,
Email varchar(100) not null,
Username varchar(50) not null,
Password varchar(100) not null,
Salt varchar(50) not null
);

create table Exams(
ExamID int identity not null constraint Pk_Exams_ExamID primary key,
UserID int not null constraint Fk_Exams_Users references Users (UserID),
Name varchar(20) null,
Duration int not null,
isRandom bit not null,
State int not null
);

create table Questions(
QuestionID int identity not null constraint Pk_Questions_QuestionID primary key,
ExamID int not null constraint Fk_Questions_Exams references Exams (ExamID),
Type int not null,
Question varchar(1000) not null,
Notes varchar(500) not null
);

create table QuestionDetails(
QuestionDetailsID int identity not null constraint Pk_QuestionDetails_QuestionDetailsID primary key,
QuestionID int not null constraint Fk_QuestionDetails_Questions references Questions (QuestionID),
isRight bit not null,
Text varchar(500) not null
);

create table Sessions(
SessionID int identity not null constraint Pk_Sessions_SessionID primary key,
ExamID int not null constraint Fk_Sessions_Exams references Exams (ExamID),
Name varchar(100) not null,
Info varchar(500) not null
);

create table Answers(
AnswerID int identity not null constraint Pk_Answers_AnswerID primary key,
SessionID int not null constraint Fk_Answers_Sessions references Sessions (SessionID),
QuestionID int not null constraint Fk_Answers_Questions references Questions (QuestionID),
QuestionDetailsID int constraint Fk_Answers_QuestionDetails references QuestionDetails (QuestionDetailsID),
Text varchar(500)
);