## HomeExpenses WebAPI is a financier of a house.

It provides easy way to track your home expenses, spendings and balance your economy.

### Description:

#### 1.Held data:

		HomeExpenses serializes recipts and stores them in a database, reciept is most important data structure, that
		other revolve around.
		*reciept*: 
		[
			id,
			date,
			time,
			paymentMethod,
			totalValue,
			store[],
			products[]
		]
		*Store* describes a place where the reciept is from, a place where goods were bought.
		*store*:
		[
			name,
			address,
			nip
		]
		*Products* describes bought goods.
		*product*:
		[
			name,
			itemPrice,
			ammount,
			discount,
			value
		]

#### 2. Endpoints:
		
		HomeExpenses provides described functionality with following endpoints:

		*/get

