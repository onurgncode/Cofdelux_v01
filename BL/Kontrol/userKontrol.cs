using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Kontrol
{
    public class userKontrol
    {
        private int userID;
        private string tcNO;
        private string firstName;
        private string lastName;
        private string password;
        private int role;
        private string mail;
        private string phoneNo;
        private string adress;
        private string cinsiyet;

        public int UserID { get => userID; set => userID = value; }
        public string TcNO { get => tcNO; set => tcNO = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public int Role { get => role; set => role = value; }
        public string Mail { get => mail; set => mail = value; }
        public string PhoneNo { get => phoneNo; set => phoneNo = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Cinsiyet { get => cinsiyet; set => cinsiyet = value; }
    }
}
