// Конвертер валют. 
// У пользователя есть баланс в каждой из представленных валют. 
// С помощью команд он может попросить сконвертировать одну валюту в другую. 
// Курс конвертации просто описать в программе. 
// Программа заканчивает свою работу в тот момент, когда решит пользователь.

// Библиотеки курсов валют
var rubRate = new Dictionary<string, double>()
{
    {"rub", 1},
    {"usd", 1/ 60.37},
    {"eur", 1/62.88},
    {"rmb", 1/8.52}
};
var usdRate = new Dictionary<string, double>()
{
    {"usd", 1},
    {"rub", 1 / rubRate["usd"]},
    {"eur", rubRate["eur"] / rubRate["usd"]},
    {"rmb", rubRate["rmb"] / rubRate["usd"]},
};
var eurRate = new Dictionary<string, double>()
{
    {"eur", 1},
    {"rub", 1 / rubRate["eur"]},
    {"usd", rubRate["usd"] / rubRate["eur"]},
    {"rmb", rubRate["rmb"] / rubRate["eur"]}
};
var rmbRate = new Dictionary<string, double>()
{
    {"rmb", 1},
    {"rub", 1 / rubRate["rmb"]},
    {"usd", rubRate["usd"] / rubRate["rmb"]},
    {"eur", rubRate["eur"] / rubRate["rmb"]}
};
Dictionary<string, double> rates = new Dictionary<string, double>();


PrintMenu("Это программа-конвертер валют. Список доступных команд:");
Console.WriteLine("Валюты, с которыми Вы можете работать: rub, usd, eur, rmb");
Console.WriteLine();

Console.WriteLine("Введите имеющееся количество денежных едениц");
double rubAmount = ReadInt("В рублях: ");
double usdAmount = ReadInt("В долларах: ");
double eurAmount = ReadInt("В евро: ");
double rmbAmount = ReadInt("В юанях: ");

double originCurrencyAmount = 0;
double transferAmount = 0;
double transferAmountinRub = 0;
double transferAmountinUsd = 0;
double transferAmountinEur = 0;
double transferAmountinRmb = 0;
double transferAmountConverted = 0;
double transferAmountConvertedtoRub = 0;
double transferAmountConvertedtoUsd = 0;
double transferAmountConvertedtoEur = 0;
double transferAmountConvertedtoRmb = 0;

bool programstop = false;
while (!programstop)
{
    string command = ReadString("Введите команду: ");
    switch (command)
    {
        case "Menu":
            PrintMenu("Список доступных команд:");
            break;
        case "Convert":
            string purpose = ReadCurrencyName("Какая целевая валюта конвертации? ");
            ConvertBalance(purpose);
            break;
        case "Transfer":
            Console.WriteLine("Данная команда позволяет сделать перевод денег из одной валюты в другую.");
            originCurrencyAmount = 0;

            string originCurrency = ReadCurrencyName("Из какой валюты будет сделан перевод? ");
            SwitchToCurrency(originCurrency); // получаем значение, сколько у нас валюты, которую можно перевести

            bool correctInput = false; //ввод суммы перевода с проверкой на корректность ввода
            while (!correctInput)
            {
                transferAmount = ReadInt("Какую сумму будем переводить? ");
                if (transferAmount <= originCurrencyAmount && transferAmount >= 0)
                {
                    correctInput = true;
                }
                else
                {
                    Console.Write($"Вы ввели некорректную сумму. Введите значение от 0 до {originCurrencyAmount}. ");
                }
            }

            string finalCurrency = ReadCurrencyName("В какую валюту будет сделан перевод? ");
            transferAmountConverted = ConvertCurrency(originCurrency, finalCurrency, transferAmount);

            SwitchToCurrency(originCurrency, finalCurrency); // присваиваем значения, которые нужны для уменьшения и увеличения валют

            TransferCurrency();
            break;
        case "Balance":
            PrintBalance();
            break;
        case "Exit":
            programstop = true;
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

// метод для считывания текстового ввода с возможностью выхода из программы
string ReadString(string message)
{
    Console.WriteLine(message);
    string input = Console.ReadLine();
    if (input == "Exit")
    {
        programstop = true;
    }
    return input;
}

// метод конвертации из 1 валюты в другую
double ConvertCurrency(string ourCurrency, string purposeCurrency, double amount)
{
    double convertResult = 0;
    SwitchToCurrency(ourCurrency);
    convertResult = amount * rates[purposeCurrency];
    return convertResult;
}

// метод конвертации всего баланса в заданную валюту
void ConvertBalance(string currency)
{
    double rubConvert = ConvertCurrency("rub", currency, rubAmount);
    double usdConvert = ConvertCurrency("usd", currency, usdAmount);
    double eurConvert = ConvertCurrency("eur", currency, eurAmount);
    double rmbConvert = ConvertCurrency("rmb", currency, rmbAmount);
    double totalConvert = rubConvert + usdConvert + eurConvert + rmbConvert;
    Console.WriteLine($"1. Рубли: {rubAmount} -> {rubConvert} {currency}");
    Console.WriteLine($"2. Доллары: {usdAmount} -> {usdConvert} {currency}");
    Console.WriteLine($"3. Евро: {eurAmount} -> {eurConvert} {currency}");
    Console.WriteLine($"4. Юани: {rmbAmount} -> {rmbConvert} {currency}");
    Console.WriteLine($"Общий баланс в {currency}: {totalConvert} {currency}");
}

// метод вывода баланса
void PrintBalance()
{
    Console.WriteLine($"1. Рубли: {rubAmount}");
    Console.WriteLine($"2. Доллары: {usdAmount}");
    Console.WriteLine($"3. Евро: {eurAmount}");
    Console.WriteLine($"4. Юани: {rmbAmount}");
}

// метод перевода денег из одной валюты в другую
void TransferCurrency()
{
    rubAmount = rubAmount - transferAmountinRub + transferAmountConvertedtoRub;
    usdAmount = usdAmount - transferAmountinUsd + transferAmountConvertedtoUsd;
    eurAmount = eurAmount - transferAmountinEur + transferAmountConvertedtoEur;
    rmbAmount = rmbAmount - transferAmountinRmb + transferAmountConvertedtoRmb;
    Console.WriteLine("Конвертация прошла успешно. Ваш текущий баланс:");
    Console.WriteLine($"1. Рубли: {rubAmount}");
    Console.WriteLine($"2. Доллары: {usdAmount}");
    Console.WriteLine($"3. Евро: {eurAmount}");
    Console.WriteLine($"4. Юани: {rmbAmount}");
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

// switch for ConvertCurrency
void SwitchToCurrency(string currencyName, string finalCurrencyName = "0")
{
    // сначала обнулим все значения, для корректной работы при повторных транзакциях
    transferAmountinRub = 0;
    transferAmountinUsd = 0;
    transferAmountinEur = 0;
    transferAmountinRmb = 0;
    transferAmountConvertedtoRub = 0;
    transferAmountConvertedtoUsd = 0;
    transferAmountConvertedtoEur = 0;
    transferAmountConvertedtoRmb = 0;
    // указываем какие курсы используем
    // задаем величину доступной для перевода суммы 
    // указываем сумму, на кот уменьшим валюту в результате транзакции
    switch (currencyName)
    {
        case "rub":
            rates = new Dictionary<string, double>(rubRate);
            originCurrencyAmount = rubAmount;
            transferAmountinRub = transferAmount;
            break;
        case "usd":
            rates = new Dictionary<string, double>(usdRate);
            originCurrencyAmount = usdAmount;
            transferAmountinUsd = transferAmount;
            break;
        case "eur":
            rates = new Dictionary<string, double>(eurRate);
            originCurrencyAmount = eurAmount;
            transferAmountinEur = transferAmount;
            break;
        case "rmb":
            rates = new Dictionary<string, double>(rmbRate);
            originCurrencyAmount = rmbAmount;
            transferAmountinRmb = transferAmount;
            break;
    }
    // указываем сумму, на кот увелиичим валюту в результате транзакции
    switch (finalCurrencyName)
    {
        case "rub":
            transferAmountConvertedtoRub = transferAmountConverted;
            break;
        case "usd":
            transferAmountConvertedtoUsd = transferAmountConverted;
            break;
        case "eur":
            transferAmountConvertedtoEur = transferAmountConverted;
            break;
        case "rmb":
            transferAmountConvertedtoRmb = transferAmountConverted;
            break;
    }
};

// метод считывания валюты с проверкой корректности ввода
string ReadCurrencyName(string message)
{
    Console.WriteLine(message);
    string currencyName = "0";
    bool correctCurrency = false;
    while (!correctCurrency)
    {
        currencyName = Console.ReadLine();
        // ниже условия для названия валют, но разумнее было бы расписать последовательную проверку, 
        // чтобы программу можно было адаптировать и под другие валюты и большее количество валют
        if (currencyName == "rub" || currencyName == "usd" || currencyName == "eur" || currencyName == "rmb")
        {
            correctCurrency = true;
        }
        else if (currencyName == "Exit")
        // как отсюда сделать выход вообще из программы?
        {
            programstop = true;
            correctCurrency = true;
        }
        else
        {
            Console.WriteLine("Вы ввели некорректное название валюты либо программа еще не поддерживает перевод в данную валюту. Введите валюту еще раз.");
        }
    }
    return currencyName;
}






