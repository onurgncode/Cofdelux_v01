# CofDelux 🏪💳

**CofDelux**, kafe ve restoranlar için geliştirilmiş **masa adisyon takip** ve **ödeme yönetimi** uygulamasıdır. Bu uygulama sayesinde işletmeler:

✅ **Masa bazlı sipariş alabilir**,  
✅ **Ödemeleri nakit / kredi kartı olarak kaydedebilir**,  
✅ **Fiş kesebilir ve satış raporları oluşturabilir**,  
✅ **Günlük, haftalık veya aylık satış analizleri yapabilir**.  

CofDelux, kullanıcı dostu arayüzü ve kolay kullanımı ile işletmelerin **hızlı ve verimli** çalışmasını sağlar. 🚀  

> **Uyarı**
> Programı Kullanmadan önce detaylı okuyun !


## Ana ekran
![Home](https://raw.githubusercontent.com/onurgncode/Cofdelux_v01/refs/heads/main/Resimler/Ekran_Al%C4%B1nt%C4%B1s%C4%B12.PNG)

connect.cs > Sql servere bağlayınız 
```csharp
SqlConnection baglan = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;");
```
frmUrun.cs > Aynı Şekilde burayıda düzenleyiniz
```csharp
private async Task LoadDataAndCheckButton()
{
    try
    {
        await Task.Delay(100);
        using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;"))
```
frmCari.cs > Hava Durumu bilgisi için api giriniz
```csharp
private async Task<string> GetWeatherAsync(string city)
{
    string apiKey = "//"; // OpenWeatherMap API Key'inizi buraya yazın
```
frmCari.cs > Güncel Kur bilgisi için api giriniz yorum satırını kaldırınız
```csharp
            private async Task<string> GetDollarRateAsync()
            {
                //string apiKey = "//"; // Döviz kuru API Key'inizi buraya yazın
```



### **Bu program Nesne tabanlı programlama dersi için ödev projesidir.**
### **Bu programı geliştirirken yardımcı olan @YusufAIpp @NasirKrmzz Teşekkür ederim**
