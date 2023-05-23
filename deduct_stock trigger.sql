CREATE TRIGGER deduct_stock
ON [dbo].[OrderItems]
AFTER INSERT
AS
BEGIN
-- deduct stock from ordered items
UPDATE Items
SET Items.Stock -= i.Quantity
FROM inserted i INNER JOIN Items
	ON i.ItemId = Items.Id
WHERE Items.Stock IS NOT NULL AND Items.Stock > 0
END