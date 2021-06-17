
-- Dados de Teste --

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