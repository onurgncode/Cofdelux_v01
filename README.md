# CofDelux 🏪💳

**CofDelux**, kafe ve restoranlar için geliştirilmiş **masa adisyon takip** ve **ödeme yönetimi** uygulamasıdır. Bu uygulama sayesinde işletmeler:

✅ **Masa bazlı sipariş alabilir**,  
✅ **Ödemeleri nakit / kredi kartı olarak kaydedebilir**,  
✅ **Fiş kesebilir ve satış raporları oluşturabilir**,  
✅ **Günlük, haftalık veya aylık satış analizleri yapabilir**.  

CofDelux, kullanıcı dostu arayüzü ve kolay kullanımı ile işletmelerin **hızlı ve verimli** çalışmasını sağlar. 🚀  

> **Uyarı**
> Programı Kullanmadan önce detaylı okuyun !


## Veri tabanı nasıl eklenir ?
Sql Server kurulu olması gerekir ayrıca Sql Server Menagement Studio(SSMS) ile de kurulumu yapacağız
[script.sql](https://github.com/onurgncode/Cofdelux_v01/blob/main/script.sql) dosyasını indirdikten sonra SSMS açıyoruz.
Yeni bir database oluşturup aşağıdaki kısmı değiştiriyoruz
![database](https://raw.githubusercontent.com/onurgncode/Cofdelux_v01/refs/heads/main/falan.PNG)
## DB eklenmesi gerekenler
- [x] Tables tablosuna | id:1 tableName:Masa1 | gibi 12 tane veri eklenmeli
- [x] Category tablosuna | categoryName:Yemekler | gibi 4 veri eklenmeli
- [x] İlk Girişte kayıt olarak giriş yapabilirsiniz


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



### **Bu program Görsel programlama dersi için ödev projesidir.**
### **Bu programı geliştirirken yardımcı olan @YusufAIpp @NasirKrmzz Teşekkür ederim**
