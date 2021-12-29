create database CafeManagement
go

use CafeManagement
go

-- Item ( food and drinks)
-- seat ( table )
-- Category
-- Account ( Staff)
-- Bill
--BillInfo

create table Seat
(
	id int identity primary key,
	name nvarchar(100) not null,
	status int not null,
	-- 0 mean empty , 1 mean has customer
)
go

create table Staff
(
	id int identity primary key ,
	displayName nvarchar(100) not null,
	userName nvarchar(100) not null unique,
	password nvarchar(1000) not null,
	phone nvarchar(20),
	gender int not null,
	birth date ,
	dateBecomeMember date default getDate() null
)
go

create table Customer
(
	id int identity primary key,
	name nvarchar(100) not null default 'Unknown',
	birthdate date ,
	gender int,
	dateBecomeMember date default getDate()
)
go

create table Category
(
	id int identity primary key,
	name nvarchar(100) not null,
)
go

create table Item
(
	id int identity primary key,
	name nvarchar(100) not null default 'Unknown item',
	idCategory int not null,
	price float not null,
	quantity int not null,
	foreign key (idCategory) references dbo.Category(id)
)
go

create table Bill
(
	id int identity primary key,
	idSeat int ,
	idStaff int not null,
	idCustomer int ,
	dateCheckIn DateTime not null default getDate(),
	dateCheckOut DateTime,
	type int not null default 0,
	-- 0 pay as cash , 1 pay as credit card
	-- idSeat 1 mean , take away service.

	foreign key (idSeat) references dbo.Seat(id),
	foreign key (idStaff) references dbo.Staff(id),
	foreign key (idCustomer) references dbo.Customer(id)
)
go


create table BillInfo
(
	id int identity unique,
	idBill int not null,
	idItem int not null,
	quantity int not null default 0,
	
	primary key(idBill,idItem) ,
	foreign key (idBill) references dbo.Bill(id),
	foreign key (idItem) references dbo.Item(id)
)
go


-- inserting 
select * from Staff
insert into dbo.Staff values
(N'Nguyễn Đức Trọng','admin','40bd001563085fc35165329ea1ff5c5ecbdbbeef','0909678191',1,'01/01/2001',getDate()),
(N'Nguyễn Minh Quang','minhquang88','7c222fb2927d828af22f592134e8932480637c0d','0811785321',1,getDate(),getDate()),
(N'Nguyễn Chí Tín','chitincute','7c222fb2927d828af22f592134e8932480637c0d','0805523561',1,'06/21/1999',getDate()),
(N'Lâm Anh Thy','thythy23','7c222fb2927d828af22f592134e8932480637c0d','1209322833',1,'04/21/1998',getDate()),
(N'Phạm Bảo Anh','baoanh22','12345678','180800599',1,'05/19/2000',getDate())
--delete from staff
--select * from Staff



insert into dbo.Category values
('Coffee'),
('Milk tea'),
('Blended'),
('Juice'),
('Snack'),
('Meal')



--drop table dbo.Item
--drop table BillInfo
--select * from Category
--delete from Category
insert into dbo.Item values
('Milk Coffee',1,19.500,10),
('Black Coffee',1,19.500,10),
('Almond Lotus Cappucino Crunch',1,50.500,10),
('Espresso Raspberry Almond',1,49.000,10),
('Coffee Tropical Smoothie',1,63.250,10),
('Cappuccino Blast',1,53.227,10),
('PhucLong Milk Tea',2,31.200,10),
('BlackSugar Milk Tea',2,29.300,10),
('Matcha Milk Tea',2,30.000,50),
('Pearl Milk Tea',2,37.000,52),
('Milkcow Tea',2,28.000,30),
('Green Tea Ice Blended',3,58.363,68),
('Peach tea Blended',3,63.227,52),
('Cookie Blended',3,58.363,31),
('Tropical Mixed Juice',4,48.637 ,25),
('Apple Juice',4,37.000,20),
('Orange Juice',4,28.000,20),
('Pomelo Juice',4,28.000,20),
('Croissant',5,18.300,10),
('Banh mi',6,20.300,10),
('Tart',5,29.300,10),
('Tiramisu',5,22.500,10),
('Sandwich',6,23.800,10),
('Pho',6,35.400,10),
('Beef Steak',6,75.000,18),
('Mix fried snack',5,100.000,12),
('Peach Berry Milk Tea',2,42.000,14),
('Healthy Green Muffin',5,100.000,9)


update Item
set idCategory=6
where id =13 or id=9 or id=10
select * from Category
select * from Item


--delete from Item
-- select * from Item


insert into Seat values
('Take a way',0),
('Ban1',0),
('Ban2',0),
('Ban3',0),
('Ban4',0),
('Ban5',0),
('Ban6',0),
('Ban7',0),
('Ban8',0),
('Ban9',0),
('Ban10',0)
go


--select * from Seat
 



insert into Bill values
(1,1,null,getDate(),getDate(),1),
(1,1,null,getDate(),getDate(),1),
(2,1,null,getDate(),getDate(),1),
(3,2,null,getDate(),getDate(),1),
(4,3,null,getDate(),getDate(),1),
(4,4,null,getDate(),getDate(),1),
(5,5,null,getDate(),getDate(),1)

insert into BillInfo values
(1,3,1),
(1,7,1),
(2,8,1),
(2,2,1),
(3,7,3),
(3,4,2),
(4,7,1),
(4,8,2),
(5,3,1),
(6,2,3),
(7,2,1)


-- procedure
go
create proc GetAccountByUsername
@username nvarchar(100)
as 
begin
	select * from dbo.Staff where username = @username
end
go


--exec dbo.GetAccountByUsername @username='ductrong77'

create proc UserLogin
@username nvarchar(100), @password nvarchar(1000)
as
begin
	select * from dbo.Staff where username = @username and password = @password
end 
go





create proc getSeatList
as select * from dbo.Seat
go

exec dbo.getSeatList

go
create proc getBillInfoByTableId
@seatId int 
as
select i.name,bi.quantity,i.price,b.dateCheckIn ,i.price*bi.quantity AS totalPrice from dbo.BillInfo as bi, dbo.Bill as b, dbo.Item as i
where bi.idBill = b.id and bi.idItem = i.id and b.idSeat=@seatId
and B.dateCheckIn in
(
	select top 1 b.dateCheckIn from Bill as b
	where b.idSeat=@seatId
	order by dateCheckIn desc
)
go

--exec dbo.getBillInfoByTableId @seatId=3

go
create proc InsertBill
@idSeat int,@idStaff int ,@type int
as 
begin
	insert dbo.Bill values
	(@idSeat,@idStaff,null,getDate(),getDate(),@type)
end
go




go
create proc InsertBillInfo
@idBill int,@idItem int,@quantity int
as
begin
	insert dbo.BillInfo values
	(@idBill,@idItem,@quantity)
end
go

create proc getStaffByUsername
@username nvarchar(100)
as
begin
	select * from Staff where userName = @username
end
go

--exec getStaffByUsername @username = 'admin'

create proc GetLastestBill
as
begin
	SELECT *
	FROM Bill 
	WHERE id=(
    SELECT max(id) FROM Bill
    )
end
go

go
create proc updateStaff
@username nvarchar(100), @disName nvarchar(100) , @phone nvarchar(20), @birth date
as
begin
	update  Staff
	set   displayName = @disName, phone=@phone , birth = @birth 
	where userName = @username
end
go

create proc updatePassword
@username nvarchar(100), @password nvarchar(1000)
as
begin
	update Staff
	set password = @password
	where userName = @username
end
go
create proc decreaseQuantity
@quantity int, @id int
as 
begin
	update Item
	set quantity = quantity - @quantity 
	where id = @id 
end
go
 
--use CafeManagement
 --select * from Item

exec decreaseQuantity @quantity = -1,@id = 1

select * from Item
select * from Seat
select * from Bill
select * from BillInfo
select * from Staff

delete from Item where id > 13


---- TESTING
select * from Staff

select b.id,b.type,b.dateCheckIn,s.userName,i.name,i.price,bi.quantity
from Bill as b, BillInfo bi,Staff s,Item i
where b.id = bi.idBill
and b.idStaff = s.id
and bi.idItem = i.id
order by id







select * from BillInfo

