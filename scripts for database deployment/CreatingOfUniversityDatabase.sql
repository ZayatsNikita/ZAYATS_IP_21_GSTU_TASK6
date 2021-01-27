if db_id('UniversityDatabase') is null
begin 
	create database UniversityDatabase
end;
else
begin 
	drop database UniversityDatabase
	create database UniversityDatabase
end;
