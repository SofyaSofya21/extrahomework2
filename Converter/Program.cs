// Конвертер валют. 
// У пользователя есть баланс в каждой из представленных валют. 
// С помощью команд он может попросить сконвертировать одну валюту в другую. 
// Курс конвертации просто описать в программе. 
// Программа заканчивает свою работу в тот момент, когда решит пользователь.

int rubAmout = ReadInt("Введите имеющееся количество денежных едениц в рублях: ");
int usdAmout = ReadInt("Введите имеющееся количество денежных едениц в долларах: ");
int eurAmout = ReadInt("Введите имеющееся количество денежных едениц в евро: ");
int rmbAmout = ReadInt("Введите имеющееся количество денежных едениц в юанях: ");

double samecurrency = 1;
double rubusd = 0,017;
double rubeur = 0,016;
double rubrmb = 0,12;
double usdeur = 0,97;
double usdrmb = 7,12;
double eurrmb = 7,36;

double[,] rates = new double[4,4]{{samecurrency,rubusd,rubeur,rubrmb},{rubusd,samecurrency,usdeur,usdrmb},{rubeur,usdeur,samecurrency,eurrmb},{rubrmb,usdrmb,eurrmb,samecurrency}};



int ReadInt(string message)
{
    Console.WriteLine(message);
    return Convert.ToInt32(Console.ReadLine());
}



