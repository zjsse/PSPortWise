using System;
using System.Security.Cryptography.X509Certificates;
using PortWiseWS;

namespace PSPortWise
{
    public class PWAccount
    {
        private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public Account Account { get; private set; }

        public bool IsDisconnected { get; private set; }

        public PWAccount()
        {
            Account = new Account();
            IsDisconnected = true;
        }
        public PWAccount(PWAccount pwAccount)
        {
            Account = pwAccount.Account;
            IsDisconnected = false;
        }
        public PWAccount(Account account)
        {
            Account = account;
            IsDisconnected = false;
        }
        public PWAccount(string userName)
        {
            Account = new Account();
            Account.userName = userName;
            IsDisconnected = true;
        }

        public MapItem[] CustomAttributes
        {
            get
            {
                return Account.customAttribs;
            }
            set
            {
                Account.customAttribs = value;
            }
        }
        public string DisplayName
        {
            get
            {
                return Account.displayName;
            }
            set
            {
                Account.displayName = value;
            }
        }
        public string EmailAddress
        {
            get
            {
                return Account.emailAddress;
            }
            set
            {
                Account.emailAddress = value;
            }
        }
        public bool Enabled
        {
            get
            {
                return Account.enabled;
            }
            set
            {
                Account.enabled = value;
            }
        }
        public GlobalAccess GlobalAccess
        {
            get
            {
                return Account.globalAccess;
            }
            set
            {
                Account.globalAccess = value;
            }
        }
        public DateTime? LastLogon
        {
            get
            {
                return Account.lastLogon == 0 ? null : (DateTime?)unixEpoch.AddMilliseconds(Account.lastLogon).ToLocalTime();
            }
        }
        public string LocationDn
        {
            get
            {
                return Account.locationDN;
            }
        }
        public MethodAccess MethodAccess
        {
            get
            {
                return Account.methodAccess;
            }
            set
            {
                Account.methodAccess = value;
            }
        }
        public MapItem[] NotificationMappings
        {
            get
            {
                return Account.notificationMappings;
            }
            set
            {
                Account.notificationMappings = value;
            }
        }
        public string SmsNumber
        {
            get
            {
                return Account.smsNumber;
            }
            set
            {
                Account.smsNumber = value;
            }
        }
        public X509Certificate2 UserCertificate
        {
            get
            {
                if (Account.userCertificate != null && Account.userCertificate.Length > 0)
                {
                    return new X509Certificate2(Account.userCertificate);
                }
                else
                {
                    return null;
                }
            }
        }
        public string UserName
        {
            get
            {
                return Account.userName;
            }
            set
            {
                Account.userName = value;
            }
        }
        public DateTime? ValidFrom
        {
            get
            {
                return Account.validFrom == 0 ? null : (DateTime?)unixEpoch.AddMilliseconds(Account.validFrom).ToLocalTime();
            }
            set
            {
                Account.validFrom = value == null ? 0 : (long)(TimeZoneInfo.ConvertTimeToUtc((DateTime)value) - unixEpoch).TotalMilliseconds;
            }
        }
        public DateTime? ValidTo
        {
            get
            {
                return Account.validTo == 0 ? null : (DateTime?)unixEpoch.AddMilliseconds(Account.validTo).ToLocalTime();
            }
            set
            {
                Account.validTo = value == null ? 0 : (long)(TimeZoneInfo.ConvertTimeToUtc((DateTime)value) - unixEpoch).TotalMilliseconds;
            }
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
