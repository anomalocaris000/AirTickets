using AirTickets.Services;

var service = new TicketService();

while (true)
{
    Console.WriteLine("1. Добавить рейс");
    Console.WriteLine("2. Список рейсов");
    Console.WriteLine("3. Редактировать рейс");
    Console.WriteLine("4. Добавить аэропорт");
    Console.WriteLine("5. Список аэропортов");
    Console.WriteLine("6. Редактировать аэропорт");
    Console.WriteLine("7. Добавить клиента");
    Console.WriteLine("8. Список клиентов");
    Console.WriteLine("9. Редактировать клиента");
    Console.WriteLine("10. Продать билет");
    Console.WriteLine("11. Список проданных билетов");
    Console.WriteLine("12. Выручка");
    Console.WriteLine("0. Выход");
    Console.Write("Выбор: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Номер рейса: ");
            var number = ReadText();
            Console.Write("Авиакомпания: ");
            var airline = ReadText();
            Console.Write("Цена билета: ");
            var price = ReadPrice();
            Console.WriteLine($"Рейс добавлен. ID: {service.AddFlight(number, airline, price).Id}");
            break;

        case "2":
            foreach (var f in service.Flights)
                Console.WriteLine($"{f.Id}. {f.Number} | {f.Airline} | {f.TicketPrice:F2}");
            break;

        case "3":
            Console.Write("ID рейса: ");
            var flightId = ReadInt();
            Console.Write("Номер рейса: ");
            var newNumber = ReadText();
            Console.Write("Авиакомпания: ");
            var newAirline = ReadText();
            Console.Write("Цена билета: ");
            var newPrice = ReadPrice();
            Console.WriteLine(service.EditFlight(flightId, newNumber, newAirline, newPrice) ? "Рейс изменен" : "Рейс не найден или цена неверна");
            break;

        case "4":
            Console.Write("Название аэропорта: ");
            var airportName = ReadText();
            Console.Write("Город: ");
            var city = ReadText();
            Console.Write("Код аэропорта: ");
            var code = ReadText();
            Console.WriteLine($"Аэропорт добавлен. ID: {service.AddAirport(airportName, city, code).Id}");
            break;

        case "5":
            foreach (var a in service.Airports)
                Console.WriteLine($"{a.Id}. {a.Name} | {a.City} | {a.Code}");
            break;

        case "6":
            Console.Write("ID аэропорта: ");
            var airportId = ReadInt();
            Console.Write("Название: ");
            var newAirportName = ReadText();
            Console.Write("Город: ");
            var newCity = ReadText();
            Console.Write("Код: ");
            var newCode = ReadText();
            Console.WriteLine(service.EditAirport(airportId, newAirportName, newCity, newCode) ? "Аэропорт изменен" : "Аэропорт не найден");
            break;

        case "7":
            Console.Write("ФИО клиента: ");
            var fullName = ReadText();
            Console.Write("Email: ");
            var email = ReadText();
            Console.WriteLine($"Клиент добавлен. ID: {service.AddCustomer(fullName, email).Id}");
            break;

        case "8":
            foreach (var c in service.Customers)
                Console.WriteLine($"{c.Id}. {c.FullName} | {c.Email}");
            break;

        case "9":
            Console.Write("ID клиента: ");
            var customerId = ReadInt();
            Console.Write("ФИО клиента: ");
            var newName = ReadText();
            Console.Write("Email: ");
            var newEmail = ReadText();
            Console.WriteLine(service.EditCustomer(customerId, newName, newEmail) ? "Клиент изменен" : "Клиент не найден");
            break;

        case "10":
            if (!service.Flights.Any() || !service.Customers.Any())
            {
                Console.WriteLine("Сначала добавьте рейс и клиента");
                break;
            }

            Console.WriteLine("Рейсы:");
            foreach (var f in service.Flights)
                Console.WriteLine($"{f.Id}. {f.Number} | {f.Airline} | {f.TicketPrice:F2}");

            Console.WriteLine("Клиенты:");
            foreach (var c in service.Customers)
                Console.WriteLine($"{c.Id}. {c.FullName}");

            Console.Write("ID рейса: ");
            var sellFlightId = ReadInt();
            Console.Write("ID клиента: ");
            var sellCustomerId = ReadInt();
            var ticket = service.SellTicket(sellFlightId, sellCustomerId);
            Console.WriteLine(ticket == null ? "Рейс или клиент не найдены" : $"Билет продан. Номер: {ticket.Id}, стоимость: {ticket.Price:F2}");
            break;

        case "11":
            foreach (var t in service.Tickets)
                Console.WriteLine($"Билет {t.Id} | Рейс {service.FindFlight(t.FlightId)?.Number} | Клиент {service.FindCustomer(t.CustomerId)?.FullName} | {t.SaleDate:dd.MM.yyyy HH:mm} | {t.Price:F2}");
            break;

        case "12":
            Console.WriteLine($"Общая выручка: {service.Revenue:F2}");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Нет такого пункта");
            break;
    }

    Console.WriteLine();
}

static string ReadText()
{
    while (true)
    {
        var value = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(value)) return value.Trim();
        Console.Write("Введите значение: ");
    }
}

static int ReadInt()
{
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out var value) && value > 0) return value;
        Console.Write("Введите положительное целое число: ");
    }
}

static decimal ReadPrice()
{
    while (true)
    {
        var value = Console.ReadLine()?.Replace(',', '.');
        if (decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var price) && price > 0)
            return price;
        Console.Write("Введите цену больше нуля: ");
    }
}
