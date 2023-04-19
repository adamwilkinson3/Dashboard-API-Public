CREATE PROCEDURE [dbo].[spCustomer_Insert]
	@first_name nvarchar(45),
	@last_name nvarchar(45),
	@email nvarchar(50),
	@active bit,
	@create_date datetime
AS
begin
	insert into [dbo].[customer] (first_name, last_name, email, active, create_date)
	values (@first_name, @last_name, @email, @active, @create_date);
end