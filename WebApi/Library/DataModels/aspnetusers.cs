using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Library.DataModels 
{ 
     [TableName("aspnetusers")] 
     public class aspnetusers:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public string Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Email")] 
          public string Email 
          { 
               get{return _email;} 
               set{ 
                      _email=value; 
                     OnPropertyChange("Email");
                     }
          } 

          [DbColumn("EmailConfirmed")] 
          public 0 EmailConfirmed 
          { 
               get{return _emailconfirmed;} 
               set{ 
                      _emailconfirmed=value; 
                     OnPropertyChange("EmailConfirmed");
                     }
          } 

          [DbColumn("PasswordHash")] 
          public string PasswordHash 
          { 
               get{return _passwordhash;} 
               set{ 
                      _passwordhash=value; 
                     OnPropertyChange("PasswordHash");
                     }
          } 

          [DbColumn("SecurityStamp")] 
          public string SecurityStamp 
          { 
               get{return _securitystamp;} 
               set{ 
                      _securitystamp=value; 
                     OnPropertyChange("SecurityStamp");
                     }
          } 

          [DbColumn("PhoneNumber")] 
          public string PhoneNumber 
          { 
               get{return _phonenumber;} 
               set{ 
                      _phonenumber=value; 
                     OnPropertyChange("PhoneNumber");
                     }
          } 

          [DbColumn("PhoneNumberConfirmed")] 
          public 0 PhoneNumberConfirmed 
          { 
               get{return _phonenumberconfirmed;} 
               set{ 
                      _phonenumberconfirmed=value; 
                     OnPropertyChange("PhoneNumberConfirmed");
                     }
          } 

          [DbColumn("TwoFactorEnabled")] 
          public 0 TwoFactorEnabled 
          { 
               get{return _twofactorenabled;} 
               set{ 
                      _twofactorenabled=value; 
                     OnPropertyChange("TwoFactorEnabled");
                     }
          } 

          [DbColumn("LockoutEndDateUtc")] 
          public DateTime LockoutEndDateUtc 
          { 
               get{return _lockoutenddateutc;} 
               set{ 
                      _lockoutenddateutc=value; 
                     OnPropertyChange("LockoutEndDateUtc");
                     }
          } 

          [DbColumn("LockoutEnabled")] 
          public 0 LockoutEnabled 
          { 
               get{return _lockoutenabled;} 
               set{ 
                      _lockoutenabled=value; 
                     OnPropertyChange("LockoutEnabled");
                     }
          } 

          [DbColumn("AccessFailedCount")] 
          public int AccessFailedCount 
          { 
               get{return _accessfailedcount;} 
               set{ 
                      _accessfailedcount=value; 
                     OnPropertyChange("AccessFailedCount");
                     }
          } 

          [DbColumn("UserName")] 
          public string UserName 
          { 
               get{return _username;} 
               set{ 
                      _username=value; 
                     OnPropertyChange("UserName");
                     }
          } 

          private string  _id;
           private string  _email;
           private 0  _emailconfirmed;
           private string  _passwordhash;
           private string  _securitystamp;
           private string  _phonenumber;
           private 0  _phonenumberconfirmed;
           private 0  _twofactorenabled;
           private DateTime  _lockoutenddateutc;
           private 0  _lockoutenabled;
           private int  _accessfailedcount;
           private string  _username;
      }
}


