
-- Script: Tables --

create table Users(
UserID int identity not null constraint Pk_Users_UserID primary key,
Name varchar(100) not null,
Email varchar(100) not null,
Username varchar(50) not null constraint Uk_Users_Username unique,
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
Info varchar(500) not null,
SessionExits int default 0
);

create table Answers(
AnswerID int identity not null constraint Pk_Answers_AnswerID primary key,
SessionID int not null constraint Fk_Answers_Sessions references Sessions (SessionID),
QuestionID int not null constraint Fk_Answers_Questions references Questions (QuestionID),
QuestionDetailsID int constraint Fk_Answers_QuestionDetails references QuestionDetails (QuestionDetailsID),
Text varchar(500)
);
go

-- Script: Stored Procedures --

-- UserManagement --
create procedure UserSearch
@Name varchar(100) as
begin
select Username, Password, Salt
from Users
where Username = @Name or Email = @Name;
end
go

create procedure UserEmailSearch
@Email varchar(100),
@Username varchar(100) as
begin
select Email
from Users
where Email = @Email and Username not like @Username;
end
go

create procedure UserAdd
@Name varchar(100),
@Email varchar(100),
@Username varchar(50),
@Password varchar(100),
@Salt varchar(50) as
begin
insert into Users
values (@Name, @Email, @Username, @Password, @Salt);
end
go

create procedure UserOpen
@Username varchar(50) as
begin
select Name, Email
from Users
where Username = @Username;
end
go

create procedure UserUpdate
@Name varchar(100),
@Email varchar(100),
@UsernameOld varchar(50),
@UsernameNew varchar(50) as
begin
update Users set
Name = @Name,
Email = @Email,
Username = @UsernameNew
where Username = @UsernameOld;
end
go

create procedure UserUpdatePass
@Username varchar(50),
@Password varchar(100),
@Salt varchar(50) as
begin
update Users set
Password = @Password,
Salt = @Salt
where Username = @Username;
end
go

-- ExamManagement --
create procedure ExamsView
@Username varchar(50) as
begin
select ExamID, concat(Username, '-', isnull(e.Name, ExamID)) as ExamName, Duration, isRandom, State
from Exams e join Users u
on u.UserID = e.UserID
where Username = @Username;
end
go

create procedure ExamNameSearch
@ExamID int,
@ExamName varchar(20),
@Username varchar(50) as
begin
select ExamID
from Exams e join Users u
on u.UserID = e.UserID
where ExamID != @ExamID and e.Name = @ExamName and Username = @Username
end
go

create procedure ExamAdd
@Username varchar(50),
@Duration int,
@isRandom bit,
@State int as
begin
insert into Exams (UserID, Duration, isRandom, State)
values ((select UserID from Users where Username=@Username), @Duration, @isRandom, @State);
select scope_identity();
end
go

create procedure ExamOpen
@Username varchar(50),
@ExamID int as
begin
select isnull(e.Name, ExamID) as ExamName, Duration, isRandom, State
from Exams e join Users u
on u.UserID = e.UserID
where Username = @Username and ExamID = @ExamID;
end
go

create procedure ExamUpdate
@ExamID int,
@ExamName varchar(20),
@Duration int,
@isRandom bit as
begin
update Exams set
Name = @ExamName,
Duration = @Duration,
isRandom = @isRandom
where ExamID = @ExamID;
end
go

create procedure ExamUpdateState
@ExamID int,
@State int as
begin
update Exams set
State = @State
where ExamID = @ExamID;
end
go

create procedure ExamDelete
@ExamID int as
begin
delete from QuestionDetails
where QuestionID in (select QuestionID
from Questions where ExamID = @ExamID);
delete from Questions
where ExamID = @ExamID;
delete from Exams
where ExamID = @ExamID;
end
go

create procedure QuestionAdd
@ExamID int,
@Type int,
@Question varchar(1000),
@Notes varchar(500) as
begin
insert into Questions (ExamID, Type, Question, Notes)
values (@ExamID, @Type, @Question, @Notes);
select scope_identity();
end
go

create procedure QuestionsOpen
@ExamID int as
begin
select QuestionID, Type, Question, Notes
from Questions
where ExamID = @ExamID;
end
go

create procedure QuestionsOpenRandom
@ExamID int as
begin
select QuestionID, Type, Question
from Questions
where ExamID = @ExamID
order by newID();
end
go

create procedure QuestionUpdate
@QuestionID int,
@Question varchar(1000),
@Notes varchar(500) as
begin
update Questions set
Question = @Question,
Notes = @Notes
where QuestionID = @QuestionID;
end
go

create procedure QuestionDelete
@QuestionID int as
begin
delete from QuestionDetails
where QuestionID = @QuestionID;
delete from Questions
where QuestionID = @QuestionID;
end
go

create procedure QuestionDetailsAdd
@QuestionID int,
@isRight bit,
@Text varchar(500) as
begin
insert into QuestionDetails (QuestionID, isRight, Text)
values (@QuestionID, @isRight, @Text);
end
go

create procedure QuestionDetailsOpen
@QuestionID int as
begin
select QuestionDetailsID, isRight, Text
from QuestionDetails
where QuestionID = @QuestionID;
end
go

create procedure QuestionDetailsUpdate
@QuestionDetailsID int,
@isRight bit,
@Text varchar(500) as
begin
update QuestionDetails set
isRight = @isRight,
Text = @Text
where QuestionDetailsID = @QuestionDetailsID;
end
go

create procedure QuestionDetailsDelete
@QuestionDetailsID int as
begin
delete from QuestionDetails
where QuestionDetailsID = @QuestionDetailsID;
end
go

-- SessionManagement --
create procedure ExamSearch
@Username varchar(50),
@ExamID int,
@ExamName varchar(20) as
begin
select ExamID, isnull(e.Name, ExamID) as ExamName, Duration, isRandom, State, Username
from Exams e join Users u
on u.UserID = e.UserID
where (ExamID = @ExamID or e.Name = @ExamName) and Username like '%';
end
go

create procedure SessionsView
@ExamID int as
begin
select SessionID, Name, Info, SessionExits
from Sessions
where ExamID = @ExamID;
end
go

create procedure SessionAdd
@ExamID int,
@Name varchar(100),
@Info varchar(500) as
begin
insert into Sessions (ExamID, Name, Info)
values (@ExamID, @Name, @Info);
select scope_identity();
end
go

create procedure SessionUpdate
@SessionID int,
@SessionExits int as
begin
update Sessions set
SessionExits = @SessionExits
where SessionID = @SessionID;
end
go

create procedure AnswerAdd
@SessionID int,
@QuestionID int,
@QuestionDetailsID int = null,
@Text varchar(500) = null as
begin
insert into Answers (SessionID, QuestionID, QuestionDetailsID, Text)
values (@SessionID, @QuestionID, @QuestionDetailsID, @Text);
end
go

create procedure SessionOpen
@SessionID int as
begin
select count(AnswerID)
from Answers
where SessionID = @SessionID;
end
go

create procedure SessionQuestionsOpen
@SessionID int as
begin
select QuestionID, Type, Question
from Questions q join Sessions s
on q.ExamID = s.ExamID
where SessionID = @SessionID;
end
go

create procedure SessionAnswersOpen
@SessionID int,
@QuestionID int as
begin
select QuestionDetailsID, Text
from Answers
where SessionID = @SessionID and QuestionID = @QuestionID;
end
go