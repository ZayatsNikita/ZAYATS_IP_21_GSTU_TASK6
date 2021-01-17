use UniversityDatabase
insert into Exam Values
('MMA','Exam'),
('Mechanics','Test')

insert into GroupOfStudent
values
('IP-21'),
('ML-42')

insert into Student
Values
('Zayats Nikita Sergeevich','M','2001-10-01',(select id from GroupOfStudent where NameOfGroup='IP-21')),
('Pishuck Alex Igoreevna','F','2002-10-01',(select id from GroupOfStudent Where NameOfGroup = 'IP-21')),
('Tsmyg Dmitry Alexandrovich','M','1998-09-13',(select id from GroupOFStudent where NameOfGroup = 'ML-42'))

insert into ExamInfo
values 
((select id from Exam where NameOfExam = 'MMA'),'2020-06-25',2),
((select id from Exam where NameOfExam = 'MMA'),'2020-01-08',1),
((select id from Exam where NameOfExam = 'MMA'),'2021-01-12',3),
((select id from Exam where NameOfExam = 'Mechanics'),'2018-02-02',4),
((select id from Exam where NameOfExam = 'Mechanics'),'2019-01-01',5)

insert into UniversitySessionInfo 
values
((select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-06-25')),( select id from GroupOfStudent where NameOfGroup='IP-21')),
((select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-01-08')),( select id from GroupOfStudent where NameOfGroup='IP-21')),
((select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2021-01-12')),( select id from GroupOfStudent where NameOfGroup='IP-21')),
((select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'Mechanics' and DatOfTheExam = '2018-02-02')),( select id from GroupOfStudent where NameOfGroup='ML-42')),
((select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'Mechanics' and DatOfTheExam = '2019-01-01')),( select id from GroupOfStudent where NameOfGroup='ML-42'))



insert into StudentSessionIfno
values
((select id from Student where FIO ='Zayats Nikita Sergeevich'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-01-08')),10),
((select id from Student where FIO ='Zayats Nikita Sergeevich'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-06-25')),9),
((select id from Student where FIO ='Zayats Nikita Sergeevich'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2021-01-12')),8),
((select id from Student where FIO ='Pishuck Alex Igoreevna'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-01-08')),9),
((select id from Student where FIO ='Pishuck Alex Igoreevna'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2020-06-25')),8),
((select id from Student where FIO ='Pishuck Alex Igoreevna'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'MMA' and DatOfTheExam = '2021-01-12')),8),
((select id from Student where FIO ='Tsmyg Dmitry Alexandrovich'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'Mechanics' and DatOfTheExam = '2018-02-02')),7),
((select id from Student where FIO ='Tsmyg Dmitry Alexandrovich'), (select id from ExamInfo where ExamId = (select id from Exam where NameOfExam = 'Mechanics' and DatOfTheExam = '2019-01-01')),3)

