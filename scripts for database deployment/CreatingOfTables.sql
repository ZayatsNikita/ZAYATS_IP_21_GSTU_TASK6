use UniversityDatabase
create table Exam
(
	Id int identity,
	NameOfExam nvarchar(30) not null,
	TypeOfExam nvarchar(4) not null

	CONSTRAINT CK_TypeOfExam Check(TypeOfExam = 'Test' OR TypeOfExam='Exam'),
	CONSTraint CK_LengthOfNameOfExam check(NameOfExam <> ''),
	constraint qu_UniqueexamName Unique(NameOfExam),
	constraint PR_ExamPk Primary key(Id)
)


create table GroupOfStudent
(
	Id int identity,
	NameOfGroup nvarchar(30) not null

	constraint Pk_GroupOfStudentPk Primary key(Id),
	CONSTraint CK_LengthOfNameOfGruop check(NameOfGroup <> ''),
	constraint UQ_NameOfGroupUQ unique(NameOfGroup)
)

create table Student
(

	Id int Identity,
	FIO nvarchar(50) not null,
	Sex nvarchar(5) not null,
	BirthDay date not null,
	GroupId int not null

	constraint PK_StudentPk Primary key(Id),
	constraint CH_FIOCk Check(LEN(FIO)>5),
	constraint FK_StudentFk Foreign key(GroupId) references GroupOfStudent(Id) on delete cascade on update cascade
)


create table ExamInfo
(
	Id int identity,
	ExamId int not null,
	DatOfTheExam date not null,
	NumberSession int not null,

	constraint PK_ExamInfoPk Primary key(Id),
	constraint UQ_ExamInfoQu Unique(ExamId,DatOfTheExam),
	constraint CK_NumberSessionCk Check(NumberSession>0 and NumberSession<21),
	constraint CK_DatOfTheExamCk check(DatOfTheExam between '1900-01-01' and '2100-01-01'),
	constraint FK_ExamDateFk foreign key(ExamId) references Exam(Id) on delete cascade on update cascade
)

create table UniversitySessionInfo
(
	id int identity,
	ExamInfoId int not null,
	GroupId int not null

	constraint PK_UniversitySessionInfoPk Primary key(Id),
	constraint UQ_UniversitySessionInfoQu Unique(ExamInfoId,GroupId),
	constraint FK_ExamDateFkEXAM foreign key(ExamInfoId) references ExamInfo(Id) on delete cascade on update cascade,
	constraint FK_ExamDateFkGROUP foreign key(GroupId) references GroupOfStudent(Id) on delete cascade on update cascade

)

create table StudentSessionIfno
(
	id int identity,
	StudentId int not null,
	ExamInfoId int not null,
	Mark int not null

	constraint PK_StudentSessionIfnoPk Primary key(id),
	constraint FK_StudentSessionIfnoFkEXAM foreign key(ExamInfoId) references ExamInfo(Id) on delete cascade on update cascade,
	constraint FK_StudentSessionIfnoFkSTUDENT foreign key(StudentId) references Student(Id) on delete cascade on update cascade,
	constraint UQ_StudentExamUQ unique (StudentId,ExamInfoId),
)