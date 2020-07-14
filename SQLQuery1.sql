use PersonalDiary
create table Users
(
	UsersID int primary key identity(1,1),
	UsersName nvarchar(50),
	UsersPhoto nvarchar(50),
	UsersPhone nvarchar(11),
	UsersPwd nvarchar(20),
	Remark nvarchar(200),
)
create table Diary
(
	DiaryID int primary key identity(1,1),
	DiaryTitle nvarchar(50),
	DiaryTitleContent nvarchar(4000),
	CreateTime date default getdate(),
	IsPublic bit default 0,
	DiaryType nvarchar(50),
	UsersID int foreign key references Users(UsersID),
)

insert into Users values('уехЩ','','15524257845','123456','')