/////////////////
STORED PROCEDURES
/////////////////
(View file in Raw version for better layout)

CREATE PROCEDURE [dbo].[spStaffAddress_Insert]
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50),
	@username nvarchar(16),
	@password nvarchar(40),
	@address1 nvarchar(255),
	@address2 nvarchar(150),
	@state_id smallint,
	@city nvarchar(189),
	@postal_code nvarchar(10),
	@phone nvarchar(20)
AS
BEGIN
	BEGIN TRANSACTION
		SET XACT_ABORT ON
		INSERT INTO [address] (address1, address2, state_id, city, postal_code, phone)
		VALUES (@address1, @address2, @state_id, @city, @postal_code, @phone);

		INSERT INTO [staff] (first_name, last_name, email, address_id, active, username, [password])
		VALUES (@first_name, @last_name, @email, SCOPE_IDENTITY(), 1, @username, HASHBYTES('SHA2_512', @password));
	COMMIT
END

CREATE PROCEDURE [dbo].[spStaff_Update]
	@id int,
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50),
	@username nvarchar(16),
	@address1 nvarchar(255) = null,
	@address2 nvarchar(150) = null,
	@state_id smallint = null,
	@city nvarchar(189) = null,
	@postal_code nvarchar(10) = null,
	@phone nvarchar(20) = null
AS
begin
	BEGIN TRANSACTION
		SET XACT_ABORT ON

		update [dbo].[staff] 
		set first_name = @first_name, last_name = @last_name, email = @email, username = @username
		where id = @id;

		if(@address1 is not null or @address2 is not null or @state_id is not null or @city is not null or @postal_code is not null or @phone is not null)
		begin
			--address parameters have values
			declare @address_id int;
			set @address_id = (select address_id from [dbo].[staff] where id = @id);
			if(@address_id is not null)
				--staff has address record
				update [dbo].[address] 
				set address1 = @address1, address2 = @address2, state_id = @state_id, city = @city, postal_code = @postal_code, phone = @phone
				where id = @address_id;
			else
				begin
				--staff does not have address record
				insert into [dbo].[address] (address1, address2, state_id, city, postal_code, phone)
				values (@address1, @address2, @state_id, @city, @postal_code, @phone);

				update [dbo].[staff]
				set address_id = SCOPE_IDENTITY()
				where id = @id;
				end
		end
	COMMIT
end

CREATE PROCEDURE [dbo].[spStaff_GetAll]

AS
begin
	select a.id, a.first_name, a.last_name, a.email, a.active, a.username, b.address1, b.address2, b.city, b.postal_code, b.phone, c.[state]
	from dbo.[staff] a 
	left join dbo.[address] b
	on a.address_id = b.id
	left join dbo.[state] c
	on b.state_id = c.id;
end

CREATE PROCEDURE [dbo].[spStaff_Get]
	@id int
AS
begin
	select a.id, a.first_name, a.last_name, a.email, a.username, b.address1, b.address2, b.state_id, b.city, b.postal_code, b.phone
	from dbo.[staff] a 
	left join dbo.[address] b
	on a.address_id = b.id
	where a.id = @id;
end

CREATE PROCEDURE [dbo].[spStaff_Delete]
	@id int
AS
begin
BEGIN TRANSACTION
	SET XACT_ABORT ON
	declare @addressId INT
	set @addressId = (select address_id from [dbo].[staff] where id = @id)
	delete from [dbo].[staff] where id = @id
	delete from [dbo].[address] where id = @addressId;
	COMMIT
end

CREATE PROCEDURE [dbo].[spCustomerAddress_Insert]
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50),
	@address1 nvarchar(255),
	@address2 nvarchar(150),
	@state_id smallint,
	@city nvarchar(189),
	@postal_code nvarchar(10),
	@phone nvarchar(20)
AS
BEGIN
	BEGIN TRANSACTION
		SET XACT_ABORT ON
		INSERT INTO [address] (address1, address2, state_id, city, postal_code, phone)
		VALUES (@address1, @address2, @state_id, @city, @postal_code, @phone);

		INSERT INTO [customer] (first_name, last_name, email, address_id, active, create_date)
		VALUES (@first_name, @last_name, @email, SCOPE_IDENTITY(), 1, CURRENT_TIMESTAMP);
	COMMIT
END

CREATE PROCEDURE [dbo].[spCustomer_Update]
	@id int,
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50),
	@address1 nvarchar(255) = null,
	@address2 nvarchar(150) = null,
	@state_id smallint = null,
	@city nvarchar(189) = null,
	@postal_code nvarchar(10) = null,
	@phone nvarchar(20) = null
AS
begin
	BEGIN TRANSACTION
		SET XACT_ABORT ON

		update [dbo].[customer] 
		set first_name = @first_name, last_name = @last_name, email = @email
		where id = @id;

		if(@address1 is not null or @address2 is not null or @state_id is not null or @city is not null or @postal_code is not null or @phone is not null)
		begin
			--address parameters have values
			declare @address_id int;
			set @address_id = (select address_id from [dbo].[customer] where id = @id);
			if(@address_id is not null)
				--customer has address record
				update [dbo].[address] 
				set address1 = @address1, address2 = @address2, state_id = @state_id, city = @city, postal_code = @postal_code, phone = @phone
				where id = @address_id;
			else
				begin
				--customer does not have address record
				insert into [dbo].[address] (address1, address2, state_id, city, postal_code, phone)
				values (@address1, @address2, @state_id, @city, @postal_code, @phone);

				update [dbo].[customer]
				set address_id = SCOPE_IDENTITY()
				where id = @id;
				end
		end
	COMMIT
end

CREATE PROCEDURE [dbo].[spCustomer_Insert]
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50)
AS
begin
	insert into [dbo].[customer] (first_name, last_name, email, active, create_date)
	values (@first_name, @last_name, @email, 1, CURRENT_TIMESTAMP);
end

CREATE PROCEDURE [dbo].[spCustomer_GetAll]

AS
begin
	select a.id, a.first_name, a.last_name, a.email, a.active, a.create_date, b.address1, b.address2, b.city, b.postal_code, b.phone, c.[state]
	from dbo.[customer] a 
	left join dbo.[address] b
	on a.address_id = b.id
	left join dbo.[state] c
	on b.state_id = c.id;
end

CREATE PROCEDURE [dbo].[spCustomer_Get]
	@id int
AS
begin
	select a.id, a.first_name, a.last_name, a.email, b.address1, b.address2, b.state_id, b.city, b.postal_code, b.phone
	from dbo.[customer] a 
	left join dbo.[address] b
	on a.address_id = b.id
	where a.id = @id;
end

CREATE PROCEDURE [dbo].[spCustomer_Delete]
	@id int
AS
begin
BEGIN TRANSACTION
	SET XACT_ABORT ON
	declare @addressId INT
	set @addressId = (select address_id from [dbo].[customer] where id = @id)
	delete from [dbo].[customer] where id = @id
	delete from [dbo].[address] where id = @addressId;
	COMMIT
end
