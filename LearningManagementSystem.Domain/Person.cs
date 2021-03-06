﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain
{
    public class Person : BaseModel
    {
        [Key]
        public long PersonID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public IDCard IDCard { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Address { get; set; }

        #region User

        #region Private Variables

        private string _password;
        private bool _forceUserToChangePassword;

        #endregion

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ForceUserToChangePassword = false;
            }
        }
        public string Salt { get; set; }

        public DateTime LastPasswordChangeDate { get; set; }

        public bool IsLockedOut { get; set; }

        public bool ForceUserToChangePassword
        {
            get { return _forceUserToChangePassword; }
            set
            {
                _forceUserToChangePassword = value;
                if (value == false)
                {
                    LastPasswordChangeDate = DateTime.Now;
                }
            }
        }

        #endregion

        public virtual ICollection<UploadedFile> Files { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female
    }

    public class EncryptionManagement
    {
        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[10];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string Encrypt(string input, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256Managed = new SHA256Managed();
            var encrypted = sha256Managed.ComputeHash(bytes);

            return Convert.ToBase64String(encrypted);
        }

        public static bool Validate(string sha256String, string password, string salt)
        {
            return Encrypt(password, salt) == sha256String;
        }
    }
}
