create database if not exists webAppDatabase;
use webAppDatabase;
create table if not EXISTS users
(
    EmployeeNumber varchar(20) PRIMARY KEY,
    Name        varchar(255) not null,
    Email     varchar(255) UNIQUE not null,
    Password        varchar(255) not null,
    IsAdmin        boolean not null DEFAULT false
);
insert into users(EmployeeNumber, Name, Email, Password,IsAdmin) values ('1234','Hans Gruber', 'hans@gruber.net','HashWorkInProgress',true);
insert into users(EmployeeNumber, Name, Email, Password) values ('1235','John McClane', 'jmcclan@nypd.com','UnderConstruction');
insert into users(EmployeeNumber, Name, Email, Password) values ('1236','Colin Powell', 'cpowell@lapd.com','twinkie');
insert into users(EmployeeNumber, Name, Email, Password) values ('1237','Mr Takagi', 'thebigone@thetower.com','GitGudScrb');