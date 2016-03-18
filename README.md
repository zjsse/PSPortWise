PSPortWise
==========
> A PowerShell module for PortWise

What is PSPortWise?
---
PSPortWise is a (very) thin wrapper around PortWise XPI (specifically the User Account Web Service). It is tested on PortWise XPI release 4.12 and PowerShell 5.0. 

Prerequisites
---
- XPI enabled on the Policy Service.
- An Authentication Method enabled that accepts PortWise Password. Note the Authentication Method ID. You will need it to connect.
- An account in the Super Administrator role. You can create a new role and customize privileges, but it may limit the tasks you can perform (you will see Permission Denied error messages). The account must have PortWise Password enabled. To be able to delegate permissions, the account must be linked to the directory. I have not found a way to delegate to a user that only exists in the PortWise user store.

Install module
---
The easiest way to install the module is to put the files the Documents folder under WindowsPowerShell\Modules\PSPortWise. This will make the module available only to you. Read here https://msdn.microsoft.com/en-us/library/dd878350%28v=vs.85%29.aspx for more information on how to install PowerShell modules.

Connect
---
The first thing you have to do is to authenticate. You do this once for the duration of the session (until you close the PowerShell console or the authentication times out). Use the cmdlet Connect-PWPolicyService to connect:
```
Connect-PWPolicyService -Url 'https://portwise.example.com:7443' -PortWisePasswordId 2 -Credential (Get-Credential)
```
Cmdlets
---
When you are connected you can explore the different cmdlets available. Try Get-Help [cmdlet] to get more information about how to use each cmdlet in the module.

**Cmdlets and how they map to the Web Services:**

Cmdlet | Web Service method (User Account)
--- | ---
Get-PWAccount | getAccount
Get-PWLockedAccount | getLocked
New-PWAccount  | add
Remove-PWAccount | remove
Reset-PWAccount | reset
Set-PWAccount | update
Set-PWAccountLinkState | link, unlink
Enable-PWAccount | enable
Disable-PWAccount | disable
Unlock-PWaccount | unlock
Reset-PWAccount | reset
(Not implemented) | isLocked
(Not implemented) | isDefined
Connect-PWPolicyService | authenticate (Authentication)
New-PWChallengeProperties | (n/a)
New-PWGlobalAccess | (n/a)
New-PWInvisibleTokenProperties | (n/a)
New-PWMapItem | (n/a)
New-PWMethodAccess | (n/a)
New-PWMobileTextProperties | (n/a)
New-PWPasswordProperties | (n/a)
New-PWSynchronizedProperties | (n/a)
New-PWWebProperties | (n/a)

Examples
-------------

Get an account from the user store.
```
PS> Get-PWAccount john@example.com

Account              : PortWiseWS.Account
IsDisconnected       : False
CustomAttributes     :
DisplayName          : John D.
EmailAddress         : john@example.com
Enabled              : True
GlobalAccess         : PortWiseWS.GlobalAccess
LastLogon            : 2016-03-04 14:50:07
LocationDn           : CN=John,OU=Users,DC=example,DC=com
MethodAccess         : PortWiseWS.MethodAccess
NotificationMappings : {}
SmsNumber            : 555-12345
UserCertificate      :
UserName             : john@example.com
ValidFrom            : 2015-01-01 00:00:00
ValidTo              :
```

Add a new linked account to the store with PortWise Password and Invisible Token enabled.
```
$pass = New-PWPasswordProperties -UseDirectoryPassword $true
$inv = New-PWInvisibleTokenProperties -UseDirectoryPassword $true
$ma = New-PWMethodAccess -MaxRetries 15 -PasswordProperties $pass -InvisibleTokenProperties $inv
New-PWAccount -UserName john@example.com -DisplayName 'John D.' -MethodAccess $ma -Linked
```

Adding a new authentication method to an existing account. If you want to overwrite all existing methods, you don't have to get the account first. Instead you create a new MethodAccess object and pass it to Set-PWAccount.
```
$a = Get-PWAccount john@example.com
$pass = New-PWPasswordProperties -UseDirectoryPassword $true
$a.MethodAccess.passwordProps = $pass
$a | Set-PWAccount
```

Get all accounts from the user store. Unfortunately wildcard search is not supported by XPI so we have to get all the users from the directory instead. This example assumes the directory is Active Directory.
```
Import-Module ActiveDirectory
# PortWise stores user name in the Locality-Name attribute (LDAP Display Name is lower case L).
$adobjects = Get-ADObject -LDAPFilter '(l=*)' -SearchBase 'OU=accounts,OU=PortWise,DC=example,DC=com' -Property l
$pwaccounts = $adobjects | Foreach-Object -Process {Get-PWAccount -Identity $_.l} 
```
