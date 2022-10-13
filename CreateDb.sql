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


create table if not EXISTS suggestions
(
    PostID int NOT NULL,
    SuggestionMakerID varchar(20) NOT NULL,
    Title varchar(100) NOT NULL,
    Category varchar(50) NOT NULL,
    TeamID varchar(100),
    Description varchar(255) NOT NULL,
    Phase varchar(20) NOT NULL,
    Status varchar(50) NOT NULL,
    TimeStamp varchar(100) NOT NULL,
    Deadline varchar(50) NOT NULL,
    PRIMARY KEY (PostID),
    FOREIGN KEY (SuggestionMakerID) REFERENCES users(EmployeeNumber)
);

insert into suggestions(PostID, SuggestionMakerID, Title, Category, TeamID, Description, Phase, Status, TimeStamp, Deadline) values ('69420', '1236', 'sølt sjokomelk', 'vaskehjelp', 'Logistikk', 'noen har sølt sjokomelk', 'ACT', 'In progress', '12:39', '10. Okt. 2022');
    