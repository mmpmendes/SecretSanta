# SecretSanta

Secret santa web app in Aspire.net

## Components (functional)

### Api

Just randomizes the pairs of friends, then for each pair takes the email of the giver and fires an email at him.

### Web

Simple form that allows you to setup as many friends as you wish, allows to setup their names and emails

## Setup

For it to work it's necessary to setup the appsettings.json of the ApiService

```json
"MailSettings": {
  "Host": "smtp host",
  "DefaultCredentials": false,
  "Port": 587,
  "Name": "No-reply-example",
  "EmailId": "<no.reply.example@example.com>",
  "UserName": "<no.reply.example@example.com>",
  "Password": "example_password",
  "UseSSL": true,
  "DefaultSubject": "Some test subject"
},
"template_path": "folder\\path\\of\\templates",
"secret_santa_template": "SecretSantaEmailTemplate.html"
```
