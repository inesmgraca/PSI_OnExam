
-- Dados de teste --

exec UserAdd 'Professor', 'prof@escola.com', 'prof',
'XYmhTil1K4tgb7yltFsmYIhMycZP9R2e1mKDVX8G32w=', 'aL69rDTOwSKtVPj2AnMwxQ=='; -- prof2021

/* 1 */ exec ExamAdd 'prof', '15', 0, 2;
/* 2 */ exec ExamAdd 'prof', '10', 1, 0;

exec ExamUpdate 1, 'testePSI', 15, 0;

/* 1 */ exec QuestionAdd 1, 0, 'O que quer dizer PSI?', '';
/* 2 */ exec QuestionAdd 1, 1, 'Existe uma prova no fim do curso?', '';
/* 3 */ exec QuestionAdd 1, 2, 'Plataformas utilizadas no projeto:', '';

/* 1 */ exec QuestionDetailsAdd 2, 1, 'Verdadeiro';
/* 2 */ exec QuestionDetailsAdd 2, 0, 'Falso';
/* 3 */ exec QuestionDetailsAdd 3, 0, 'CodeBlocks';
/* 4 */ exec QuestionDetailsAdd 3, 1, 'GitHub';
/* 5 */ exec QuestionDetailsAdd 3, 1, 'SQL Server';
/* 6 */ exec QuestionDetailsAdd 3, 1, 'Visual Studio';

/* 1 */ exec SessionAdd 1, 'Aluno de 20', 'Programação';
/* 2 */ exec SessionAdd 1, 'Aluno de 14', 'Multimédia';

exec SessionUpdate 2, 1;
exec AnswerAdd 1, 1, null, 'Programação de sistemas informáticos';
exec AnswerAdd 2, 1, null, 'Psicologia';
exec AnswerAdd 1, 2, 1, null;
exec AnswerAdd 2, 2, 1, null;
exec AnswerAdd 1, 3, 4, null;
exec AnswerAdd 1, 3, 5, null;
exec AnswerAdd 1, 3, 6, null;
exec AnswerAdd 2, 3, 3, null;
exec AnswerAdd 2, 3, 4, null;

-- Procedures necessários para funcionar corretamente --

/*
create procedure UserAdd
@Name varchar(100), @Email varchar(100), @Username varchar(50),
@Password varchar(100), @Salt varchar(50) as
begin
insert into Users values (@Name, @Email, @Username, @Password, @Salt);
end

create procedure ExamAdd
@Username varchar(50), @Duration int, @isRandom bit, @State int as
begin
insert into Exams (UserID, Duration, isRandom, State)
values ((select UserID from Users where Username=@Username), @Duration, @isRandom, @State);
select max(ExamID) as ExamID from Exams e join Users u
on u.UserID = e.UserID where Username = @Username;
end

create procedure ExamUpdate
@ExamID int, @ExamName varchar(20), @Duration int, @isRandom bit as
begin
update Exams set Name = @ExamName, Duration = @Duration, isRandom = @isRandom
where ExamID = @ExamID;
end

create procedure QuestionAdd
@ExamID int, @Type int, @Question varchar(1000), @Notes varchar(500) as
begin
insert into Questions (ExamID, Type, Question, Notes)
values (@ExamID, @Type, @Question, @Notes);
select max(QuestionID) as QuestionID
from Questions where ExamID = @ExamID;
end

create procedure QuestionDetailsAdd
@QuestionID int, @isRight bit, @Text varchar(500) as
begin
insert into QuestionDetails (QuestionID, isRight, Text)
values (@QuestionID, @isRight, @Text);
end

create procedure SessionAdd
@ExamID int, @Name varchar(100), @Info varchar(500) as
begin
insert into Sessions (ExamID, Name, Info) values (@ExamID, @Name, @Info);
select max(SessionID) as SessionID from Sessions
where ExamID = @ExamID and Name = @Name and Info = @Info;
end

create procedure SessionUpdate
@SessionID int, @SessionExits int as
begin
update Sessions set SessionExits = @SessionExits
where SessionID = @SessionID;
end

create procedure AnswerAdd
@SessionID int, @QuestionID int, @QuestionDetailsID int = null, @Text varchar(500) = null as
begin
insert into Answers (SessionID, QuestionID, QuestionDetailsID, Text)
values (@SessionID, @QuestionID, @QuestionDetailsID, @Text);
end
*/