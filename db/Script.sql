use master 
go
create database ChubbInventory
go
use ChubbInventory
go
--Creacion tabla donde se almacena la informacion del Item
create table Item
(
	idItem int primary key identity(1,1),
	numItem int not null,
	[Name] varchar(30) not null
)
go
--Creacion de la tabla que llevara el control del inventario
create table Inventory
(
	idInventory int primary key identity(1,1),
	idItem int constraint fk_inventorytoitem foreign key references Item (idItem) not null,
	Quantity int,
	Withdrawal int
)
go

--Sp que se encarga de guardar los item, en caso de insertar un item ya existente se actualizara el numero de item del registro guardado
create procedure saveItem
@numItem int ,
@Name varchar(30) 
as

declare @id int

if exists(select top 1* from Item where [Name] = Trim(@Name) ) 
begin

	update i
	set numItem = @numItem
	from Item i
	where [Name] = Trim(@Name)

	select @id = idItem from Item where [Name] = Trim(@Name)

end
else
	begin
		insert into Item (numItem,[Name]) values ( @numItem , @Name)
		set @id =  @@Identity
	end

	select * from Item where idItem = @id

go

--Sp que retorna los items
create procedure getItems 
as
begin
	select * from Item
end
go

--Sp para almacenar un item en el inventario, si existe actualiza la cantidad, en caso de no existir lo inserta
create procedure saveItemInventory
@idItem int,
@Quantity int
as
begin

	if exists (select top 1* from Inventory where idItem = @idItem)
	begin

		declare @currentAmount int
		select @currentAmount = Quantity from Inventory where idItem = @idItem

		update i
		set Quantity = @currentAmount + @Quantity
		from Inventory i
		where idItem = @idItem


	end
	else

		insert into Inventory (idItem,Quantity,Withdrawal)	values (@idItem,@Quantity,0)

	select Item.[Name] item , Item.numItem numItem, Inventory.Quantity quantity , Inventory.Withdrawal Withdrawal
	from Inventory
	inner join Item on Item.idItem = Inventory.idItem 
	where Inventory.idItem = @idItem

end
go

--Sp que retorna los valores del inventario
create procedure getInventory
as
begin
	select Item.[Name] item , Item.numItem numItem, Inventory.Quantity quantity , Inventory.Withdrawal Withdrawal
	from Inventory
	inner join Item on Item.idItem = Inventory.idItem
end
go

--Sp para descontar un item del inventario y aumentar su cantidad de retiro
create procedure discountItemInventory
@idItem int,
@Quantity int
as
begin

	declare @currentAmount int, @QuantitySold int, @resp varchar(30)

	--Guarda en variable la cantidad actual en la bd y la cantidad descontada
	select @currentAmount = Quantity, 
	@QuantitySold = Withdrawal 
	from Inventory 
	where idItem = @idItem
	
	--Valida si la cantidad actual es mayor a 0 o que la cantidad solicitada no sea mayor a la que se encuentra actualmente
	if @currentAmount > 0 and @Quantity < @currentAmount
	begin

		update i
		set Quantity = @currentAmount - @Quantity,
		Withdrawal = @QuantitySold + @Quantity
		from Inventory i
		where idItem = @idItem

		set @resp = 'OK'

	end
	else
		set @resp = 'Cantidad no disponible'

	select Item.[Name] item , Item.numItem numItem, Inventory.Quantity quantity , Inventory.Withdrawal Withdrawal,@resp as Resp
	from Inventory
	inner join Item on Item.idItem = Inventory.idItem 
	where Inventory.idItem = @idItem

end

go

exec saveItem 1001, 'Smartphone'
exec saveItem 1002, 'Computer'

exec getItems

exec saveItemInventory 1,12
exec saveItemInventory 2,5

exec getInventory

exec discountItemInventory 1,1
exec discountItemInventory 2,1