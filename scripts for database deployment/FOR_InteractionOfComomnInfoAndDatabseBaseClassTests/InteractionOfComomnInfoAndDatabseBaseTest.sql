if db_id('InteractionOfComomnInfoAndDatabseBaseTest') is null
begin 
	create database InteractionOfComomnInfoAndDatabseBaseTest
end;
else
begin 
	drop database InteractionOfComomnInfoAndDatabseBaseTest
	create database InteractionOfComomnInfoAndDatabseBaseTest
end;
