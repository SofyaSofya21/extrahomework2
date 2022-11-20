// Конвертер валют. 
// У пользователя есть баланс в каждой из представленных валют. 
// С помощью команд он может попросить сконвертировать одну валюту в другую. 
// Курс конвертации просто описать в программе. 
// Программа заканчивает свою работу в тот момент, когда решит пользователь.


PrintMenu("Это программа-конвертер валют. Список доступных команд:");
Console.WriteLine();

Console.WriteLine("Введите имеющееся количество денежных едениц");
double rubAmount = ReadInt("В рублях: ");
double usdAmount = ReadInt("В долларах: ");
double eurAmount = ReadInt("В евро: ");
double rmbAmount = ReadInt("В юанях: ");

double samecurrency = 1;
double rubusd = 0.017;
double rubeur = 0.016;
double rubrmb = 0.12;
double usdeur = 0.97;
double usdrmb = 7.12;
double eurrmb = 7.36;
double usdrub = 1 / rubusd;
double eurrub = 1 / rubeur;
double rmbrub = 1 / rubrmb;
double eurusd = 1 / usdeur;
double rmbusd = 1 / usdrmb;
double rmbeur = 1 / eurrmb;


double[,] rates = new double[4, 4] { { samecurrency, rubusd, rubeur, rubrmb }, { usdrub, samecurrency, usdeur, usdrmb }, { eurrub, eurusd, samecurrency, eurrmb }, { rmbrub, rmbusd, rmbeur, samecurrency } };

bool programstop = false;
while(!programstop)
{
    string command = ReadString("Введите команду: ");
    switch (command)
    {
        case "Menu":
            PrintMenu("Список доступных команд:");
            break;
        case "Convert":
            bool correctCurrency = false;
            while (!correctCurrency)
            {
                string purpose = ReadString("Какая целевая валюта конвертации? ");
                // ниже условия для названия валют, но разумнее было бы расписать последовательную проверку, 
                // чтобы программу можно было адаптировать и под другие валюты и большее количество валют
                if (purpose == "rub" || purpose == "usd" || purpose == "eur" || purpose == "rmb")
                {
                    ConvertBalance(purpose);
                    correctCurrency = true;
                }
                else if (purpose == "Exit")
                // как отсюда сделать выход вообще из программы?
                {
                    correctCurrency = true;
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректное название валюты либо программа еще не поддерживает перевод в данную валюту.");
                }
            }
            break;
        case "Transfer":
            Console.WriteLine("Данная команда позволяет сделать перевод денег из одной валюты, в другую.");
            TransferOperation();
            break;
        case "Balance":
            PrintBalance();
            break;
            
        
        default:
            Console.WriteLine("Вы ввели некорректную команду.");
            break;
    }

}

Console.WriteLine("Спасибо за использование конвертера! Хорошего дня!");

int ReadInt(string message)
{
    Console.Write(message);
    return Convert.ToInt32(Console.ReadLine());
}

string ReadString(string message)
{
    Console.WriteLine(message);
    string input = Console.ReadLine();
    if(input == "Exit")
    {
        programstop = true;
    }
    return input;
}

// метод конвертации из 1 валюты в другую
double Convertor(string ourCurrency, string purposeCurrency, double amount)
{
    int index2 = 0;
    int index1 = 0;
    double balanceAmount = amount;
    switch (ourCurrency)
    {
        case "rub":
            index1 = 0;
            break;
        case "usd":
            index1 = 1;
            break;
        case "eur":
            index1 = 2;
            break;
        case "rmb":
            index1 = 3;
            break;
    }
    switch (purposeCurrency)
    {
        case "rub":
            index2 = 0;
            break;
        case "usd":
            index2 = 1;
            break;
        case "eur":
            index2 = 2;
            break;
        case "rmb":
            index2 = 3;
            break;
    }
    double convertResult = balanceAmount * rates[index1, index2];
    return convertResult;
}

// метод вывода баланса
void PrintBalance()
{
    Console.WriteLine($"1. Рубли: {rubAmount}");
    Console.WriteLine($"2. Доллары: {usdAmount}");
    Console.WriteLine($"3. Евро: {eurAmount}");
    Console.WriteLine($"4. Юани: {rmbAmount}");
}

// метод конвертации всего баланса в заданную валюту
void ConvertBalance(string currency)
{
    double rubConvert = Convertor("rub", currency, rubAmount);
    double usdConvert = Convertor("usd", currency, usdAmount);
    double eurConvert = Convertor("eur", currency, eurAmount);
    double rmbConvert = Convertor("rmb", currency, rmbAmount);
    double totalConvert = rubConvert + usdConvert + eurConvert + rmbConvert;
    Console.WriteLine($"1. Рубли: {rubAmount} -> {rubConvert} {currency}");
    Console.WriteLine($"2. Доллары: {usdAmount} -> {usdConvert} {currency}");
    Console.WriteLine($"3. Евро: {eurAmount} -> {eurConvert} {currency}");
    Console.WriteLine($"4. Юани: {rmbAmount} -> {rmbConvert} {currency}");
    Console.WriteLine($"Общий баланс в {currency}: {totalConvert} {currency}");
}

// метод вывода меню
void PrintMenu(string message)
{
    Console.WriteLine(message);
    Console.WriteLine("1. Menu - вывод списка доступных команд");
    Console.WriteLine("2. Convert - сконвертировать валюты");
    Console.WriteLine("3. Transfer - перевод денег из одной валюты в другую");
    Console.WriteLine("4. Balance - вывод доступного баланса");
    Console.WriteLine("5. Exit - завершение программы");
}

// метод перевода заданной суммы из одной валюты в другую
void TransferOperation()
{
string originCurrency = "0";
string finalCurrency = "0";
double transferAmount = 0;

bool correctInput1 = false; //ввод валюты, из кот переводим с проверкой на корректность ввода
while (!correctInput1)
{
    originCurrency = ReadString("Из какой валюты будет сделан перевод?");
    if (originCurrency == "rub" || originCurrency == "usd" || originCurrency == "eur" || originCurrency == "rmb")
    {
        correctInput1 = true;
    }
    else
    {
        Console.Write("Вы ввели некорректное значение. Введите название одной из валют. ");
    }
}

double originCurrencyAmount = 0;
switch (originCurrency)
{
    case "rub":
        originCurrencyAmount = rubAmount;
        break;
    case "usd":
        originCurrencyAmount = usdAmount;
        break;
    case "eur":
        originCurrencyAmount = eurAmount;
        break;
    case "rmb":
        originCurrencyAmount = rmbAmount;
        break;
}
bool correctInput2 = false; //ввод суммы перевода с проверкой на корректность ввода
while (!correctInput2)
{
    transferAmount = ReadInt("Какую сумму будем переводить? ");
    if (transferAmount <= originCurrencyAmount && transferAmount >= 0)
    {
        correctInput2 = true;
    }
    else
    {
        Console.Write($"Вы ввели некорректную сумму. Введите значение от 0 до {originCurrencyAmount}. ");
    }
}

bool correctInput3 = false; //ввод валюты, в кот переводим с проверкой на корректность ввода
while (!correctInput3)
{
    finalCurrency = ReadString("В какую валюту будет сделан перевод?");
    if (finalCurrency == "rub" || finalCurrency == "usd" || finalCurrency == "eur" || finalCurrency == "rmb")
    {
        correctInput3 = true;
    }
    else
    {
        Console.Write("Вы ввели некорректное значение. Введите название одной из валют. ");
    }
}

double transferConvert = Convertor(originCurrency, finalCurrency, transferAmount);

// уменьшаем валюту, из которой переводим 
switch (originCurrency)
{
    case "rub":
        rubAmount -= transferAmount;
        break;
    case "usd":
        usdAmount -= transferAmount;
        break;
    case "eur":
        eurAmount -= transferAmount;
        break;
    case "rmb":
        rmbAmount -= transferAmount;
        break;
}

//увеличиваем валюту, в которую переводим
switch (finalCurrency)
{
    case "rub":
        rubAmount += transferConvert;
        break;
    case "usd":
        usdAmount += transferConvert;
        break;
    case "eur":
        eurAmount += transferConvert;
        break;
    case "rmb":
        rmbAmount += transferConvert;
        break;
}

Console.WriteLine("Ваш баланс после осуществления перевода:");
PrintBalance();
}

