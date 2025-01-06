using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class user
    {
        
        static connect bgl = new connect();
        public string userDogrula(string mail,string iste)
        {

            logKaydi kayit;
            try
            {
                // Sorgu oluştur
                SqlCommand komutBul = new SqlCommand("SELECT userID,firstName,lastName,password,role,mail FROM Users WHERE mail = @B1", bgl.baglanti("on"));
                komutBul.Parameters.AddWithValue("@B1", mail);

                // Bağlantıyı aç
                bgl.baglanti("on");

                // Sorguyu çalıştır ve sonucu al
                SqlDataReader reader = komutBul.ExecuteReader();


                if (reader.Read())
                {
                    if (iste == "isim")
                    {
                        return reader["firstName"].ToString();
                    }
                    else if (iste == "id")
                    {
                        return reader["userID"].ToString();
                    }
                    else if (iste == "soyisim")
                    {
                        return reader["lastName"].ToString();
                    }
                    else if (iste == "sifre")
                    {
                        return reader["password"].ToString();
                    }
                    else if (iste == "rol")
                    {
                        return reader["role"].ToString();
                    }
                    else if (iste == "mail")
                    {
                        return reader["mail"].ToString();
                    }
                    else
                    {
                        return "Hatalı İstek";
                    }
                }
                else
                {

                   return "Kullanıcı Bulunamadı";
                }

                
            }
            catch (SqlException ex)
            {
                // Hata durumunda loglama veya başka bir işlem yapılabilir
                // Hata fırlat
                MessageBox.Show(ex.Message, "hata");
                kayit = new logKaydi();
                kayit.gonder(ex);
                return "";
                

            }
            finally
            {
                // Bağlantıyı kapat
                bgl.baglanti("of");
            }
        }
        
        public void userListele()
        {
            bgl.baglanti("on");
            SqlCommand komutListele = new SqlCommand("select * from user order by asc", bgl.baglanti("on"));
            bgl.baglanti("of");
            
        }
        public void userEkle(string isim, string soyisim, string sifre, string 
            mail, string telefon)
        {
            try {
                SqlCommand komutEkle = new SqlCommand("insert into Users (firstName,lastName,password,mail,phoneNo) values " +
                "(@G2,@G3,@G4,@G6,@G7)", bgl.baglanti("on"));
                komutEkle.Parameters.AddWithValue("@G2",isim);
                komutEkle.Parameters.AddWithValue("@G3",soyisim);
                komutEkle.Parameters.AddWithValue("@G4",sifre);
                komutEkle.Parameters.AddWithValue("@G6",mail);
                komutEkle.Parameters.AddWithValue("@G7",telefon);
                komutEkle.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya başka bir işlem yapılabilir
                throw ex; // Hata fırlat
            }
            finally
            {
                // Bağlantıyı kapat
                bgl.baglanti("of");
                MessageBox.Show("Başarılı bir şekilde kayıt oldunuz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        void userSil(int id)
        {
            bgl.baglanti("on");
            SqlCommand komutSil = new SqlCommand("delete from user where id=@S1",bgl.baglanti("on"));
            komutSil.Parameters.AddWithValue(id.ToString(), "@S1");
            komutSil.ExecuteNonQuery();
            bgl.baglanti("of");
        }
        void userGuncelle(int id,string tcno,string isim, string soyisim, string sifre, int rol, string
            mail, string telefon, string adres, bool cinsiyet)
        {
            bgl.baglanti("on");
            SqlCommand komutGuncelle = new SqlCommand("update user set tcNo=@P1,firstName=@P2,lastName=@P3" +
                ",password=@P4,role=@P5,mail=@P6,phoneNo=@P7,adress=@P8,gender=@P9 WHERE userID=@P10",bgl.baglanti("on"));
            komutGuncelle.Parameters.AddWithValue(tcno, "@P1");
            komutGuncelle.Parameters.AddWithValue(isim, "@P2");
            komutGuncelle.Parameters.AddWithValue(soyisim, "@P3");
            komutGuncelle.Parameters.AddWithValue(sifre, "@P4");
            komutGuncelle.Parameters.AddWithValue(rol.ToString(), "@P5");
            komutGuncelle.Parameters.AddWithValue(mail, "@P6");
            komutGuncelle.Parameters.AddWithValue(telefon, "@P7");
            komutGuncelle.Parameters.AddWithValue(adres, "@P8");
            komutGuncelle.Parameters.AddWithValue(cinsiyet.ToString(), "@P9");
            komutGuncelle.Parameters.AddWithValue(id.ToString(), "@P10");
            bgl.baglanti("of");
        }
        
    }
}
