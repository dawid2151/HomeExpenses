Data return model is based on *many to one* corelation.
There are 3 primary data types: **Store**, **Receipt**, **Product**.

#####Store
Represents a entity with selling capabilities. It has one to many connection with *receipt* - one *store* can have many *receipts*.

####Receipt
Represents a entity given out by one of *stores*. It has a one to one relation with *store* - one *receipt* has *one *store*.
It has a one to many relation with *products* - one *receipt* has many *products*.

***returns :*** json object
[
	id,
	date_time,
	payment_method,
	store_id,
	products[]
]

####Product
Represents a single product visible on a *receipt*. It has one to one relation with *receipt* - one *product* has one *receipt*.

***returns :*** json object
[
	id,
	receipt_id,
	category,
	name,
	item_price,
	ammount,
	discount
]