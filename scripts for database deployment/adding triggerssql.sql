use UniversityDatabase
go
create trigger StudentSessionInfoAdd
on StudentSessionIfno
instead of insert
as
	Declare @ExamInfoId int
	Declare @StudentId int
	Declare @Mark int
	Select @ExamInfoId = ExamInfoId FROM INSERTED
	Select @StudentId = StudentId FROM INSERTED
	Select @mark =Mark from INSERTED
	if (@ExamInfoId in (Select ExamInfoId from UniversitySessionInfo where GroupId = (Select top 1 GrioupId from Student where Id=@StudentId)))
	begin	
		insert into StudentSessionIfno (StudentId, ExamInfoId, Mark)
		select StudentId, ExamInfoId, Mark from INSERTED
	end

